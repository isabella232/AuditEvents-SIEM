// ----------------------------------------------------------------------------------------------------
// <copyright file="VersionTransitionType.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    /// <summary>
    /// ApplicationVersion Serialization information
    /// </summary>
    public enum VersionTransitionType
    {
        /// <summary>
        /// No transition in progress
        /// </summary>
        NoTransition = 0,

        /// <summary>
        /// The applicationversion is being promoted
        /// </summary>
        Promoting = 1,

        /// <summary>
        /// The applicationversion is being demoted
        /// </summary>
        Demoting = 2,
    }
}