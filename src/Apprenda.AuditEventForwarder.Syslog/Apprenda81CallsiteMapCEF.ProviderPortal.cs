// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMapCEF.ProviderPortal.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    ///  This partial provides the mappings for the Provider Portal component of the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Adds the mappings for the operations in the User Store Plugin Handler component of the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureProviderPortalService()
        {
            AddDefaultMapCef("Application Creation", "PROV1");
            AddMappedMapCef(new[] { "Application Deletion Started", "Application Deletion Completed" }, "Application Delete", "PROV2");
            AddDefaultMapCef("Version Creation", "PROV3");
            AddDefaultMapCef("Version Deletion", "PROV4");
            AddMappedMapCef("Updating Version Binaries", "Version Binaries Update", "PROV5");
            AddMappedMapCef(
                new[]
                {
                    "Persistence Partition Relocation Failed",
                    "Persistence Partition Relocation Started",
                    "Persistence Partition Relocation Completed",
                    "Persistence Partition Relocation",
                }, "Relocate Persistence Partition",
                "PROV6");
            AddDefaultMapCef("Version Demotion", "PROV7");
            AddDefaultMapCef("Cancel Promotion", "PROV8");
            AddMap("Removing Vanity URL Certificate", DefaultCefActionResultFormatter("PROV9"));
            AddMap("Updating Vanity URL Certificate", DefaultCefActionResultFormatter("PROV10"));
        }
    }
}