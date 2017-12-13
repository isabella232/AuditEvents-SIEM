// ----------------------------------------------------------------------------------------------------
// <copyright file="AddOnExtensionConfiguration.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    using System;
    using System.Configuration;
    using Apprenda.Services.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// Consume the addon endpoint details to provide connection details for the extension.
    /// </summary>
    public class AddOnExtensionConfiguration : IExtensionConfiguration
    {
        private readonly ConfigurationDTO _configData;

        private IAuditCallsiteMap _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOnExtensionConfiguration"/> class.
        /// </summary>
        public AddOnExtensionConfiguration()
        {
            var serialized = ConfigurationManager.AppSettings["SyslogHostPortProtocol"];
            if (serialized.StartsWith("$#ADDON", StringComparison.Ordinal))
            {
                LogManager.Instance().GetLogger(typeof(AddOnExtensionConfiguration))
                    .Error(
                        "The connection information was not injected into this workload (perhaps the Host-Port add-on has not been configured by platform operators)");
                _configData = new ConfigurationDTO();
            }
            else
            {
                _configData = JsonConvert.DeserializeObject<ConfigurationDTO>(serialized);
            }
        }

        /// <inheritdoc />
        public string Host => _configData.Host;

        /// <inheritdoc />
        public int Port => _configData.Port;

        /// <inheritdoc />
        public SyslogFlavor Flavor => _configData.Flavor;

        /// <inheritdoc />
        public SyslogProtocol Protocol => _configData.Protocol;

        /// <inheritdoc />
        public IAuditCallsiteMap AuditEventMap => _map ?? (_map = ChooseMap(_configData.MapType));

        /// <summary>
        /// Choose the appropriate CallsiteMap once we have additional platform versions that require such a mapping.
        /// </summary>
        /// <param name="mapType">The configuration map type</param>
        /// <returns>A callsite map instance</returns>
        private static IAuditCallsiteMap ChooseMap(string mapType)
        {
            switch (mapType)
            {
                default: return new Apprenda81CallsiteMap();
            }
        }
    }
}