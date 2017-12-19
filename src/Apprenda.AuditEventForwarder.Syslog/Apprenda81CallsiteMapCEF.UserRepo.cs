// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.UserRepo.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial provides the mappings for the operations in the User Repository component of the Apprenda Cloud Platform
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Adds the mappings for the operations in the User Repository component of the Apprenda Cloud Platform
        /// </summary>
        private void ConfigureUserRepository()
        {
            AddDefaultMapCef("Account Lockout Reset", "UR1");
            AddDefaultMapCef("Account Lockout", "UR2");
        }
    }
}