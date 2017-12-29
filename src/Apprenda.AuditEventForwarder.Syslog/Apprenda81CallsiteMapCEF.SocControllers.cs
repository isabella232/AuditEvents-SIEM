// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMapCEF.SocControllers.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

#pragma warning disable SA1200
using System.Globalization;
using Apprenda.SaaSGrid.Extensions.DTO;
using SyslogNet.Client;
#pragma warning restore SA1200

namespace Apprenda.AuditEventForwarder.Syslog
{
    using System;
    using Newtonsoft.Json;
    using AuditMapFunc = System.Func<AuditedEventDTO, SyslogMessage>;

    /// <summary>
    /// This partial provides the SOC Controller audit event formatter mappings for the Apprenda Cloud Platform 8.1
    /// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        /// <summary>
        /// Factory method which produces a Formatter renaming the incoming event's operation to the provided name and parsing a ReasonDetail from the Details
        /// </summary>
        /// <param name="mappedName">The operation alias to emit</param>
        /// <param name="cefEventId">The CEF event correlation ID</param>
        /// <returns>A formatter</returns>
        private AuditMapFunc MappedReasonDetailFormatter(string mappedName, string cefEventId)
        {
            return evt =>
            {
                evt.Operation = mappedName;
                var eventDetails = JsonConvert.DeserializeObject<DetailsObject>(evt.Details);
                var details = JsonConvert.DeserializeObject<ReasonDetail>(eventDetails.Details);
                var message =
                    $"CEF:0|Apprenda|CloudPlatform|{PlatformVersion}|-|{evt.Operation}|{cefEventId}|outcome={evt.EventTypeDescription()} {details}";
                return evt.ToSyslogMessage(message);
            };
        }

        private void ConfigureSocControllers()
        {
            AddMappedMapCef(new[] { "Add-on Upload Failed", "Add-on Uploaded" }, "Add-on Upload", "SOC1");
            AddMappedMapCef("Add-On deleted", "Add-on Deleted", "SOC2");
            AddMappedMapCef(new[] { "Create Kubernetes cluster started", "Create Kubernetes cluster completed", "Create Kubernetes cluster failed" }, "Create Kubernetes Cluster", "SOC3");
            AddMappedMapCef(new[] { "Update Kubernetes cluster started", "Update Kubernetes cluster completed", "Update Kubernetes cluster failed" }, "Update Kubernetes Cluster", "SOC4");
            AddMappedMapCef(new[] { "Delete Kubernetes cluster started", "Delete Kubernetes cluster completed", "Delete Kubernetes cluster failed" }, "Delete Kubernetes Cluster", "SOC5");
            AddActionMapCef("DB Partition Information Modification", "SOC6");
            AddDefaultMapCef("External Auth Header Modification", "SOC7");
            AddMap("External Auth Plugin Update Failed", dto =>
            {
                var result = dto.Details;

                // todo: dto.Details might be a report card.
                // AddMap("External Auth Plugin Update Failed", DefaultReportCardMappedMapper("Update External Authentication Plugin"));
                return dto.ToSyslogMessage(
                    Facility.SecurityOrAuthorizationMessages1,
                    Severity.Error,
                    $"CEF:0|Apprenda|CloudPlatform|{PlatformVersion}|-|{dto.Operation}|SOC8|outcome={dto.EventTypeDescription()} {result}");
            });
            AddMappedMapCef(new[] { "External Auth Plugin Update Started", "External Auth Plugin Update Completed" }, "Update External Authentication Plugin", "SOC9");
            AddMappedMapCef(new[] { "External Auth Plugin Removal Started", "External Auth Plugin Removal Failed", "External Auth Plugin Removal Completed" }, "External Auth Plugin Removal", "SOC10");
            var operationParent = "Setting Node Role State {0}";
            var operationEvents = new[] { "Started", "Failed", "Completed" };
            foreach (var op in operationEvents)
            {
                AddMap(string.Format(CultureInfo.InvariantCulture, operationParent, op), MappedReasonDetailFormatter("Node Role Set State", "SOC11"));
            }

            operationParent = "Setting Node State {0}";
            foreach (var op in operationEvents)
            {
                AddMap(string.Format(CultureInfo.InvariantCulture, operationParent, op), MappedReasonDetailFormatter("Node Set State", "SOC12"));
            }

            AddMappedMapCef(new[] { "Assign Tenant Administrator Started", "Assign Tenant Administrator Completed", "Assign Tenant Administrator Failed" }, "Assign Tenant Administrator", "SOC13");
            AddMappedMapCef(new[] { "Custom Bootstrap Policy Creation Completed", "Custom Bootstrap Policy Creation Failed" }, "Custom Bootstrap Policy Creation", "SOC14");
            AddMappedMapCef(new[] { "Static Bootstrap Policy Creation Completed", "Static Bootstrap Policy Creation Failed" }, "Static Bootstrap Policy Creation", "SOC15");
            AddMappedMapCef(new[] { "Custom Bootstrap Policy Modification Completed", "Custom Bootstrap Policy Modification Failed" }, "Custom Bootstrap Policy Modification", "SOC16");
            AddMappedMapCef(new[] { "Static Bootstrap Policy Modification Completed", "Static Bootstrap Policy Modification Failed" }, "Static Bootstrap Policy Modification", "SOC17");
            AddMappedMapCef(new[] { "Bootstrap Policy Deletion Completed", "Bootstrap Policy Deletion Failed" }, "Bootstrap Policy Deletion", "SOC18");
            AddActionMapCef("Addition of an Operator to the SOC", "SOC Add Operator", "SOC19");
            AddActionMapCef("Removal of an Operator to the SOC", "SOC Remove Operator", "SOC20");
            AddActionMapCef("Cancel Promotion", "SOC21");
            AddActionMapCef("Workload Account Creation", "SOC22");
            AddActionMapCef("Workload Account Modification", "SOC23");
            AddActionMapCef("Workload Account deletion", "SOC24");
            AddDefaultMapCef("Encrypt password", "SOC25");
            AddDefaultMapCef("Export Platform Encryption Certificate", "SOC26");
            AddActionMapCef("Log Deltion", "SOC27");
            AddActionMapCef("Delete All Logs", "SOC28");
        }

        /// <summary>
        /// Standard encapsulation of a named Reason within the Detail of a AuditedEventDTO
        /// </summary>
        internal class ReasonDetail
        {
            /// <summary>
            /// Gets or sets the Reason details.
            /// </summary>
            /// <value>The Reason</value>
            public string Reason { get; set; }
        }
    }
}