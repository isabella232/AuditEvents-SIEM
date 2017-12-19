// ----------------------------------------------------------------------------------------------------
// <copyright file="BaseAuditCallsiteMap.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------
#pragma warning disable SA1200 // Using directives should be placed correctly
using Apprenda.SaaSGrid.Extensions.DTO;
using SyslogNet.Client;
#pragma warning restore SA1200 // Using directives should be placed correctly

namespace Apprenda.AuditEventForwarder.Syslog
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    using AuditMapFunc = System.Func<AuditedEventDTO, SyslogMessage>;

    /// <summary>
    /// Provide several default implementations to support mapping Apprenda Cloud Platform auditing events
    /// from platform callsites.
    /// </summary>
    public abstract class BaseAuditCallsiteMap : IAuditCallsiteMap
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAuditCallsiteMap"/> class.
        /// </summary>
        /// <param name="platformVersion">Target cloud platform version</param>
        protected BaseAuditCallsiteMap()
        {
            Formatters = new Dictionary<string, AuditMapFunc>();
        }

        /// <summary>
        /// Gets the container of Operation to Formatter mappings for this CallsiteMap.
        /// </summary>
        /// <value>The map of Operations to Formatters</value>
        public Dictionary<string, AuditMapFunc> Formatters { get; }

        /// <summary>
        /// A factory for constructing a SyslogMessage based on details of the provided AuditedEventDto
        /// </summary>
        /// <param name="auditedEvent">The AuditedEventDto to format</param>
        /// <param name="message">The message body to send</param>
        /// <returns>SyslogMessage propagating basic AuditedEventDto details</returns>
        public static SyslogMessage FromEventDTO(AuditedEventDTO auditedEvent, string message) => (auditedEvent == null) ? null : FromEventDTO(auditedEvent, Facility.LogAudit, Severity.Informational, message);

        /// <summary>
        /// A factory for constructing a SyslogMessage based on details of the provided AuditedEventDto
        /// </summary>
        /// <param name="auditedEvent">The AuditedEventDto to format</param>
        /// <param name="facility">The Syslog facility identifier for this message</param>
        /// <param name="severity">The Syslog severity level for this message</param>
        /// <param name="message">The message body to send</param>
        /// <returns>SyslogMessage propagating basic AuditedEventDto details</returns>
        public static SyslogMessage FromEventDTO(AuditedEventDTO auditedEvent, Facility facility, Severity severity, string message)
        {
            return (auditedEvent == null) ? null : new SyslogMessage(
                dateTimeOffset: auditedEvent.Timestamp,
                facility: facility,
                severity: severity,
                hostName: auditedEvent.SourceIP,
                appName: "ApprendaCloudPlatform",
                message: message.StripNewLines(),
                procId: "-",
                structuredDataElements: new StructuredDataElement[] { },
                msgId: "-");
        }

        /// <summary>
        /// Formats an AuditedEventDTO that contains a ReportCard in the Details field.
        /// </summary>
        /// <param name="auditedEvent">The audited event to format</param>
        /// <returns>SyslogMessage representing the provided event</returns>
        public static SyslogMessage DefaultReportCardFormatter(AuditedEventDTO auditedEvent)
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

            return FromEventDTO(auditedEvent, $"{auditedEvent.Operation} {auditedEvent.EventTypeDescription()}{messageDetail}");
        }

        /// <summary>
        /// Convenience method for adding an operation to formatter mapping.
        /// </summary>
        /// <param name="operation">The operation to map</param>
        /// <param name="formatter">The corresponding formatter</param>
        public void AddMap(string operation, Func<AuditedEventDTO, SyslogMessage> formatter)
        {
            Formatters.Add(operation, formatter);
        }

        /// <summary>
        /// Adds a mapping from the provided Operation to the DefaultOpResultFormatter
        /// </summary>
        /// <param name="operation">The operation name to map</param>
        public void AddDefaultMap(string operation)
        {
            AddMap(operation, DefaultOpResultFormatter);
        }

        /// <summary>
        /// Default formatter for AuditedEventDTO whose EventType is not encoded in the message.
        /// </summary>
        /// <param name="auditedEvent">The audited event</param>
        /// <returns>Syslos Message representing the event</returns>
        public SyslogMessage DefaultActionResultMapper(AuditedEventDTO auditedEvent) => auditedEvent == null ? null : FromEventDTO(auditedEvent, auditedEvent.Operation);

        /// <summary>
        /// Default formatter for AuditedEventDTO whose EventType is not encoded in the message and which has a non-empty Results body to include.
        /// </summary>
        /// <param name="auditedEvent">The audited event</param>
        /// <returns>Syslos Message representing the event</returns>
        public SyslogMessage DefaultOpResultFormatter(AuditedEventDTO auditedEvent) => auditedEvent == null ? null : FromEventDTO(auditedEvent, Facility.LogAudit, Severity.Informational, $"{auditedEvent.Operation} {auditedEvent.EventTypeDescription()} {auditedEvent.Details.StripNewLines()}");

        /// <summary>
        /// Factory method to produce Formatters which rename an Operation and format a result containing a ReportCard in the Details
        /// </summary>
        /// <param name="renamedOperation">The Operation as which to alias Events formatted by the resulting formatter.</param>
        /// <returns>A formatter</returns>
        protected static AuditMapFunc DefaultReportCardMappedMapper(string renamedOperation)
        {
            return dto =>
            {
                dto.Operation = renamedOperation;
                return DefaultReportCardFormatter(dto);
            };
        }

        /// <summary>
        /// Convenience method for adding an operation emitted as operationName [Starting|Completed|Failed] to all map through the same formatter
        /// </summary>
        /// <param name="operationName">The base operation name</param>
        protected virtual void AddActionMap(string operationName) => AddActionMap(operationName, operationName);

        /// <summary>
        /// Convenience method for adding an operation emitted as operationName [Starting|Completed|Failed] to all map through the same formatter
        /// </summary>
        /// <param name="operationName">The base operation name</param>
        /// <param name="renamedOperation">The collecting operation name</param>
        protected virtual void AddActionMap(string operationName, string renamedOperation)
        {
            AddMappedMap(new[] { $"{operationName} Starting", $"{operationName} Completed", $"{operationName} Failed" }, renamedOperation);
        }

        /// <summary>
        /// Add a formatter which accepts an operation name and formats it as a different standardized operation name
        /// </summary>
        /// <param name="mappedOperation">Single operation to alias</param>
        /// <param name="renamedOperation">The standardized operation alias</param>
        protected virtual void AddMappedMap(string mappedOperation, string renamedOperation)
        {
            AddMap(mappedOperation, MappedOperation(renamedOperation));
        }

        /// <summary>
        /// Adds default formatters which accept a list of operation names and aliases them all to a standardized operation name.
        /// </summary>
        /// <param name="operations">An enumerable list of operation names to alias</param>
        /// <param name="renamedOperation">The operation name to alias them</param>
        protected virtual void AddMappedMap(IEnumerable<string> operations, string renamedOperation)
        {
            if (operations == null)
            {
                return;
            }

            var mapper = MappedOperation(renamedOperation);
            foreach (var op in operations)
            {
                AddMap(op, mapper);
            }
        }

        /// <summary>
        /// Creates a formatter that renames the operation before invoking the default operation result map.
        /// </summary>
        /// <param name="newOperation">The new operation name</param>
        /// <returns>An AuditMapFunc providing simple rename-and-format handling of an AuditedEventDTO.</returns>
        protected virtual AuditMapFunc MappedOperation(string newOperation)
        {
            return auditedEvent =>
            {
                auditedEvent.Operation = newOperation;
                return DefaultOpResultFormatter(auditedEvent);
            };
        }
    }
}