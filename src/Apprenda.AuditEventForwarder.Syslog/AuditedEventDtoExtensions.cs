// ----------------------------------------------------------------------------------------------------
// <copyright file="AuditedEventDTOExtensions.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    using Apprenda.SaaSGrid.Extensions.DTO;
    using SyslogNet.Client;

    /// <summary>
    /// Provide extension methods for the AuditedEventDto type.
    /// </summary>
    public static class AuditedEventDTOExtensions
    {
        /// <summary>
        /// Retrieve an English descriptive word for the EventType property of the provided AuditedEventDTO instance.
        /// </summary>
        /// <param name="source">An AuditedEventDto</param>
        /// <returns>en-US Descriptive term for the AuditedEventDTO EventType property</returns>
        public static string EventTypeDescription(this AuditedEventDTO source)
        {
            if (source == null)
            {
                return "Unknown event state";
            }

            switch (source.EventType)
            {
                case AuditEventType.OperationCompleted: return "Completed";
                case AuditEventType.OperationFailed: return "Failed";
                case AuditEventType.OperationStarting: return "Starting";
                default: return "Unknown event state";
            }
        }

        /// <summary>
        /// Format an AuditedEventDTO into a syslog message containing a specific message body.
        /// </summary>
        /// <param name="auditedEvent">The audited event</param>
        /// <param name="message">The message body</param>
        /// <returns>Syslog Message containing the requested message and audited event details</returns>
        public static SyslogMessage ToSyslogMessage(this AuditedEventDTO auditedEvent, string message) => (auditedEvent == null) ? null : ToSyslogMessage(auditedEvent, Facility.LogAudit, Severity.Informational, message);

        /// <summary>
        /// Format an AuditedEventDTO into a syslog message containing a specific message body.
        /// </summary>
        /// <param name="auditedEvent">The audited event</param>
        /// <param name="facility">The Syslog facility identifier</param>
        /// <param name="severity">The Syslog severity identifier</param>
        /// <param name="message">The message body</param>
        /// <returns>Syslog Message containing the requested message and audited event details</returns>
        public static SyslogMessage ToSyslogMessage(this AuditedEventDTO auditedEvent, Facility facility, Severity severity, string message)
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
    }
}