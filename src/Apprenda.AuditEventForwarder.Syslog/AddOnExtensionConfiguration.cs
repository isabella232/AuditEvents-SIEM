using System;
using System.Configuration;
using System.Net;
using Apprenda.SaaSGrid;
using Apprenda.Services.Logging;
using Newtonsoft.Json;

namespace Apprenda.AuditEventForwarder.Syslog
{
    public class AddOnExtensionConfiguration : IExtensionConfiguration
    {
        private ConfigurationDto _configData;

        public AddOnExtensionConfiguration()
        {
            var serialized = ConfigurationManager.AppSettings["SyslogHostPortProtocol"];
            if (serialized.StartsWith("$#ADDON"))
            {
                LogManager.Instance().GetLogger(typeof(AddOnExtensionConfiguration))
                    .Error(
                        "The connection information was not injected into this workload (perhaps the Host-Port add-on has not been configured by platform operators)");
                _configData = new ConfigurationDto();
            }
            else
            {
                _configData = JsonConvert.DeserializeObject<ConfigurationDto>(serialized);
            }
        }

        public string Host => _configData.Host;
        public int Port => _configData.Port;
        public SyslogFlavor Flavor => _configData.Flavor;
        public SyslogProtocol Protocol => _configData.Protocol;

        private IAuditCallsiteMap _map;
        public IAuditCallsiteMap AuditEventMap => _map ?? (_map = ChooseMap(_configData.MapType));

        IAuditCallsiteMap ChooseMap(string mapType)
        {
            switch (mapType)
            {
                default: return new Apprenda81CallsiteMap();
            }
        }
    }

    public class ConfigurationDto
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public SyslogFlavor Flavor { get; set; }
        public SyslogProtocol Protocol { get; set; }

        public string MapType { get; set; }
    }
}