// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.AuthenticationWorkflows.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial configures the the Authentication Workflow event mappings
    /// </summary>
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigureAuthenticationWorkflows()
        {
            AddDefaultMap("Tenant Removal Started");
            AddDefaultMap("Tenant Removal Completed");
            AddDefaultMap("Tenant Removal Failed");
        }
    }
}