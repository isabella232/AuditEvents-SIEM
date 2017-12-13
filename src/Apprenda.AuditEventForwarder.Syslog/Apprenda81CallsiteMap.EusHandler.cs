// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.EusHandler.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial provides the mappings for the User Store Plugin Handler component of the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMap
    {
        /// <summary>
        /// Adds the mappings for the User Store Plugin Handler component of the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureUserStorePluginHandler()
        {
            AddDefaultMap("EUS Plugin Addition");
            AddDefaultMap("EUS Plugin Modification");
        }
    }
}