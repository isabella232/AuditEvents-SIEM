// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMapCEF.DevPortalControllers.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial provides the mappings for the Developer Portal Controller operations in the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Adds the mappings for the Developer Portal Controller operations in the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureDeveloperPortalControllers()
        {
            AddActionMapCef("Provision Add-on", "DPA1");
            AddActionMapCef("De-Provision Add-on", "DPA2");
            AddActionMapCef("Add Groups to Application", "Application Add Groups", "DPA3");
            AddActionMapCef("Remove Groups from Application", "Application Remove Groups", "DPA4");
            AddActionMapCef("Start Version", "DPA5");
            AddActionMapCef("Stop Version", "DPA6");
            AddActionMapCef("Add Users to Application", "Application Add Users", "DPA7");
            AddActionMapCef("Remove Users from Application", "Application Remove Users", "DPA8");
            AddActionMapCef("Delete Logs", "DPA9");
        }
    }
}