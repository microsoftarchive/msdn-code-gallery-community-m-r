// <copyright file="ConfigurationService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ConfigurationService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services
{
    using System;
    using System.Collections.Generic;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// Provides an implementation for the <see cref="IConfigurationService"/> service.
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>
        /// The parameters defined in the configuration.
        /// </summary>
        private readonly IDictionary<string, string> settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationService"/> class.
        /// </summary>
        /// <param name="settings">The settings definied in the configuration.</param>
        public ConfigurationService(IDictionary<string, string> settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            this.settings = settings;
        }

        /// <summary>
        /// Occurs when configuration changes.
        /// </summary>
        public event EventHandler ConfigurationChanged;

        /// <summary>
        /// Retrieves the parameter value based on the given <paramref name="parameter"/>.
        /// </summary>
        /// <param name="parameter">The parameter to look for.</param>
        /// <returns>The parameter value if the parameter exists;othwerwise null.</returns>
        public string GetParameterValue(string parameter)
        {
            if (this.settings.ContainsKey(parameter))
            {
                return this.settings[parameter];
            }

            return null;
        }

        /// <summary>
        /// Updates the value of the given parameters.
        /// </summary>
        /// <param name="parameters">The parameters to update.</param>
        public void UpdateParameters(IDictionary<string, string> parameters)
        {
            bool settingsUpdated = false;

            foreach (string key in parameters.Keys)
            {
                if (this.settings.ContainsKey(key) && this.settings[key] != parameters[key])
                {
                    settingsUpdated = true;
                    this.settings[key] = parameters[key];
                }
            }

            if (settingsUpdated)
            {
                this.OnConfigurationChanged();
            }
        }

        private void OnConfigurationChanged()
        {
            EventHandler handler = this.ConfigurationChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
