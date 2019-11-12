// <copyright file="IConfigurationService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IConfigurationService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines an interface to be used to retrieve the values specified in the configuration.
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Occurs when configuration changes.
        /// </summary>
        event EventHandler ConfigurationChanged;

        /// <summary>
        /// Retrieves the parameter value based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="parameter">The parameter to look for.</param>
        /// <returns>The parameter value if the parameter exists;othwerwise null.</returns>
        string GetParameterValue(string parameter);

        /// <summary>
        /// Updates the value of the given parameters.
        /// </summary>
        /// <param name="parameters">The parameters to update.</param>
        void UpdateParameters(IDictionary<string, string> parameters);
    }
}
