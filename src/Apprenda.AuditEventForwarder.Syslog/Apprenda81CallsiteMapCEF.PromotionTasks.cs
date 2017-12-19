// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.PromotionTasks.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    using System;
    using Apprenda.SaaSGrid.Extensions.DTO;
    using Newtonsoft.Json;
    using SyslogNet.Client;

/// <summary>
/// This partial provides the maps of Promotion Task operations to Formatters.
/// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        private void ConfigurePromotionTasks()
        {
            AddMap("Version Promotion", VersionPromotionFormatter);
        }

        /// <summary>
        /// AuditedEventDTO to SyslogMessage formatter for the Version Promotion operation family.
        /// </summary>
        /// <param name="auditedEvent">The AuditedEventDTO instance to format</param>
        /// <returns>SyslogMessage representation of the event</returns>
        private SyslogMessage VersionPromotionFormatter(AuditedEventDTO auditedEvent)
        {
            var severity = Severity.Informational;
            string detail;
            if (auditedEvent.EventType == AuditEventType.OperationFailed)
            {
                severity = Severity.Warning;
                detail = $"{auditedEvent.Details.StripNewLines()}";
            }
            else
            {
                var av = JsonConvert.DeserializeObject<ApplicationVersionDto>(auditedEvent.Details);
                detail = $"{av.Name} ({av.Alias}) {av.Stage}";
            }

            var message = $"CEF:0|Apprenda|CloudPlatform|{PlatformVersion}|-|{auditedEvent.Operation}|Unknown|outcome={auditedEvent.EventTypeDescription()} {detail}";

            return new SyslogMessage(
                auditedEvent.Timestamp,
                Facility.UserLevelMessages,
                severity,
                auditedEvent.SourceIP,
                "ApprendaCloudPlatform",
                message: message.StripNewLines(),
                procId: "-",
                structuredDataElements: new StructuredDataElement[0],
                msgId: "-");
        }
    }
}