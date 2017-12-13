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
    public partial class Apprenda81CallsiteMap
    {
        private static SyslogMessage LoginFailureMapper(AuditedEventDTO evt)
        {
            var loginDetails = FormatLoginFailureDto(evt.Details);
            return FromEventDTO(evt, Facility.SecurityOrAuthorizationMessages1, Severity.Notice, $"{evt.Operation} {loginDetails}");
        }

        private static string FormatLoginFailureDto(string json)
        {
            var jsonDetails = JsonConvert.DeserializeObject<DetailsObject>(json);
            var d = JsonConvert.DeserializeObject<LoginFailureDTO>(jsonDetails.Details);
            return
                $"User {d.Identifier} Reason {d.Reason} Attempts: {d.FailedLoginAttempts}. Was Locked out: {d.WasLockedOut} Locked out: {d.IsLockedOut} ";
        }

        private void ConfigureCitadel()
        {
            AddDefaultMap("Tenant Administrator User Creation");
            AddActionMap("Tenant Creation");

            AddMap("Login Failure", LoginFailureMapper);
            AddDefaultMap("Reset User Password"); // todo
            AddDefaultMap("Platform User Addition");
            AddDefaultMap("Unauthorized Application Access");
            AddDefaultMap("User Password Reset Failed");
            AddMappedMap(new[] { "Platform User Addition to Tenant Completed" }, "Tenant Add Platform User");
            AddMappedMap("User Password Rest Completed", "User Password Reset");
            AddMappedMap(new[] { "Platform User Removal from Tenant Complete" }, "Tenant Remove Platform User");
        }
    }
}