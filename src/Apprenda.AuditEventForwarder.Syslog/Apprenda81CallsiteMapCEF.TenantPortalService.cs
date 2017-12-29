// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMapCEF.TenantPortalService.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// Provides the Operation to Formatter mappings for the Tenant Portal Service component of the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Adds the Operation to Formatter mappings for the Tenant Portal Service component of the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureTenantPortalService()
        {
            AddDefaultMapCef("User Addition", "TP1");
            AddDefaultMapCef("Add Contact Section", "TP2");
            AddDefaultMapCef("Remove Contact Section", "TP3");
            AddDefaultMapCef("Update Contact Section", "TP4");
            AddDefaultMapCef("Add Contact Detail", "TP5");
            AddDefaultMapCef("Remove Contact Detail", "TP6");
            AddDefaultMapCef("Save Company Profile", "TP7");
            AddDefaultMapCef("Set Company Primary Location", "TP8");
            AddDefaultMapCef("Assign Tenant Administrator", "TP9");
            AddMappedMapCef(
                new[] { "Addition of a Role to Tenant Completed", "Addition of a Role to Tenant Failed" },
                "Tenant Role Addition",
                "TP10");

            AddMappedMapCef("Remove Role from Tenant", "Tenant Role Removal", "TP11");
            AddMappedMapCef(
                new[] { "Addition of a User to Role Completed", "Addition of a User to Role Failed" },
                "Role User Addition",
                "TP12");
            AddMappedMapCef(
                new[] { "Addition of a Role to Role Completed", "Addition of a Role to Role Failed" },
                "Role Role Addition",
                "TP13");
            AddMappedMapCef(
                new[] { "Removal of a User from a Role Completed", "Removal of a User from a Role Failed" },
                "Role User Removal",
                "TP14");
            AddMappedMapCef(
                new[] { "Removal of Members from a Role Completed", "Removal of Members from a Role Failed" },
                "Role Multiple User Removal",
                "TP15");
            AddMappedMapCef(
                new[] { "Addition of a Securable to Role Completed", "Addition of a Securable to Role Failed" },
                "Role Securable Addition",
                "TP16");
            AddMappedMapCef(new[] { "Remove Securable from Role.", "Remove Securable from Role" }, "Role Securable Removal", "TP17");
            AddMappedMapCef(
                new[]
                {
                    "Subscription Assignment to User Completed", "Subscription Assignment to User Failed",
                    "Subscription Assignment to User",
                }, "User Subscription Assignment",
                "TP18");
            AddMappedMapCef(
                new[]
                {
                    "Subscription Removal from User Completed", "Subscription Removal from User Failed",
                    "Removal of a Subscription from User Failed",
                }, "User Subscription Removal",
                "TP20");
            AddMappedMapCef(
                new[] { "Subscription Creation", "Subscription Creation Completed", "Subscription Creation Failed" },
                "Subscription Creation",
                "TP22");
            AddMappedMapCef(
                new[] { "Subscription Cancellation Completed", "Subscription Cancellation Failed" },
                "Subscription Cancellation",
                "TP23");
        }
    }
}