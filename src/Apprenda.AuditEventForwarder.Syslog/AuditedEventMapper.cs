using System;
using Apprenda.SaaSGrid.Extensions.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    using AuditMapFunc = Func<AuditedEventDTO, SyslogMessage>;

    public class AuditedEventMapper
    {
        public SyslogMessage ToSyslogMessage(AuditedEventDTO source)
        {
            var details = JsonConvert.DeserializeObject<DetailsObject>(source.Details);
            var joDetail = JsonConvert.DeserializeObject<JObject>(details.Details);

            var detailDetail = JsonConvert.SerializeObject(joDetail, Formatting.None);

            return new SyslogMessage(
                dateTimeOffset: source.Timestamp,
                facility: Facility.LogAudit,
                severity: Severity.Informational,
                hostName: source.SourceIP,
                appName: "ApprendaCloudPlatform",
                message: detailDetail,
                procId: "-",
                structuredDataElements: new StructuredDataElement[]
                {
                },
                msgId: "-"
            );
        }
        public AuditedEventMapper(IAuditCallsiteMap map)
        {

        }

    }
}