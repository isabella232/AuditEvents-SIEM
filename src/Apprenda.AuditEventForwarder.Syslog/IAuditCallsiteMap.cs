using System;
using System.Collections.Generic;
using Apprenda.SaaSGrid.Extensions.DTO;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    public interface IAuditCallsiteMap
    {
        Dictionary<string, Func<AuditedEventDTO, SyslogMessage>> Maps { get; }
        void  AddMap(string operation, Func<AuditedEventDTO, SyslogMessage> mapper);
    }
}