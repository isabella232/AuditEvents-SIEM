// ----------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    using System;

    /// <summary>
    /// Provides utility methods for Strings used in the Syslog extension.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a string with all Environment.NewLine sequences removed.
        /// </summary>
        /// <param name="source">A string to strip</param>
        /// <returns>The string without newlines</returns>
        public static string StripNewLines(this string source)
        {
            return string.IsNullOrEmpty(source) ? string.Empty : source.Replace(Environment.NewLine, string.Empty);
        }
    }
}