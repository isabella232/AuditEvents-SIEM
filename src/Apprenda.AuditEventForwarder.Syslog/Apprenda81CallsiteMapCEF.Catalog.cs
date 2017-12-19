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
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Add the mapped audited events.
        /// </summary>
        private void ConfigureApprendaCatalog()
        {
            AddActionMapCef("Custom Property Model Creation", "CAT1");
            AddActionMapCef("Custom Property Model Modification", "CAT2");
            AddActionMapCef("Custom Property Model Deletion", "CAT3");
            AddActionMapCef("Deployment Policy Creation", "CAT4");
            AddActionMapCef("Deployment Policy Modification", "CAT5");
            AddActionMapCef("Deployment Policy Deletion", "CAT6");
            AddMappedMapCef(new[] { "Add-on Update Completed", "Add-on Update Failed" }, "Add-on Update", "CAT7");
        }
    }
}