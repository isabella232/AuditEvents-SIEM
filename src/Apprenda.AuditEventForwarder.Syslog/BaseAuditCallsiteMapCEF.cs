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
        /// Initializes a new instance of the <see cref="BaseAuditCallsiteMapCef"/> class.
        /// </summary>
        /// <param name="platformVersion">ACP Version to mark CEF messages with</param>
        protected BaseAuditCallsiteMapCef(string platformVersion)
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
        /// Gets the platform version for the default CEF formatter factory
        /// </summary>
        protected string PlatformVersion { get; }

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

        /// <summary>
        /// Add a DefaultCefOpResultFormatter for the provided CEF Event Id and operation.
        /// </summary>
        /// <param name="operation">The operation name</param>
        /// <param name="cefEventId">The CEF Event ID to map the operation to</param>
        protected void AddDefaultMapCef(string operation, string cefEventId)
        {
            AddMap(operation, DefaultCefOpResultFormatter(cefEventId));
        }

        /// <summary>
        /// Add a MappedOperationCef renamed operation and CEF ID to a given operation name.
        /// </summary>
        /// <param name="operationName">Operation to rename</param>
        /// <param name="renamedOperation">New operation name for formatter output</param>
        /// <param name="cefEventId">The CEF Event ID for this operation</param>
        protected void AddMappedMapCef(string operationName, string renamedOperation, string cefEventId) => AddMappedMapCef(new[] { operationName }, renamedOperation, cefEventId);

        /// <summary>
        /// Add a MappedOperationCef renamed operation and CEF ID to a given list of operations.
        /// </summary>
        /// <param name="operations">Operations to rename</param>
        /// <param name="renamedOperation">New operation name for formatter output</param>
        /// <param name="cefEventId">The CEF Event ID for this operation</param>
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

        /// <summary>
        /// Add a mapped operation name as an Action Map operation family with a CEF Event Id to the Formatters collection
        /// </summary>
        /// <param name="operationName">Operation Name of the Action</param>
        /// <param name="cefEventId">The CEF Event ID for the operation family.</param>
        protected void AddActionMapCef(string operationName, string cefEventId) => AddActionMapCef(operationName, operationName, cefEventId);

        /// <summary>
        /// Add a mapped operation name as an Action Map operation family with a CEF Event Id to the Formatters collection
        /// </summary>
        /// <param name="operationName">Operation Name of the Action</param>
        /// <param name="renamedOperation">Operation name of the mapped family.</param>
        /// <param name="cefEventId">The CEF Event ID for the operation family.</param>
        protected void AddActionMapCef(string operationName, string renamedOperation, string cefEventId)
        {
            AddMappedMapCef(new[] { $"{operationName} Starting", $"{operationName} Completed", $"{operationName} Failed" }, renamedOperation, cefEventId);
        }

        /// <summary>
        /// Returns a formatter which formats an auditedevent into a CEF message with an unknown ("-") CEF Event ID.
        /// </summary>
        /// <param name="newOperation">The operation to rename all operations passed into the formatter to</param>
        /// <returns>Formatter which creates SyslogMessages containing event details</returns>
        protected AuditMapFunc MappedOperation(string newOperation)
        {
            return auditedEvent =>
            {
                auditedEvent.Operation = newOperation;
                return DefaultCefOpResultFormatter("-")(auditedEvent);
            };
        }

        /// <summary>
        /// Returs a formatter which formats an auditedevent into a CEF message with Message, Action and Outcome, providing a CEF Event ID to the formatter.
        /// </summary>
        /// <param name="cefEventId">The AuditedEventDTO to format</param>
        /// <returns>SyslogMessage representation of the event</returns>
        protected AuditMapFunc DefaultCefActionResultFormatter(string cefEventId)
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
                var message = $"CEF:0|Apprenda|CloudPlatform|{PlatformVersion}|-|{operation}|{cefEventId}|outcome={eventType} {messageDetail}";
                return auditedEvent.ToSyslogMessage(message);
            };
        }

        /// <summary>
        /// Creates a AuditMapFunc Formatter that returns a new operation name and CEF event ID when formatting the auditedEVent
        /// </summary>
        /// <param name="newOperation">Operation name to overwrite the inbound audited event's operation name</param>
        /// <param name="cefEventId">the CEF Event ID of this operation</param>
        /// <returns>A formatter as described above</returns>
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
        protected SyslogMessage DefaultReportCardCefFormatter(AuditedEventDTO auditedEvent)
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
        /// Returns a formatter which formats an auditedevent into a CEF message with Message, Action and Outcome, including expected Details property.
        /// </summary>
        /// <param name="cefEventId">A CEF Event ID for correlation</param>
        /// <returns>SyslogMessage representation of the event</returns>
        protected AuditMapFunc DefaultCefOpResultFormatter(string cefEventId)
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
