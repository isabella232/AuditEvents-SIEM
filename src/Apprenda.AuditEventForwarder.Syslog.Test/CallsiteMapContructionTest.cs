// ----------------------------------------------------------------------------------------------------
// <copyright file="CallsiteMapContructionTest.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog.Test
{
    using FluentAssertions;
    using Xunit;

    /// <summary>
    /// Test cases for the construction of the Platform Version 8.1 Callsite Map
    /// </summary>
    public class CallsiteMapContructionTest
    {
        /// <summary>
        /// Creates an instance of the CallsiteMap to ensure it is correctly instantiated without runtime errors.
        /// </summary>
        [Fact]
        public void Version81CallsiteMapConstructable()
        {
            var map = new Apprenda81CallsiteMap();
            map.Should().NotBeNull();
        }

        /// <summary>
        /// Creates an instance of the CEF CallsiteMap to ensure it is correctly instantiated without runtime errors.
        /// </summary>
        [Fact]
        public void Version81CefCallsiteMapConstructable()
        {
            var map = new Apprenda81CallsiteMapCEF();
            map.Should().NotBeNull();
        }
    }
}
