// ----------------------------------------------------------------------------------------------------
// <copyright file="PresentationDeploymentStrategy.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
#pragma warning disable SA1600 // Elements should be documented - Suppress for serialization support types.
#pragma warning disable SA1602 // Enumeration items should be documented - Suppress for serialization support types.
    internal enum PresentationDeploymentStrategy : byte
    {
        CommingledRootApp = 3,
        CommingledAppRoot = 4,
    }
#pragma warning restore SA1602 // Enumeration items should be documented - Suppress for serialization support types.
#pragma warning restore SA1600 // Elements should be documented - Suppress for serialization support types.
}