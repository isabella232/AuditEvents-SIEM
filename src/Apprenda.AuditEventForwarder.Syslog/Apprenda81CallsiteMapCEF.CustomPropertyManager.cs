// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMapCEF.CustomPropertyManager.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial provides the mappings of Operations in the CustomPropertyService component of the platform.
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Adds the mappings of Operations in the CustomPropertyService component of the platform.
        /// </summary>
        private void ConfigureCustomPropertyService()
        {
            AddDefaultMapCef("Custom Property Modification", "CP1");
            AddDefaultMapCef("Custom Property Addition", "CP2");
            AddDefaultMapCef("Custom Property Removal", "CP3");
        }
    }
}