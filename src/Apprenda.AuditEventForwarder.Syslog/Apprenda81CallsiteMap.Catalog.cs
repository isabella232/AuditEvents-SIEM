// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.Catalog.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// Provide the Audit Events Callsite Map for the Apprenda Catalog component.
    /// </summary>
    public partial class Apprenda81CallsiteMap
    {
        /// <summary>
        /// Add the mapped audited events.
        /// </summary>
        private void ConfigureApprendaCatalog()
        {
            AddActionMap("Custom Property Model Creation");
            AddActionMap("Custom Property Model Modification");
            AddActionMap("Custom Property Model Deletion");
            AddActionMap("Deployment Policy Creation");
            AddActionMap("Deployment Policy Modification");
            AddActionMap("Deployment Policy Deletion");
            AddMappedMap(new[] { "Add-on Update Completed", "Add-on Update Failed" }, "Add-on Update");
        }
    }
}