using System;
using Apprenda.SaaSGrid.Extensions.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    public static class AuditedEventDtoExtensions
    {
        public static SyslogMessage ToSyslogMessage(this AuditedEventDTO source)
        {
            var details = JsonConvert.DeserializeObject<DetailsObject>(source.Details);

            var detailDetail = details.Details.Replace(Environment.NewLine, string.Empty);

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

        public static string EventTypeDescription(this AuditedEventDTO source)
        {
            switch (source.EventType)
            {
                    case AuditEventType.OperationCompleted: return "Completed";
                    case AuditEventType.OperationFailed: return "Failed";
                    case AuditEventType.OperationStarting: return "Starting";
                default: return "Unknown event state";
            }
        }

    }
}