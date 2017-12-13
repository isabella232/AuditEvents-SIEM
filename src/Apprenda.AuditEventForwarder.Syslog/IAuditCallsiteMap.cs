// ----------------------------------------------------------------------------------------------------
// <copyright file="IAuditCallsiteMap.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    using System;
    using System.Collections.Generic;
    using Apprenda.SaaSGrid.Extensions.DTO;
    using SyslogNet.Client;

    /// <summary>
    /// Provide a standard interface for mapping of Operations to SyslogMessage formatters.
    /// </summary>
    public interface IAuditCallsiteMap
    {
        /// <summary>
        /// Gets the map of operation names to formatters that output SyslogMessage representation of the provided AuditedEventDTO
        /// </summary>
        /// <value>The map (Dictionary) of Operations to Formatters</value>
        Dictionary<string, Func<AuditedEventDTO, SyslogMessage>> Formatters { get; }

        /// <summary>
        /// Convenience method to add an operation-formatter pair to the Formatters of this CallsiteMap
        /// </summary>
        /// <param name="operation">The operation to map to a formatter.</param>
        /// <param name="formatter">The formatter function.</param>
        void AddMap(string operation, Func<AuditedEventDTO, SyslogMessage> formatter);
    }
}