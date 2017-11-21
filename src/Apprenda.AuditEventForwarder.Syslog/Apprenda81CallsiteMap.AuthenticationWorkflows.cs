using System;
using Apprenda.SaaSGrid.Extensions.DTO;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigureAuthenticationWorkflows()
        {
            AddDefaultMap("Tenant Removal Started");
            AddDefaultMap("Tenant Removal Completed");
            AddDefaultMap("Tenant Removal Failed");
        }
    }
}