// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMap.LoggingService.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
/// <summary>
/// This partial provides the Logging Service's audite event mappings.
/// </summary>
    public partial class Apprenda81CallsiteMap
    {
        private void ConfgureLoggingService()
        {
            AddDefaultMap("Set Global Log Level");
            AddDefaultMap("Global Log Email Recipient Addition");
            AddDefaultMap("Global Log Email Recipient Removal");
            AddDefaultMap("Log Override Added");
            AddDefaultMap("Log Override Updated");
            AddDefaultMap("Log Override Version Migrated");
            AddDefaultMap("Log Override Removed");
            AddDefaultMap("Log Filter Addition");
        }
    }
}