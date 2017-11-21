using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Apprenda.SaaSGrid.Addons;
using Newtonsoft.Json;
using SyslogNet.Client;
using SyslogNet.Client.Serialization;
using SyslogNet.Client.Transport;

namespace Apprenda.AddOns.Syslog
{
    public class SyslogConfigurationAddon: AddonBase

    {
        private OperationResult FaultResult(string endUserMessage)
        {
            return new OperationResult { EndUserMessage = endUserMessage, IsSuccess = false };
        }
        private OperationResult SuccessResult(string endUserMessage)
        {
            return new OperationResult { EndUserMessage = endUserMessage, IsSuccess = true };
        }

        public override ProvisionAddOnResult Provision(AddonProvisionRequest request)
        {
            var host = request.Manifest.Properties.First(p => p.Key.Equals("Host")).Value;
            var port = request.Manifest.Properties.First(p => p.Key.Equals("Port")).Value;
            var protocol = request.Manifest.Properties.First(p => p.Key.Equals("Protocol")).Value;
            var mapType = request.Manifest.Properties.First(p => p.Key.Equals("MapType")).Value;
            var flavor = request.Manifest.Properties.First(p => p.Key.Equals("Flavor")).Value;
            var result = JsonConvert.SerializeObject(new { protocol, host, port, mapType, flavor }).Replace('"', '\'');

            return new ProvisionAddOnResult(result);
        }

        public override OperationResult Deprovision(AddonDeprovisionRequest request)
        {
            return SuccessResult("The HostPortProtocol addon was deprovisioned successfully.");
        }

        public override OperationResult Test(AddonTestRequest request)
        {
            var host = request.Manifest.Properties.First(p => p.Key.Equals("Host")).Value;
            var portValue = request.Manifest.Properties.First(p => p.Key.Equals("Port")).Value;
            var protocol = request.Manifest.Properties.First(p => p.Key.Equals("Protocol")).Value;
            var mapType = request.Manifest.Properties.First(p => p.Key.Equals("MapType")).Value;
            var flavor = request.Manifest.Properties.First(p => p.Key.Equals("Flavor")).Value;

            IPAddress asIp;
            if (!IPAddress.TryParse(host, out asIp))
            {
                var hostEntry = Dns.GetHostEntry(host);
                if (hostEntry.AddressList.Any())
                {
                    asIp = hostEntry.AddressList.First();
                }
                else
                {
                    return FaultResult($"Could not resolve {host} as an IP address or hostname.");
                }
            }

            int port;
            if (!int.TryParse(portValue, out port))
            {
                return FaultResult("Could not parse {portValue} as a portnumber.");
            }

            switch (protocol.ToLowerInvariant())
            {
                case "tcp":
                    try
                    {
                        using (var client = new TcpClient())
                        {
                            client.Connect(asIp, port);
                            client.Close();
                        }
                    }
                    catch
                    {
                        return FaultResult($"There was a failure connecting to tcp endpoint {host}:{port}.");
                    }

                    break;
                case "udp":

                    try
                    {
                        using (var client = new UdpClient())
                        {
                            client.Connect(asIp, port);
                            client.Close();
                        }
                    }
                    catch
                    {
                        return FaultResult($"There was a failure connecting to udp endpoint {host}:{port}.");

                    }
                    break;

                default:
                    return FaultResult($"Unsupported protocol {protocol}");
            }

            using (var sender = CreateSender(protocol, host, port))
            {
                var serializer = CreateSerializer(flavor);
                var message = new SyslogMessage(DateTimeOffset.Now, Facility.LogAudit, Severity.Informational, Environment.MachineName, "Apprenda.AddOns.Syslog", "-", "TEST","Connection test message", new StructuredDataElement[0]);
                sender.Send(message, serializer);
            }
            return SuccessResult("The Host Port Protocol addon connected successfully.");
        }


        private ISyslogMessageSender CreateSender(string protocol, string host, int port)
        {
            switch (protocol)
            {
                case "udp":
                    return new SyslogUdpSender(host, port);
                case "encryptedTcp":
                    return new SyslogEncryptedTcpSender(host, port,
                        ignoreChainErrors: true);
                case "tcp":
                default:
                    return new SyslogTcpSender(host, port);
            }
        }

        private ISyslogMessageSerializer CreateSerializer(string flavor)
        {
            switch (flavor)
            {
                case "Rfc3164":
                    return new SyslogRfc3164MessageSerializer();
                case "Rfc5424":
                case "Cef":
                default: return new SyslogRfc5424MessageSerializer();
            }
        }
    }
}
