// ----------------------------------------------------------------------------------------------------
// <copyright file="ApplicationVersionDTO.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------
namespace Apprenda.AuditEventForwarder.Syslog
{
    using System;

    /// <summary>
    /// Details of the Application version, deserialized from some audit events.
    /// See https://docs.apprenda.com API documentation for details of the Application Version REST APIs
    /// </summary>
#pragma warning disable CA1812 // This type is instantiated by deserialization code.
    internal class ApplicationVersionDto
    {
#pragma warning disable SA1600 // Elements should be documented
        public string Alias { get; set; }

        public Guid ApplicationId { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; }

        public bool IsStopped { get; set; }

        public string Name { get; set; }

        public string PreviousVersionAlias { get; set; }

        public ApplicationVersionStage Stage { get; set; }

        public ApplicationVersionLifecycleStrategy Strategy { get; set; }

        public VersionTransitionType Transition { get; set; }

        public PresentationDeploymentStrategy? UrlStrategy { get; set; }

        public Guid VersionTenantId { get; set; }
#pragma warning restore SA1600 // Elements should be documented
    }
}
