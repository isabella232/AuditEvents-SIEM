// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.PlatformRegistry.cs" company="Apprenda, Inc.">
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
    public partial class Apprenda81CallsiteMap
    {
        /// <summary>
        /// Adds the mappings for the Registry Service component of the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureRegistryService()
        {
            AddMap("Setting Registry Setting Value Failed", ValueUpdateFormatter);
            AddMap("Setting Registry Setting Value Completed", ValueUpdateFormatter);
            AddDefaultMap("Deleting Registry Setting Value Completed");
            AddDefaultMap("Deleting Registry Setting Value Failed");
        }

        private SyslogMessage ValueUpdateFormatter(AuditedEventDTO auditedEvent)
        {
            if (auditedEvent == null)
            {
                return null;
            }

            var details = JsonConvert.DeserializeObject<DetailsObject>(auditedEvent.Details);

            var message = $"{auditedEvent.Operation} Change from {details.OriginalValue.StripNewLines()} to {details.NewValue.StripNewLines()}";

            return FromEventDTO(auditedEvent, message);
        }
    }
}