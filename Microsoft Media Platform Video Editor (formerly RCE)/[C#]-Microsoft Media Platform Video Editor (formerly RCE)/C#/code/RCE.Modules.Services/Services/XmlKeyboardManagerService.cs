// <copyright file="XmlKeyboardManagerService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: XmlKeyboardManagerService.cs                     
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
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Browser;
    using System.Windows.Input;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Infrastructure;

    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    /// <summary>
    /// Default Implementation.
    /// </summary>
    public class XmlKeyboardManagerService : KeyboardManagerService
    {
        private readonly KeyboardManagerService fallbackKeyboardService;

        private readonly IUserSettingsService userSettingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlKeyboardManagerService"/> class.
        /// </summary>
        /// <param name="fallbackKeyboardService"></param>
        /// <param name="userSettingsService">
        /// The url where is the xml mapping file.
        /// </param>
        public XmlKeyboardManagerService([Dependency("DefaultKeyboardManager")] KeyboardManagerService fallbackKeyboardService, IUserSettingsService userSettingsService)
        {
            this.fallbackKeyboardService = fallbackKeyboardService;
            this.userSettingsService = userSettingsService;
            this.userSettingsService.SettingsChanged += this.OnSettingsChanged;

            this.LoadMappings();
        }

        /// <summary>
        /// Deserialize an XML to create mappings on memory.
        /// </summary>
        /// <param name="document">
        /// The url to use to retrieve xml configuration file.
        /// </param>
        /// <returns>
        /// A collection of keyboard maps.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        private static IEnumerable<KeyboardMapping> Deserialize(XDocument document)
        {
            XmlReader reader = document.CreateReader();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(KeyboardMappingCollection));
            KeyboardMappingCollection keyboardMappingCollection = (KeyboardMappingCollection)xmlSerializer.Deserialize(reader);
            return keyboardMappingCollection;
        }

        private void LoadMappings()
        {
            this.Mappings.Clear();
            UserSettings userSettings = this.userSettingsService.GetSettings();

            string url = null;
            if (userSettings.KeyboardMapping != null)
            {
                url = userSettings.KeyboardMapping.Url;
            }

            this.GetMappingsAsync(url);
        }

        private void OnSettingsChanged(object sender, EventArgs e)
        {
            this.LoadMappings();
        }
       
        /// <summary>
        /// Starts to download the keyboard mappings.
        /// </summary>
        private void GetMappingsAsync(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                url = string.Concat(url, "?ignore=", Guid.NewGuid());
                HttpWebRequest mappingsRequest = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(new Uri(url));
                mappingsRequest.Method = "GET";
                mappingsRequest.BeginGetResponse(this.MappingsRequestCallback, mappingsRequest);
            }
            else
            {
                this.SetFallbackMappings();
            }
        }

        /// <summary>
        /// Callback for the GetMappingsAsync operation. Tries to parse the Keyboard mappings response.
        /// </summary>
        /// <param name="result">The status of the asynchronous operation.</param>
        private void MappingsRequestCallback(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;

                WebResponse response = request.EndGetResponse(result);

                XDocument document = XDocument.Load(response.GetResponseStream());

                IEnumerable<KeyboardMapping> keyboardMappingCollection = Deserialize(document);

                foreach (var keyboardMapping in keyboardMappingCollection)
                {
                    this.Map(
                        keyboardMapping.Key,
                        keyboardMapping.ModifierKeys,
                        keyboardMapping.KeyboardAction,
                        keyboardMapping.KeyboardActionContext);
                }

                response.Close();
            }
            catch
            {
                this.SetFallbackMappings();
            }
        }

        private void SetFallbackMappings()
        {
            this.Mappings = this.Mappings.Count == 0 ? this.fallbackKeyboardService.Mappings : this.Mappings;
        }

        /// <summary>
        /// Class to serialize/deserealize mappings.
        /// </summary>
        [XmlRoot("KeyboardMappings")]
        public class KeyboardMappingCollection : List<KeyboardMapping>
        {
        }

        /// <summary>
        /// Class to serialize/deserealize mappings.
        /// </summary>
        public class KeyboardMapping
        {
            /// <summary>
            /// Gets or sets key.
            /// </summary>
            /// <value>
            /// The key value.
            /// </value>
            [XmlAttribute]
            public Key Key { get; set; }

            /// <summary>
            /// Gets or sets modifierKeys.
            /// </summary>
            /// <value>
            /// The modifier keys.
            /// </value>
            [XmlAttribute]
            public ModifierKeys ModifierKeys { get; set; }

            /// <summary>
            /// Gets or sets keyboardAction.
            /// </summary>
            /// <value>
            /// The keyboard action.
            /// </value>
            [XmlAttribute]
            public KeyboardAction KeyboardAction { get; set; }

            /// <summary>
            /// Gets or sets keyboardActionContext.
            /// </summary>
            /// <value>
            /// The keyboard action context.
            /// </value>
            [XmlAttribute]
            public KeyboardActionContext KeyboardActionContext { get; set; }
        }
    }
}
