using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.Text;
using Apprenda.SaaSGrid.Extensions;
using Apprenda.SaaSGrid.Extensions.DTO;
using Apprenda.Services.Logging;
using SyslogNet.Client;
using SyslogNet.Client.Serialization;
using SyslogNet.Client.Transport;

namespace Apprenda.AuditEventForwarder.Syslog
{
    using AuditMapFunc = Func<AuditedEventDTO, SyslogMessage>;
    public class SyslogAuditForwarder : AuditEventForwardingExtensionBase
    {
        private readonly IExtensionConfiguration _config;

        private readonly ISyslogMessageSerializer _serializer;

        private readonly BaseAuditCallsiteMap _mapper;
        private bool _killswitch = false;


        public SyslogAuditForwarder() : this(new AddOnExtensionConfiguration())
        {
        }

        public SyslogAuditForwarder(IExtensionConfiguration config)
        {
            _config = config;
            if (_config.Host == null)
            {
                _killswitch = true;
                return;
            }
            _serializer = CreateSerializer();
            _mapper = config.AuditEventMap as BaseAuditCallsiteMap ?? CreateMapper();
        }

        private BaseAuditCallsiteMap CreateMapper()
        {
            return  new Apprenda81CallsiteMap(); // when new callsite mapping is needed, this becomes an version-based factory.
        }

        private ISyslogMessageSender CreateSender()
        {
            switch (_config.Protocol)
            {
                case SyslogProtocol.Udp:
                    return new SyslogUdpSender(_config.Host, _config.Port);
                case SyslogProtocol.EncryptedTcp:
                    return new SyslogEncryptedTcpSender(_config.Host, _config.Port,
                        ignoreChainErrors: true);
                case SyslogProtocol.Tcp:
                default:
                    return new SyslogTcpSender(_config.Host, _config.Port);
            }
        }

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

        public override void OnAuditedEventReceived(AuditedEventDTO message)
        {
            if (_killswitch) return;
            // use new connections to reduce connection management overhead. If perf of TCP connection establishment becomes an issue, switch to an
            // instance of the sender and do connection re-establishment as needed.
            using (var sender = CreateSender())
            {
                AuditMapFunc map;
                if (!_mapper.Maps.TryGetValue(message.Operation, out map))
                {
                    map = _mapper.DefaultActionResultMapper;
                }

                var syslogM = map(message);

                sender.Send(syslogM, _serializer);

            }
        }
    }

}

