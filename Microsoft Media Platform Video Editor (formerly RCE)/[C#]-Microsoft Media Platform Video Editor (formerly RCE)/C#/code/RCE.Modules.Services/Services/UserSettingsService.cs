// <copyright file="UserSettingsService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: UserSettingService.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class UserSettingsService : IUserSettingsService
    {
        private const string UserSettingsFileName = "RCEUserSettings.xml";

        private readonly IPersistenceService persistenceService;

        private readonly IConfigurationService configurationService;

        private UserSettings currentUserSettings;

        public UserSettingsService(IPersistenceService persistenceService, IConfigurationService configurationService)
        {
            this.persistenceService = persistenceService;
            this.configurationService = configurationService;
        }

        public event EventHandler SettingsChanged;

        public UserSettings GetSettings()
        {
            if (this.currentUserSettings != null)
            {
                return this.currentUserSettings;
            }

            Stream stream = this.persistenceService.Retrieve(UserSettingsFileName);

            UserSettings userSettings = null;

            if (stream != null)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    userSettings = Deserialize(typeof(UserSettings), content) as UserSettings;
                }

                stream.Close();
            }

            if (userSettings == null)
            {
                userSettings = new UserSettings();
                userSettings.MinBitrate = this.configurationService.GetParameterValueAsLong("MinBitrate").GetValueOrDefault(0);
                userSettings.MaxBitrate = this.configurationService.GetParameterValueAsLong("MaxBitrate").GetValueOrDefault(5000000);
                userSettings.IsSingleBitrate = this.configurationService.GetParameterValueAsBoolean("SingleBitrate").GetValueOrDefault(false);
                userSettings.KeyboardMapping = this.configurationService.GetKeyboardMappings().FirstOrDefault();
                this.currentUserSettings = userSettings;
            }
            else
            {
                this.currentUserSettings = userSettings;

                this.OnSettingsChanged();
            }

            return userSettings;
        }

        public void SaveSettings(UserSettings userSettings)
        {
            string content = Serialize(userSettings);

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            this.persistenceService.Persist(UserSettingsFileName, stream);
            this.currentUserSettings = userSettings;
            this.OnSettingsChanged();
        }

        private static string Serialize(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter writer = new StringWriter(stringBuilder))
            {
                serializer.Serialize(writer, obj);
            }

            return stringBuilder.ToString();
        }

        private static object Deserialize(Type type, string content)
        {
            XDocument document = XDocument.Parse(content);

            try
            {
                using (XmlReader reader = document.CreateReader())
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    return serializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void OnSettingsChanged()
        {
            EventHandler handler = this.SettingsChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
