// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMapCEF.UserManager.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial provides the mappings for the operations in the UserManager component of the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Add the UserManager Audit Events to the Callsite Map.
        /// </summary>
        private void ConfigureUserManager()
        {
            AddMap("User Removal", DefaultCefActionResultFormatter("UM1"));
        }
    }
}