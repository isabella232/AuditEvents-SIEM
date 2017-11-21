using System;
using Apprenda.SaaSGrid.Extensions.DTO;
using Newtonsoft.Json;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    using AuditMapFunc = Func<AuditedEventDTO, SyslogMessage>;

    public partial class Apprenda81CallsiteMap : BaseAuditCallsiteMap
    {
        public Apprenda81CallsiteMap() : base("8.1")
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