using System;
using System.Collections.Generic;
using System.Linq;
using Apprenda.SaaSGrid.Extensions.DTO;
using Newtonsoft.Json;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    using AuditMapFunc = Func<AuditedEventDTO, SyslogMessage>;
    public abstract class BaseAuditCallsiteMap : IAuditCallsiteMap
    {
        private string _platformVersion;

        public BaseAuditCallsiteMap(string platformVersion)
        {
            Maps = new Dictionary<string, AuditMapFunc>();
            _platformVersion = platformVersion;
        }
        public Dictionary<string, Func<AuditedEventDTO, SyslogMessage>> Maps { get; }
        public void AddMap(string operation, Func<AuditedEventDTO, SyslogMessage> mapper)
        {
            Maps.Add(operation, mapper);
        }

        public void AddDefaultMap(string operation)
        {
            AddMap(operation, DefaultOpResultMapper);
        }

        public static SyslogMessage FromEventDto(AuditedEventDTO evt, string message) => FromEventDto(evt, Facility.LogAudit, Severity.Informational, message);

        public static SyslogMessage FromEventDto(AuditedEventDTO evt, Facility facility, Severity severity, string message)
        {
            return new SyslogMessage(
                dateTimeOffset: evt.Timestamp,
                facility: facility,
                severity: severity,
                hostName: evt.SourceIP,
                appName: "ApprendaCloudPlatform",
                message: message,
                procId: "-",
                structuredDataElements: new StructuredDataElement[]
                {
                },
                msgId: "-"
            );

        }

        public static Tuple<string, string> LastWordOf(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                throw new ArgumentNullException(nameof(phrase));
            }
            var lastSpace = phrase.LastIndexOf(" ", StringComparison.Ordinal);

            return new Tuple<string, string>(phrase.Remove(lastSpace), lastSpace < phrase.Length ? phrase.Substring(lastSpace + 1) : string.Empty);
        }

        public SyslogMessage DefaultCefActionResultMapper(AuditedEventDTO evt)
        {
            var actionEvent = LastWordOf(evt.Operation);
            var operation = actionEvent.Item1;
            var eventType = actionEvent.Item2;
            var message = evt.Details == null
                ? string.Empty
                : $"msg={evt.Details.Replace(Environment.NewLine, string.Empty)}";
            return FromEventDto(evt, $"act={operation} outcome={eventType} {message}");
        }

        public SyslogMessage DefaultCefOpResultMapper(AuditedEventDTO evt, string cefEventId)
        {
            var operationName = LastWordOf(evt.Operation).Item1;
            var extension = $"msg={evt.Details.Replace(System.Environment.NewLine, string.Empty)}";

            return FromEventDto(evt, $"CEF:0|Apprenda|CloudPlatform|{_platformVersion}|{cefEventId}|{operationName}|Unknown|{extension}");
        }

        public SyslogMessage DefaultActionResultMapper(AuditedEventDTO evt) => FromEventDto(evt, evt.Operation);
        public SyslogMessage DefaultOpResultMapper(AuditedEventDTO evt) => FromEventDto(evt, Facility.LogAudit, Severity.Informational, $"{evt.Operation} {evt.EventTypeDescription()} {evt.Details}");

        protected AuditMapFunc DefaultReportCardMappedMapper(string renamedOperation)
        {
            return dto =>
            {
                dto.Operation = renamedOperation;
                return DefaultReportCardMapper(dto);
            };
        }
        public SyslogMessage DefaultReportCardMapper(AuditedEventDTO evt)
        {
            var details = JsonConvert.DeserializeObject<DetailsObject>(evt.Details);
            var messageDetail = string.Empty;
            var reportCard = JsonConvert.DeserializeObject<ReportCard>(details.Details, new JsonSerializerSettings
            {
                Error = (_, __) => messageDetail = $"{details.Details}"
            });
            if (reportCard != null)
            {
                messageDetail = $" {string.Join(";", reportCard.ErrorMessages.ToArray())}";
            }
            return FromEventDto(evt, $"{evt.Operation} {evt.EventTypeDescription()}{messageDetail}");
        }

        protected void AddActionMap(string operationName) => AddActionMap(operationName, operationName);

        protected void AddActionMap(string operationName, string renamedOperation)
        {
            AddMappedMap(new[] { $"{operationName} Starting", $"{operationName} Completed", $"{operationName} Failed" }, renamedOperation);
        }
        
        protected void AddMappedMap(string mappedOperation, string renamedOperation)
        {
            AddMap(mappedOperation, MappedOperation(renamedOperation));
        }

        protected void AddMappedMap(IEnumerable<string> operations, string renamedOperation)
        {
            var mapper = MappedOperation(renamedOperation);
            foreach (var op in operations)
            {
                AddMap(op, mapper);
            }
        }
        protected AuditMapFunc MappedOperation(string newOperation)
        {
            return evt =>
            {
                evt.Operation = newOperation;
                return DefaultOpResultMapper(evt);
            };
        }
    }
}