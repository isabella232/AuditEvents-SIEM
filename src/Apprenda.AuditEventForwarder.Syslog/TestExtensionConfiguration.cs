// ----------------------------------------------------------------------------------------------------
// <copyright file="TestExtensionConfiguration.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// Configuration settings for local workstation (bxcr or platform developer) instances of a Syslog receiver on the machine running the Extension
    /// </summary>
    public class TestExtensionConfiguration : IExtensionConfiguration
    {
        /// <inheritdoc />
        public string Host => "localhost";

        /// <inheritdoc />
        public int Port => 514;

        /// <inheritdoc />
        public SyslogFlavor Flavor => SyslogFlavor.Rfc5424;

        /// <inheritdoc />
        public SyslogProtocol Protocol => SyslogProtocol.Tcp;

        /// <inheritdoc />
        public IAuditCallsiteMap AuditEventMap { get; } = new Apprenda81CallsiteMap();
    }
}