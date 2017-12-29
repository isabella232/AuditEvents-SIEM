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
            return string.IsNullOrEmpty(source) ? string.Empty : source.Replace("\n", string.Empty).Replace("\r", string.Empty);
        }

        /// <summary>
        /// Breaks a string into a pair of strings, the last word of the input and the remaining input.
        /// </summary>
        /// <param name="phrase">String to break apart</param>
        /// <returns>a Tuple containing Item2 as the last word of the provided string, and Item1 as all preceding content.</returns>
        internal static Tuple<string, string> LastWordOf(this string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                throw new ArgumentNullException(nameof(phrase));
            }

            var lastSpace = phrase.LastIndexOf(" ", StringComparison.Ordinal);

            return new Tuple<string, string>(phrase.Remove(lastSpace), lastSpace < phrase.Length ? phrase.Substring(lastSpace + 1) : string.Empty);
        }
    }
}