// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.TenantPage.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial provides the mappings for the operations in the Tenant Page component of the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMap
    {
        /// <summary>
        /// Adds the mappings for the operations in the Tenant Page component of the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureTenantPage()
        {
            AddDefaultMap("Set Session Duration");
        }
    }
}