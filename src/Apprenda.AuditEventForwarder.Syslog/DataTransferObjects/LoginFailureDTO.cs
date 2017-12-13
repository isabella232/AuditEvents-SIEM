// ----------------------------------------------------------------------------------------------------
// <copyright file="LoginFailureDTO.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
#pragma warning disable SA1300, SA1402, CA1702
    /// <summary>
    /// Serialization support for Login Failure events.
    /// </summary>
    public class LoginFailureDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the failed login attempt
        /// </summary>
        /// <value>The identifier</value>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the email address of the failed login attempt, if known.
        /// </summary>
        /// <value>The email address</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the login username of the failed login attempt, if known.
        /// </summary>
        /// <value>The username</value>
        public string LoginUsername { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether failed login attempt interacted with a known user.
        /// </summary>
        /// <value>Was the user known</value>
        public bool KnownUser { get; set; }

        /// <summary>
        /// Gets or sets the flag whether the failed login attempt was associated with a locked out user.
        /// </summary>
        /// <value>Was the user locked out?</value>
        public string WasLockedOut { get; set; }

        /// <summary>
        /// Gets or sets the flag whether the failed login attempt caused the user to be locked out user.
        /// </summary>
        /// <value>Is the user now locked out?</value>
        public string IsLockedOut { get; set; }

        /// <summary>
        /// Gets or sets a string representing the number of failed login attempts, if the platform was able to aggregate that count.
        /// </summary>
        /// <value>Description of the number of failed login attempts</value>
        public string FailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the Reason for login failure if known.
        /// </summary>
        /// <value>The Reason</value>
        public string Reason { get; set; }
    }
}
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning disable SA1402
