// ----------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationDto.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// Serialization clas for the endpoint configuration common to the Syslog Addon and Syslog Extension Service
    /// </summary>
    public class ConfigurationDTO
    {
        /// <summary>
        /// Gets or sets the hostname or IP Address of the endpoint.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port of the endpoint
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the Syslog Flavor supported by the endpoint
        /// </summary>
        public SyslogFlavor Flavor { get; set; }

        /// <summary>
        /// Gets or sets the transport protocol supported by the endpoint
        /// </summary>
        public SyslogProtocol Protocol { get; set; }

        /// <summary>
        /// Gets or sets the AuditedEventDTO to SyslogMessage Formatter dictionary for this endpoint.
        /// </summary>
        public string MapType { get; set; }
    }
}