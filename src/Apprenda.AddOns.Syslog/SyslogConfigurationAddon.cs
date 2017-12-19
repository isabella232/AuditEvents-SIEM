// ----------------------------------------------------------------------------------------------------
// <copyright file="SyslogConfigurationAddon.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AddOns.Syslog
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using Apprenda.SaaSGrid.Addons;
    using FluentAssertions;
    using Newtonsoft.Json;
    using SyslogNet.Client;
    using SyslogNet.Client.Serialization;
    using SyslogNet.Client.Transport;

    /// <summary>
    /// Provides the implementation of a Syslog endpoint addon, to allow platform operators to expose a known Syslog target to selected development teams
    /// for delivery of auditing, platform, or development team events using Apprenda Cloud Platform extensibility.
    /// </summary>
    public class SyslogConfigurationAddon : AddonBase
    {
        /// <summary>
        /// Provision the Syslog endpoint into a connection string, serialized as a JSON blob for portability.
        /// </summary>
        /// <param name="request">The provisioning request from the developer portal</param>
        /// <returns>A platform event result containing the connection details.</returns>
        public override ProvisionAddOnResult Provision(AddonProvisionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "The Syslog Addon received a provisioning call without a valid Request");
            }

            var host = request.Manifest.Properties.First(p => p.Key.Equals("Host")).Value;
            var port = request.Manifest.Properties.First(p => p.Key.Equals("Port")).Value;
            var protocol = request.Manifest.Properties.First(p => p.Key.Equals("Protocol")).Value;
            var mapType = request.Manifest.Properties.First(p => p.Key.Equals("MapType")).Value;
            var flavor = request.Manifest.Properties.First(p => p.Key.Equals("Flavor")).Value;
            var result = JsonConvert.SerializeObject(new { protocol, host, port, mapType, flavor }).Replace('"', '\'');

            return new ProvisionAddOnResult(result);
        }

        /// <summary>
        /// Deprovision the Syslog endpoint - which is a null operation, as such endpoints are external resources which are not allocated per-instance.
        /// </summary>
        /// <param name="request">The deprovisioning request details from the developer portal</param>
        /// <returns>Always succeeds - null operation</returns>
        public override OperationResult Deprovision(AddonDeprovisionRequest request)
        {
            return SuccessResult("The Syslog addon was deprovisioned successfully.");
        }

        /// <summary>
        /// Verify the ability to connect to the configured Syslog endpoint, and send a test message to that Syslog service.
        /// </summary>
        /// <param name="request">The platform test request details</param>
        /// <returns>Platform extensibility operation result indicating the test outcome.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification="The connection fault details can be hidden in the test result message, the underlying exception is still propagated to the system logs.")]
        public override OperationResult Test(AddonTestRequest request)
        {
            if (request == null)
            {
                return FaultResult("The Syslog addon received a Test call without a valid Request");
            }

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

            switch (protocol.ToUpperInvariant())
            {
                case "TCP":
                    try
                    {
                        TestTcpConnection(asIp, port);
                    }
                    catch
                    {
                        return FaultResult($"There was a failure connecting to tcp endpoint {host}:{port}.");
                    }

                    break;
                case "UDP":

                    try
                    {
                        TestUdpConnection(asIp, port);
                    }
                    catch
                    {
                        return FaultResult($"There was a failure connecting to udp endpoint {host}:{port}.");
                    }

                    break;
                case "ENCRYPTEDTCP":
                    // TODO: implement encrypted TCP test connection.
                    break;
                default:
                    return FaultResult($"Unsupported protocol {protocol}");
            }

            using (var sender = CreateSender(protocol, host, port))
            {
                var serializer = CreateSerializer(flavor);

                // ReSharper disable once RedundantExplicitParamsArrayCreation - SyslogNet does not handle the params array properly during serialization without explicitly creating the empty array.
                var message = new SyslogMessage(DateTimeOffset.Now, Facility.LogAudit, Severity.Informational, Environment.MachineName, "Apprenda.AddOns.Syslog", "-", "TEST", "Connection test message", new StructuredDataElement[0]);
                sender.Send(message, serializer);
            }

            mapType.ToUpperInvariant().Should().BeOneOf("DEFAULT", "8.1", "8.1CEF");

            return SuccessResult("The Syslog addon connected successfully.");
        }

        /// <summary>
        /// Test the ability to create a UDP connection to the host and port provided.
        /// </summary>
        /// <param name="asIp">Target host</param>
        /// <param name="port">Target port</param>
        private static void TestUdpConnection(IPAddress asIp, int port)
        {
            using (var client = new UdpClient())
            {
                client.Connect(asIp, port);
            }
        }

        /// <summary>
        /// Test the ability to create a TCP connection to the host and port provided.
        /// </summary>
        /// <param name="asIp">Target host</param>
        /// <param name="port">Target port</param>
        private static void TestTcpConnection(IPAddress asIp, int port)
        {
            using (var client = new TcpClient())
            {
                client.Connect(asIp, port);
            }
        }

        /// <summary>
        /// Creates a platform OperationResult indicating a fault.
        /// </summary>
        /// <param name="endUserMessage">Message to encapsulate in the OperationResult</param>
        /// <returns>The created OperationResult</returns>
        private static OperationResult FaultResult(string endUserMessage)
        {
            return new OperationResult { EndUserMessage = endUserMessage, IsSuccess = false };
        }

        /// <summary>
        /// Create a platform OperationResult indicating success.
        /// </summary>
        /// <param name="endUserMessage">Message to encapsulate in the OperationResult</param>
        /// <returns>The created OperationResult</returns>
        private static OperationResult SuccessResult(string endUserMessage)
        {
            return new OperationResult { EndUserMessage = endUserMessage, IsSuccess = true };
        }

        /// <summary>
        /// Constructs a SyslogNet transport component for the configured host, port, and transport protocol.
        /// </summary>
        /// <param name="protocol">Selected transport protocol: udp, tcp, or encryptedTcp (TLS)</param>
        /// <param name="host">Hostname</param>
        /// <param name="port">Port</param>
        /// <returns>A configured ISyslogMessageSender</returns>
        private static ISyslogMessageSender CreateSender(string protocol, string host, int port)
        {
            switch (protocol)
            {
                case "udp":
                    return new SyslogUdpSender(host, port);
                case "encryptedTcp":
                    return new SyslogEncryptedTcpSender(host, port, ignoreChainErrors: true);
                case "tcp":
                default:
                    return new SyslogTcpSender(host, port);
            }
        }

        /// <summary>
        /// Construct a SyslogNet message serializer for the selected Syslog receiver's expected message flavor.
        /// </summary>
        /// <param name="flavor">The Syslog flavor the receiver consumes: Rfc3164, Rfc5424, or Cef (RFC5424 with Common Event Format enhancements)</param>
        /// <returns>The constructed serializer</returns>
        private static ISyslogMessageSerializer CreateSerializer(string flavor)
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
