// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.CustomUrlCertificateManager.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// This partial provides the mappings of Operations in the CustomURL Certificate Manager component of the platform.
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Adds the mappings of Operations in the CustomURL Certificate Manager component of the platform.
        /// </summary>
        private void ConfigureCustomUrlCertificateManager()
        {
            AddMap("Updating Vanity Url Certificate", DefaultCefActionResultFormatter("CERT1"));
            AddMap("Removing Vanity Url Certificate", DefaultCefActionResultFormatter("CERT2"));
        }
    }
}