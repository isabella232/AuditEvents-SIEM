// ----------------------------------------------------------------------------------------------------
// <copyright file="ReportCard.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    using System.Collections.Generic;

#pragma warning disable CA1812, SA1600 // This type is instantiated by deserialization code; its fields are those provided by the upstream types in the platform.
    internal class ReportCard
    {
        private List<string> _infoMessages;
        private List<string> _warningMessages;
        private List<string> _errorMessages;

        public List<string> ErrorMessages
        {
            get { return _errorMessages ?? (_errorMessages = new List<string>()); }
            set { _errorMessages = value; }
        }

        public List<string> WarningMessages
        {
            get { return _warningMessages ?? (_warningMessages = new List<string>()); }
            set { _warningMessages = value; }
        }

        public List<string> InfoMessages
        {
            get { return _infoMessages ?? (_infoMessages = new List<string>()); }
            set { _infoMessages = value; }
        }
    }
}
