// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.DevPortalControllers.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial provides the mappings for the Developer Portal Controller operations in the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMap
    {
        /// <summary>
        /// Adds the mappings for the Developer Portal Controller operations in the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureDeveloperPortalControllers()
        {
            AddActionMap("Provision Add-on");
            AddActionMap("De-Provision Add-on");
            AddActionMap("Add Groups to Application", "Application Add Groups");
            AddActionMap("Remove Groups from Application", "Application Remove Groups");
            AddActionMap("Start Version");
            AddActionMap("Stop Version");
            AddActionMap("Add Users to Application", "Application Add Users");
            AddActionMap("Remove Users from Application", "Application Remove Users");
            AddActionMap("Delete Logs");
        }
    }
}