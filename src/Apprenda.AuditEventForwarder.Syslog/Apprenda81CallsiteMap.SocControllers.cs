// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.SocControllers.cs" company="Apprenda, Inc.">
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
    public partial class Apprenda81CallsiteMap
    {
        /// <summary>
        /// Factory method which produces a Formatter renaming the incoming event's operation to the provided name and parsing a ReasonDetail from the Details
        /// </summary>
        /// <param name="mappedName">The operation alias to emit</param>
        /// <returns>A formatter</returns>
        private static AuditMapFunc MappedReasonDetailFormatter(string mappedName)
        {
            return evt =>
            {
                evt.Operation = mappedName;
                var eventDetails = JsonConvert.DeserializeObject<DetailsObject>(evt.Details);
                var details = JsonConvert.DeserializeObject<ReasonDetail>(eventDetails.Details);

                return FromEventDTO(evt, $"{evt.Operation} {evt.EventTypeDescription()} {details.Reason}");
            };
        }

        private void ConfigureSocControllers()
        {
            AddMappedMap(new[] { "Add-on Upload Failed", "Add-on Uploaded" }, "Add-on Upload");
            AddMappedMap("Add-On deleted", "Add-on Deleted");
            AddMappedMap(new[] { "Create Kubernetes cluster started", "Create Kubernetes cluster completed", "Create Kubernetes cluster failed" }, "Create Kubernetes Cluster");
            AddMappedMap(new[] { "Update Kubernetes cluster started", "Update Kubernetes cluster completed", "Update Kubernetes cluster failed" }, "Update Kubernetes Cluster");
            AddMappedMap(new[] { "Delete Kubernetes cluster started", "Delete Kubernetes cluster completed", "Delete Kubernetes cluster failed" }, "Delete Kubernetes Cluster");
            AddActionMap("DB Partition Information Modification");
            AddDefaultMap("External Auth Header Modification");
            AddMap("External Auth Plugin Update Failed", dto =>
            {
                var result = dto.Details;

                // todo: dto.Details might be a report card.
                // AddMap("External Auth Plugin Update Failed", DefaultReportCardMappedMapper("Update External Authentication Plugin"));
                return FromEventDTO(dto, Facility.SecurityOrAuthorizationMessages1, Severity.Error, $"{dto.Operation} {result}");
            });
            AddMappedMap(new[] { "External Auth Plugin Update Started", "External Auth Plugin Update Completed" }, "Update External Authentication Plugin");
            AddMappedMap(new[] { "External Auth Plugin Removal Started", "External Auth Plugin Removal Failed", "External Auth Plugin Removal Completed" }, "External Auth Plugin Removal");
            var operationParent = "Setting Node Role State {0}";
            var operationEvents = new[] { "Started", "Failed", "Completed" };
            foreach (var op in operationEvents)
            {
                AddMap(string.Format(CultureInfo.InvariantCulture, operationParent, op), MappedReasonDetailFormatter("Node Role Set State"));
            }

            operationParent = "Setting Node State {0}";
            foreach (var op in operationEvents)
            {
                AddMap(string.Format(CultureInfo.InvariantCulture, operationParent, op), MappedReasonDetailFormatter("Node Set State"));
            }

            AddMappedMap(new[] { "Assign Tenant Administrator Started", "Assign Tenant Administrator Completed", "Assign Tenant Administrator Failed" }, "Assign Tenant Administrator");
            AddMappedMap(new[] { "Custom Bootstrap Policy Creation Completed", "Custom Bootstrap Policy Creation Failed" }, "Custom Bootstrap Policy Creation");
            AddMappedMap(new[] { "Static Bootstrap Policy Creation Completed", "Static Bootstrap Policy Creation Failed" }, "Static Bootstrap Policy Creation");
            AddMappedMap(new[] { "Custom Bootstrap Policy Modification Completed", "Custom Bootstrap Policy Modification Failed" }, "Custom Bootstrap Policy Modification");
            AddMappedMap(new[] { "Static Bootstrap Policy Modification Completed", "Static Bootstrap Policy Modification Failed" }, "Static Bootstrap Policy Modification");
            AddMappedMap(new[] { "Bootstrap Policy Deletion Completed", "Bootstrap Policy Deletion Failed" }, "Bootstrap Policy Deletion");
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