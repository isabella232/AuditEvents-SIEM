// ----------------------------------------------------------------------------------------------------
// <copyright file="BaseAuditCallsiteMapCEF.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

#pragma warning disable SA1200
using System.Collections.Generic;
using Apprenda.SaaSGrid.Extensions.DTO;
using Newtonsoft.Json;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    using AuditMapFunc = System.Func<AuditedEventDTO, SyslogMessage>;

    /// <summary>
    /// Provide several default implementations top support formatting Apprenda Cloud Platform auditing events as CEF syslog messages.
    /// </summary>
    public abstract class BaseAuditCallsiteMapCef : IAuditCallsiteMap
    {
        /// <summary>
        /// Provides the platform version for the default CEF formatter factory
        /// </summary>
        protected string PlatformVersion { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAuditCallsiteMapCef"/> class.
        /// </summary>
        /// <param name="platformVersion">ACP Version to mark CEF messages with</param>
        public BaseAuditCallsiteMapCef(string platformVersion)
        {
            PlatformVersion = platformVersion;
            Formatters = new Dictionary<string, AuditMapFunc>();
        }

        /// <summary>
        /// Gets the container of Operation to Formatter mappings for this CallsiteMap.
        /// </summary>
        /// <value>The map of Operations to Formatters</value>
        public Dictionary<string, AuditMapFunc> Formatters { get; }

        /// <summary>
        /// Convenience method for adding an operation to formatter mapping.
        /// </summary>
        /// <param name="operation">The operation to map</param>
        /// <param name="formatter">The corresponding formatter</param>
        public void AddMap(string operation, AuditMapFunc formatter)
        {
            Formatters.Add(operation, formatter);
        }

        /// <summary>
        /// Factory method to produce Formatters which rename an Operation and format a result containing a ReportCard in the Details
        /// </summary>
        /// <param name="renamedOperation">The Operation as which to alias Events formatted by the resulting formatter.</param>
        /// <returns>A formatter</returns>
        protected AuditMapFunc DefaultReportCardMappedMapperCef(string renamedOperation)
        {
            return dto =>
            {
                dto.Operation = renamedOperation;
                return DefaultReportCardCefFormatter(dto);
            };
        }

        protected void AddDefaultMapCef(string operation, string cefEventId)
        {
            AddMap(operation, DefaultCefOpResultFormatter(cefEventId));
        }

        protected void AddMappedMapCef(string operationName, string renamedOperation, string cefEventId) => AddMappedMapCef(new[]{operationName}, renamedOperation, cefEventId);

        protected void AddMappedMapCef(IEnumerable<string> operations, string renamedOperation, string cefEventId)
        {
            if (operations == null)
            {
                return;
            }

            var mapper = MappedOperationCef(renamedOperation, cefEventId);
            foreach (var op in operations)
            {
                AddMap(op, mapper);
            }
        }

        protected void AddActionMapCef(string operationName, string cefEventId) => AddActionMapCef(operationName, operationName, cefEventId);

        protected void AddActionMapCef(string operationName, string renamedOperation, string cefEventId)
        {
            AddMappedMapCef(new[] { $"{operationName} Starting", $"{operationName} Completed", $"{operationName} Failed" }, renamedOperation, cefEventId);
        }

        protected AuditMapFunc MappedOperation(string newOperation)
        {
            return auditedEvent =>
            {
                auditedEvent.Operation = newOperation;
                return DefaultCefOpResultFormatter("-")(auditedEvent);
            };
        }

        protected AuditMapFunc MappedOperationCef(string newOperation, string cefEventId)
        {
            return auditedEvent =>
            {
                auditedEvent.Operation = newOperation;
                return DefaultCefOpResultFormatter(cefEventId)(auditedEvent);
            };
        }

        /// <summary>
        /// Formats an AuditedEventDTO that contains a ReportCard in the Details field.
        /// </summary>
        /// <param name="auditedEvent">The audited event to format</param>
        /// <returns>SyslogMessage representing the provided event</returns>
        public SyslogMessage DefaultReportCardCefFormatter(AuditedEventDTO auditedEvent)
        {
            if (auditedEvent == null)
            {
                return null;
            }

            var details = JsonConvert.DeserializeObject<DetailsObject>(auditedEvent.Details);
            var messageDetail = string.Empty;
            var reportCard = JsonConvert.DeserializeObject<ReportCard>(details.Details, new JsonSerializerSettings
            {
                Error = (unused, discarded) => messageDetail = $"{details.Details}",
            });
            if (reportCard != null)
            {
                messageDetail = $" {string.Join(";", reportCard.ErrorMessages.ToArray())}";
            }

            return auditedEvent.ToSyslogMessage($"CEF:0|Apprenda|CloudPlatform|{PlatformVersion}|-|{auditedEvent.Operation}|Unknown|outcome={auditedEvent.EventTypeDescription()} {messageDetail}");
        }

        /// <summary>
        /// Returs a formatter which formats an auditedevent into a CEF message with Message, Action and Outcome, without providing CEF Event Correlation.
        /// </summary>
        /// <param name="auditedEvent">The AuditedEventDTO to format</param>
        /// <returns>SyslogMessage representation of the event</returns>
        public AuditMapFunc DefaultCefActionResultFormatter(string cefEventId)
        {
            return auditedEvent =>
            {
                if (auditedEvent == null)
                {
                    return null;
                }

                var actionEvent = auditedEvent.Operation.LastWordOf();
                var operation = actionEvent.Item1;
                var eventType = actionEvent.Item2;
                var messageDetail = auditedEvent.Details == null
                    ? string.Empty
                    : $"msg={auditedEvent.Details}";
                var message = $"CEF:0|Apprenda|CloudPlatform|{PlatformVersion}|-|{operation}|Unknown|outcome={eventType} {messageDetail}";
                return auditedEvent.ToSyslogMessage(message);
            };
        }

        /// <summary>
        /// Returns a formatter which formats an auditedevent into a CEF message with Message, Action and Outcome, including expected Details property.
        /// </summary>
        /// <param name="cefEventId">A CEF Event ID for correlation</param>
        /// <returns>SyslogMessage representation of the event</returns>
        public AuditMapFunc DefaultCefOpResultFormatter(string cefEventId)
        {
            return auditedEvent =>
            {
                if (auditedEvent == null)
                {
                    return null;
                }

                var operationName = auditedEvent.Operation.LastWordOf().Item1;
                var extension = $"msg={auditedEvent.Details}";

                return auditedEvent.ToSyslogMessage($"CEF:0|Apprenda|CloudPlatform|{PlatformVersion}|{cefEventId}|{operationName}|Unknown|{extension}");
            };
        }
    }
}
