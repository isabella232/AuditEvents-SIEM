// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.ProviderPortal.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    ///  This partial provides the mappings for the Provider Portal component of the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMap
    {
        /// <summary>
        /// Adds the mappings for the operations in the User Store Plugin Handler component of the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureProviderPortalService()
        {
            AddDefaultMap("Application Creation");
            AddMappedMap(new[] { "Application Deletion Started", "Application Deletion Completed" }, "Application Delete");
            AddDefaultMap("Version Creation");
            AddDefaultMap("Version Deletion");
            AddMappedMap("Updating Version Binaries", "Version Binaries Update");
            AddMappedMap(
                new[]
                {
                    "Persistence Partition Relocation Failed", "Persistence Partition Relocation Started",
                    "Persistence Partition Relocation Completed", "Persistence Partition Relocation",
                }, "Relocate Persistence Partition");
            AddDefaultMap("Version Demotion");
            AddDefaultMap("Cancel Promotion");
            AddMap("Removing Vanity URL Certificate", DefaultActionResultMapper);
            AddMap("Updating Vanity URL Certificate", DefaultActionResultMapper);
        }
    }
}