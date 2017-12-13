// ----------------------------------------------------------------------------------------------------
// <copyright file="IExtensionConfiguration.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// Provides the runtime configuration information needed for extension operation.
    /// </summary>
    public interface IExtensionConfiguration
    {
        /// <summary>
        /// Gets the Syslog receiver host (IP or hostname)
        /// </summary>
        /// <value>The hostname or IP address</value>
        string Host { get; }

        /// <summary>
        /// Gets the port of the Syslog receiver
        /// </summary>
        /// <value>The port</value>
        int Port { get; }

        /// <summary>
        /// Gets the supported Syslog flavor of the Syslog receiver
        /// </summary>
        /// <value>The flavor</value>
        SyslogFlavor Flavor { get; }

        /// <summary>
        /// Gets the transport protocol acceptbed by the Syslog receiver
        /// </summary>
        /// <value>The protocol</value>
        SyslogProtocol Protocol { get; }

        /// <summary>
        /// Gets the Operation formatter map to create Syslog messages from AuditedEventDTOs for the Syslog receiver.
        /// </summary>
        /// <value>The map of formatters</value>
        IAuditCallsiteMap AuditEventMap { get; }
    }
}