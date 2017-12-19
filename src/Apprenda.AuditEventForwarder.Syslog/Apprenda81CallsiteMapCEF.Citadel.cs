// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.Citadel.cs" company="Apprenda, Inc.">
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
    /// This partial configures the Citadel Service callsite mappings.
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Formatter to handle Login Failure events.
        /// </summary>
        /// <param name="auditedEvent">The audited event</param>
        /// <returns>SyslogMessage representing the event.</returns>
        private SyslogMessage LoginFailureFormatter(AuditedEventDTO auditedEvent)
        {
            var loginDetails = FormatLoginFailureDto(auditedEvent.Details);
            var message =
                $"CEF:0|Apprenda|CloudPlatform|{PlatformVersion}|-|{auditedEvent.Operation}|CIT3|outcome={auditedEvent.EventTypeDescription()} {loginDetails}";
            return auditedEvent.ToSyslogMessage(Facility.SecurityOrAuthorizationMessages1, Severity.Notice, message);
        }

        /// <summary>
        /// Format a JSON body containing a platform Login Failure DTO as a meaninful message.
        /// </summary>
        /// <param name="json">The json serialized DTO</param>
        /// <returns>Meaningful text representation of the DTO</returns>
        private static string FormatLoginFailureDto(string json)
        {
            var jsonDetails = JsonConvert.DeserializeObject<DetailsObject>(json);
            var d = JsonConvert.DeserializeObject<LoginFailureDTO>(jsonDetails.Details);
            return
                $"User {d.Identifier} Reason {d.Reason} Attempts: {d.FailedLoginAttempts}. Was Locked out: {d.WasLockedOut} Locked out: {d.IsLockedOut} ";
        }

        /// <summary>
        /// Adds the Operation formatters for operations emitted by the Citadel platform service.
        /// </summary>
        private void ConfigureCitadel()
        {
            AddDefaultMapCef("Tenant Administrator User Creation", "CIT1");
            AddActionMapCef("Tenant Creation", "CIT2");

            AddMap("Login Failure", LoginFailureFormatter); // CIT3
            AddDefaultMapCef("Reset User Password", "CIT4"); // todo
            AddDefaultMapCef("Platform User Addition", "CIT5");
            AddDefaultMapCef("Unauthorized Application Access", "CIT6");
            AddDefaultMapCef("User Password Reset Failed", "CIT7");
            AddMappedMapCef(new[] { "Platform User Addition to Tenant Completed" }, "Tenant Add Platform User", "CIT8");
            AddMappedMapCef("User Password Rest Completed", "User Password Reset", "CIT9");
            AddMappedMapCef(new[] { "Platform User Removal from Tenant Complete" }, "Tenant Remove Platform User", "CIT9");
        }
    }
}