// <copyright file="FloatableWindowManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FloatableWindowManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Windows
{
    using System;
    using System.Collections.Generic;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using System.Windows;

    using RCE.Infrastructure.Services;

    public class FloatableWindowManager : IWindowManager
    {
        private readonly ICollection<IWindow> windows;
        private const string WindowsPropertiesSettingsName = "WindowsProperties";
        private readonly IPersistenceService persistenceService;

        public FloatableWindowManager(IPersistenceService persistenceService)
        {
            this.windows = new List<IWindow>();
            this.persistenceService = persistenceService;
        }

        public IWindow CreateWindow()
        {
            var window = new FloatableWindowAdapter();
            this.windows.Add(window);
            return window;
        }

        public IWindow GetWindowWithFocus()
        {
            return this.windows.FirstOrDefault(w => w.HasFocus);
        }

        public void PersistProperty(string windowName, string propertyName, object value)
        {
            IDictionary<string, object> windowSpecificProperties = this.GetPropertiesForWindow(windowName);
            windowSpecificProperties[propertyName] = value;
            this.SetProperties(windowName, windowSpecificProperties);
        }

        public object RecoverProperty(string windowName, string propertyName)
        {
            IDictionary<string, object> windowSpecificProperties = this.GetPropertiesForWindow(windowName);

            if (!windowSpecificProperties.ContainsKey(propertyName))
            {
                return null;
            }

            return windowSpecificProperties[propertyName];
        }

        public void RemoveProperty(string windowName, string propertyName)
        {
            IDictionary<string, object> properties = this.GetPropertiesForWindow(windowName);

            if (properties.ContainsKey(propertyName))
            {
                properties.Remove(propertyName);
            }

            this.SetProperties(windowName, properties);
        }

        public bool ShouldDisplayWindow(string windowName, bool defaultValue)
        {
            bool? visiblity = this.RecoverProperty(windowName, "Visibility") as bool?;
            return visiblity.HasValue ? visiblity.Value : defaultValue;
        }

        private IDictionary<string, object> GetPropertiesForWindow(string windowName)
        {
            IDictionary<string, IDictionary<string, object>> windowProperties = this.GetProperties();

            if (!windowProperties.ContainsKey(windowName))
            {
                windowProperties.Add(windowName, new Dictionary<string, object>());
                this.SetProperties(windowName, windowProperties[windowName]);
            }

            return windowProperties[windowName];
        }

        private IDictionary<string, IDictionary<string, object>> GetProperties()
        {
            IDictionary<string, IDictionary<string, object>> windowProperties;

            IDictionary<string, object> settings = this.persistenceService.GetApplicationSettings();

            if (settings.ContainsKey(WindowsPropertiesSettingsName))
            {
                windowProperties = settings[WindowsPropertiesSettingsName] as IDictionary<string, IDictionary<string, object>>;
            }
            else
            {
                windowProperties = new Dictionary<string, IDictionary<string, object>>();
                this.persistenceService.AddApplicationSettings(WindowsPropertiesSettingsName, windowProperties);
            }

            return windowProperties;
        }

        private void SetProperties(string windowName, IDictionary<string, object> windowsProperties)
        {
            IDictionary<string, IDictionary<string, object>> applicationProperties = this.GetProperties();
            applicationProperties[windowName] = windowsProperties;
            this.persistenceService.AddApplicationSettings(WindowsPropertiesSettingsName, applicationProperties);
        }
    }
}
