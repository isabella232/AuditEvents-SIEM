// ----------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Apprenda, Inc.">
// Copyright (c) Apprenda, Inc. All rights reserved.
// Licensed under the MIT license. See the LICENSE.md in the project root for full license information.
// </copyright>
// ----------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101", Justification = "Local Standard")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Local Standard")]

[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309", Justification = "Local Standard")]
[assembly: SuppressMessage("Microsoft.Design", "CA2210", Justification = "Assembly is a runtime-loaded platform extension - It is not meaningful to strong-name it.")]