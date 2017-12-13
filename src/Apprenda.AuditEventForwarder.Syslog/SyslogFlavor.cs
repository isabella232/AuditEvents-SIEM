// ----------------------------------------------------------------------------------------------------
// <copyright file="SyslogFlavor.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// The Syslog flavor understood by a Syslog receiver
    /// </summary>
    public enum SyslogFlavor
    {
        /// <summary>
        /// RFC3164 The Syslog protocol
        /// </summary>
        Rfc3164,

        /// <summary>
        /// RFC5424 Updated Syslog protocol
        /// </summary>
        Rfc5424,

        /// <summary>
        /// The Arcsight Common Event Format, an enhanced message format encapsulated in RFC5424 messsages
        /// </summary>
        Cef,
    }
}