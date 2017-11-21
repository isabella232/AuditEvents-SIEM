using System;
using Apprenda.SaaSGrid.Extensions.DTO;
using Newtonsoft.Json;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    using AuditMapFunc = Func<AuditedEventDTO, SyslogMessage>;
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigureSocControllers()
        {
            AddMappedMap(new [] {"Add-on Upload Failed", "Add-on Uploaded"}, "Add-on Upload");
            AddMappedMap("Add-On deleted", "Add-on Deleted");
            AddMappedMap(new [] { "Create Kubernetes cluster started", "Create Kubernetes cluster completed", "Create Kubernetes cluster failed" }, "Create Kubernetes Cluster");
            AddMappedMap(new [] { "Update Kubernetes cluster started", "Update Kubernetes cluster completed", "Update Kubernetes cluster failed" }, "Update Kubernetes Cluster");
            AddMappedMap(new[] { "Delete Kubernetes cluster started", "Delete Kubernetes cluster completed", "Delete Kubernetes cluster failed" }, "Delete Kubernetes Cluster");
            AddActionMap("DB Partition Information Modification");
            AddDefaultMap("External Auth Header Modification");
            AddMap("External Auth Plugin Update Failed", dto =>
            {
                var result = dto.Details;
                // todo: dto.Details might be a report card.
                //AddMap("External Auth Plugin Update Failed", DefaultReportCardMappedMapper("Update External Authentication Plugin"));

                return FromEventDto(dto, Facility.SecurityOrAuthorizationMessages1, Severity.Error,
                    $"{dto.Operation} {result}");
            });
            AddMappedMap(new[] { "External Auth Plugin Update Started", "External Auth Plugin Update Completed" }, "Update External Authentication Plugin");
            AddMappedMap(new[] { "External Auth Plugin Removal Started", "External Auth Plugin Removal Failed", "External Auth Plugin Removal Completed" }, "External Auth Plugin Removal");
            var operationParent = "Setting Node Role State {0}";
            var operationEvents = new[] {"Started", "Failed", "Completed"};
            foreach (var op in operationEvents)
            {
                AddMap(string.Format(operationParent, op), MappedReasonDetailFormatter("Node Role Set State"));
            }

            operationParent = "Setting Node State {0}";
            foreach (var op in operationEvents)
            {
                AddMap(string.Format(operationParent, op), MappedReasonDetailFormatter("Node Set State"));
            }

            AddMappedMap(new[] { "Assign Tenant Administrator Started", "Assign Tenant Administrator Completed", "Assign Tenant Administrator Failed" }, "Assign Tenant Administrator");
            AddMappedMap(new[] { "Custom Bootstrap Policy Creation Completed", "Custom Bootstrap Policy Creation Failed" }, "Custom Bootstrap Policy Creation");
            AddMappedMap(new[] { "Static Bootstrap Policy Creation Completed", "Static Bootstrap Policy Creation Failed"}, "Static Bootstrap Policy Creation");
            AddMappedMap(new[] { "Custom Bootstrap Policy Modification Completed", "Custom Bootstrap Policy Modification Failed" }, "Custom Bootstrap Policy Modification");
            AddMappedMap(new[] { "Static Bootstrap Policy Modification Completed", "Static Bootstrap Policy Modification Failed" }, "Static Bootstrap Policy Modification");
            AddMappedMap(new[] { "Bootstrap Policy Deletion Completed", "Bootstrap Policy Deletion Failed"}, "Bootstrap Policy Deletion");
            AddActionMap("Addition of an Operator to the SOC", "SOC Add Operator");
            AddActionMap("Removal of an Operator to the SOC", "SOC Remove Operator");
            AddActionMap("Cancel Promotion");
            AddActionMap("Workload Account Creation");
            AddActionMap("Workload Account Modification");
            AddActionMap("Workload Account deletion");
            AddDefaultMap("Encrypt password");
            AddDefaultMap("Export Platform Encryption Certificate");
            AddActionMap("Log Deltion");
            AddActionMap("Delete All Logs");
        }

        AuditMapFunc MappedReasonDetailFormatter(string mappedName)
        {
            return evt =>
            {
                evt.Operation = mappedName;
                var eventDetails = JsonConvert.DeserializeObject<DetailsObject>(evt.Details);
                var details = JsonConvert.DeserializeObject<ReasonDetail>(eventDetails.Details);

                return FromEventDto(evt, $"{evt.Operation} {evt.EventTypeDescription()} {details.Reason}");
            };
        }
            

        internal class ReasonDetail
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Reason { get; set; }
        }
    }
}