using Apprenda.SaaSGrid.Extensions.DTO;
using Newtonsoft.Json;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigurePromotionTasks()
        {
            AddMap("Version Promotion", VersionPromotionMapper);
        }
        SyslogMessage VersionPromotionMapper(AuditedEventDTO evt)
        {
            var severity = Severity.Informational;
            string detail;
            if (evt.EventType == AuditEventType.OperationFailed)
            {
                severity = Severity.Warning;
                detail = $"- {evt.Details}";
            }
            else
            {
                var av = JsonConvert.DeserializeObject<ApplicationVersionDTO>(evt.Details);
                detail = $"{av.Name} ({av.Alias}) {av.Stage}";
            }

            var message = $"{evt.Operation} {evt.EventTypeDescription()} {detail}";

            return new SyslogMessage(
                evt.Timestamp, 
                Facility.UserLevelMessages, 
                severity, 
                evt.SourceIP, 
                "ApprendaCloudPlatform", 
                message: message, procId: "-", structuredDataElements: new StructuredDataElement[0], msgId: "-");
        }
    }
}