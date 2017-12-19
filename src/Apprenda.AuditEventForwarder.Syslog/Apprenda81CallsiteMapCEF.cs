// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <inheritdoc />
    /// <summary>
    /// Provides the Apprenda Audit Events Callsite Map for the Apprenda Cloud Platform version 8.1
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF : BaseAuditCallsiteMapCef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Apprenda81CallsiteMap"/> class.
        /// </summary>
        public Apprenda81CallsiteMapCEF()
            : base("8.1")
        {
            ConfigureRegistryService();
            ConfigureCustomPropertyService();
            ConfigureTenantPortalService();
            ConfigureUserManager();
            ConfigureTenantPage();
            ConfigureCustomUrlCertificateManager();
            ConfigureProviderPortalService();
            ConfigurePromotionTasks();
            ConfigureDeveloperPortalControllers();
            ConfigureApprendaCatalog();
            ConfigureUserStorePluginHandler();
            ConfigureSocControllers();
            ConfigureCitadel();
            ConfigureUserRepository();
            ConfigureAuthenticationWorkflows();
            ConfgureLoggingService();
        }
    }
}