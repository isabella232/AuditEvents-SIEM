// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMapCEF.PlatformRegistry.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    using Apprenda.SaaSGrid.Extensions.DTO;
    using Newtonsoft.Json;
    using SyslogNet.Client;

    /// <summary>
    /// This partial provides the mappings for the Registry Service component of the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Adds the mappings for the Registry Service component of the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureRegistryService()
        {
            AddDefaultMapCef("Deleting Registry Setting Value Completed", "PR2");
            AddDefaultMapCef("Deleting Registry Setting Value Failed", "PR2");
            AddMap("Setting Registry Setting Value Completed", RegistrySetValueDetailFormatter);
            AddMap("Setting Registry Setting Value Failed", RegistrySetValueDetailFormatter);
        }

        private SyslogMessage RegistrySetValueDetailFormatter(AuditedEventDTO auditedEvent)
        {
            if (auditedEvent == null)
            {
                return null;
            }

            var details = JsonConvert.DeserializeObject<DetailsObject>(auditedEvent.Details);

            var detail = $"cs1={details.OriginalValue.StripNewLines()} cs2={details.NewValue.StripNewLines()}";
            var message = $"CEF:0|Apprenda|CloudPlatform|{PlatformVersion}|-|{auditedEvent.Operation}|PR1|outcome={auditedEvent.EventTypeDescription()} {detail}";

            return auditedEvent.ToSyslogMessage(message);
        }
    }
}