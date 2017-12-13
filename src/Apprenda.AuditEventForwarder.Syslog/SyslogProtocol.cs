// ----------------------------------------------------------------------------------------------------
// <copyright file="SyslogProtocol.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// The supported transport protocols for Syslog receivers.
    /// </summary>
    public enum SyslogProtocol
    {
        /// <summary>
        /// TCP
        /// </summary>
        Tcp,

        /// <summary>
        /// TCP over TLS
        /// </summary>
        EncryptedTcp,

        /// <summary>
        /// UDP
        /// </summary>
        Udp,
    }
}