// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.CustomPropertyManager.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial provides the mappings of Operations in the CustomPropertyService component of the platform.
    /// </summary>
    public partial class Apprenda81CallsiteMap
    {
        /// <summary>
        /// Adds the mappings of Operations in the CustomPropertyService component of the platform.
        /// </summary>
        private void ConfigureCustomPropertyService()
        {
            AddDefaultMap("Custom Property Modification");
            AddDefaultMap("Custom Property Addition");
            AddDefaultMap("Custom Property Removal");
        }
    }
}