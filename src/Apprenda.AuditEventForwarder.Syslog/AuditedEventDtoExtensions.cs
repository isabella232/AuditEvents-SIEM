// ----------------------------------------------------------------------------------------------------
// <copyright file="AuditedEventDTOExtensions.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Apprenda.AuditEventForwarder.Syslog
{
    using Apprenda.SaaSGrid.Extensions.DTO;

/// <summary>
/// Provide extension methods for the AuditedEventDto type.
/// </summary>
    public static class AuditedEventDTOExtensions
    {
        /// <summary>
        /// Retrieve an English descriptive word for the EventType property of the provided AuditedEventDTO instance.
        /// </summary>
        /// <param name="source">An AuditedEventDto</param>
        /// <returns>en-US Descriptive term for the AuditedEventDTO EventType property</returns>
        public static string EventTypeDescription(this AuditedEventDTO source)
        {
            if (source == null)
            {
                return "Unknown event state";
            }

            switch (source.EventType)
            {
                case AuditEventType.OperationCompleted: return "Completed";
                case AuditEventType.OperationFailed: return "Failed";
                case AuditEventType.OperationStarting: return "Starting";
                default: return "Unknown event state";
            }
        }
    }
}