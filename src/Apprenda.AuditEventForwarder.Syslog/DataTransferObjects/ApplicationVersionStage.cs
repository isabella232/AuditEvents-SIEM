// ----------------------------------------------------------------------------------------------------
// <copyright file="ApplicationVersionStage.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
#pragma warning disable SA1028 // enumeration underlying type must match API
#pragma warning disable SA1602 // Enumeration items should be documented
    /// <summary>
    /// The lifecycle stage of the ApplicationVersion
    /// </summary>
    internal enum ApplicationVersionStage : byte
    {
        Definition = 1,
        Sandbox = 2,
        Published = 3,
        Archived = 4,
    }
#pragma warning restore SA1602 // Enumeration items should be documented
#pragma warning restore SA1028 // enumeration underlying type must match API
}
