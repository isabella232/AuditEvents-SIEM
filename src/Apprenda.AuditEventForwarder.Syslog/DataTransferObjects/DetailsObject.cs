// ----------------------------------------------------------------------------------------------------
// <copyright file="DetailsObject.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
#pragma warning disable CA1812, SA1600 // This type is instantiated by deserialization code.
    internal class DetailsObject
    {
        public string Details { get; set; }

        public string OriginalValue { get; set; }

        public string NewValue { get; set; }
    }
}