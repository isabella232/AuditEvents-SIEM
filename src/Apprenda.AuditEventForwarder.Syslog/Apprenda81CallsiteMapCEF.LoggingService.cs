// ----------------------------------------------------------------------------------------------------
// <copyright file="Apprenda81CallsiteMapCEF.LoggingService.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
/// <summary>
/// This partial provides the Logging Service's audite event mappings.
/// </summary>
    public partial class Apprenda81CallsiteMapCEF
    {
        private void ConfgureLoggingService()
        {
            AddDefaultMapCef("Set Global Log Level", "LS1");
            AddDefaultMapCef("Global Log Email Recipient Addition", "LS2");
            AddDefaultMapCef("Global Log Email Recipient Removal", "LS3");
            AddDefaultMapCef("Log Override Added", "LS4");
            AddDefaultMapCef("Log Override Updated", "LS5");
            AddDefaultMapCef("Log Override Version Migrated", "LS6");
            AddDefaultMapCef("Log Override Removed", "LS7");
            AddDefaultMapCef("Log Filter Addition", "LS8");
        }
    }
}