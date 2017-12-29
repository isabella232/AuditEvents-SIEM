// ----------------------------------------------------------------------------------------------------
// <copyright file="SyslogAuditForwarder.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    using System;
    using Apprenda.SaaSGrid.Extensions;
    using Apprenda.SaaSGrid.Extensions.DTO;
    using SyslogNet.Client.Serialization;
    using SyslogNet.Client.Transport;
    using AuditMapFunc = System.Func<Apprenda.SaaSGrid.Extensions.DTO.AuditedEventDTO, SyslogNet.Client.SyslogMessage>;

    /// <summary>
    /// Apprenda Platform Extension for forwarding Auditing Events to a Syslog receiver.
    /// </summary>
    public class SyslogAuditForwarder : AuditEventForwardingExtensionBase
    {
        private readonly IExtensionConfiguration _config;

        private readonly ISyslogMessageSerializer _serializer;

        private readonly IAuditCallsiteMap _mapper;
        private bool _killswitch = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyslogAuditForwarder"/> class, using the AddOn configuration provider.
        /// </summary>
        public SyslogAuditForwarder()
            : this(new AddOnExtensionConfiguration())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyslogAuditForwarder"/> class.
        /// Creates a new instance of the SyslogAuditForwarder
        /// </summary>
        /// <param name="config">The </param>
        public SyslogAuditForwarder(IExtensionConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config), "A constructed configuration must be provided.");
            if (_config.Host == null)
            {
                _killswitch = true;
                return;
            }

            _serializer = CreateSerializer();
            _mapper = config.AuditEventMap;
        }

        /// <summary>
        /// Forward received AuditedEventDTO to forward them to the configured Syslog receiver.
        /// </summary>
        /// <param name="message">The audited event details</param>
        public override void OnAuditedEventReceived(AuditedEventDTO message)
        {
            if (message == null)
            {
                return;
            }

            if (_killswitch)
            {
                return;
            }

            // use new connections to reduce connection management overhead. If perf of TCP connection establishment becomes an issue, switch to an
            // instance of the sender and do connection re-establishment as needed.
            using (var sender = CreateSender())
            {
                AuditMapFunc map;
                if (!_mapper.Formatters.TryGetValue(message.Operation, out map))
                {
                    map = BaseAuditCallsiteMap.DefaultActionResultMapper;
                }

                var syslogM = map(message);

                sender.Send(syslogM, _serializer);
            }
        }

        /// <summary>
        /// Factory method which constructs the appropriate ISyslogMessageSender for the configured Syslog Protocol.
        /// </summary>
        /// <returns>A configured Syslog sender for the host, port, and protocol configured.</returns>
        private ISyslogMessageSender CreateSender()
        {
            switch (_config.Protocol)
            {
                case SyslogProtocol.Udp:
                    return new SyslogUdpSender(_config.Host, _config.Port);
                case SyslogProtocol.EncryptedTcp:
                    return new SyslogEncryptedTcpSender(_config.Host, _config.Port, ignoreChainErrors: true);
                case SyslogProtocol.Tcp:
                default:
                    return new SyslogTcpSender(_config.Host, _config.Port);
            }
        }

        /// <summary>
        /// Factory method which constructs the appropriate Syslog Message Serializer for the configured Syslog receiver.
        /// </summary>
        /// <returns>The configured serializer.</returns>
        private ISyslogMessageSerializer CreateSerializer()
        {
            switch (_config.Flavor)
            {
                case SyslogFlavor.Rfc3164:
                    return new SyslogRfc3164MessageSerializer();
                case SyslogFlavor.Rfc5424:
                case SyslogFlavor.Cef:
                default: return new SyslogRfc5424MessageSerializer();
            }
        }
    }
}