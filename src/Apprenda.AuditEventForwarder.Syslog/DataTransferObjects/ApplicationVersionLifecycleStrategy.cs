// ----------------------------------------------------------------------------------------------------
// <copyright file="ApplicationVersionLifecycleStrategy.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// Denotes the mechanism which was used to create the ApplicationVersion
    /// </summary>
    public enum ApplicationVersionLifecycleStrategy
    {
        /// <summary>
        /// The ApplicationVersion was created by patching
        /// </summary>
        Patch = 0,

        /// <summary>
        /// The ApplicationVersion was created with a new archive.
        /// </summary>
        NewArchive = 1,
    }
}