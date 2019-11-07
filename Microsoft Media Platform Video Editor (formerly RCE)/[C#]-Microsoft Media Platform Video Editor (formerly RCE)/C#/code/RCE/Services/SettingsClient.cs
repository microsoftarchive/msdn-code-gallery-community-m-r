// <copyright file="SettingsClient.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SettingsClient.cs                     
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
    using System.Net;
    using System.Net.Browser;
    using System.Xml.Linq;
    using Infrastructure;

    /// <summary>
    /// Client used to download the application settings.
    /// </summary>
    public class SettingsClient
    {
        /// <summary>
        /// The settings Uri.
        /// </summary>
        private readonly Uri settingsUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsClient"/> class.
        /// </summary>
        /// <param name="settingsUri">The uri where the settings are located.</param>
        public SettingsClient(Uri settingsUri)
        {
            this.settingsUri = settingsUri;
        }

        /// <summary>
        /// Ocurrs when downloading of settings is completed.
        /// </summary>
        public event EventHandler<DataEventArgs<IDictionary<string, string>>> GetSettingsCompleted;

        /// <summary>
        /// Starts to download the application settings.
        /// </summary>
        public void GetSettingsAsync()
        {
            HttpWebRequest settingsRequest = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(this.settingsUri);
            settingsRequest.Method = "GET";
            settingsRequest.BeginGetResponse(this.SettingsRequestCallback, settingsRequest);
        }

        /// <summary>
        /// Parses the downloaded settings.
        /// </summary>
        /// <param name="document">The document that contains the application settings.</param>
        /// <returns>An <see cref="IDictionary{TKey,TValue}"/> instance that contains the parsed parameters.</returns>
        private static IDictionary<string, string> ParseDocument(XContainer document)
        {
            IEnumerable<XElement> parameters = document.Elements("Settings").Elements("Parameter");

            IDictionary<string, string> parsedParameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (XElement element in parameters)
            {
                if (element.Attribute("Name") != null && element.Attribute("Value") != null)
                {
                    string key = element.Attribute("Name").GetValue();
                    string value = element.Attribute("Value").GetValue();

                    if (!parsedParameters.ContainsKey(key))
                    {
                        parsedParameters.Add(key, value);
                    }
                }
            }

            return parsedParameters;
        }

        /// <summary>
        /// Callback for the GetSettingsAsync operation. Tries to parse the application settings response.
        /// </summary>
        /// <param name="result">The status of the asynchronous operation.</param>
        private void SettingsRequestCallback(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;

                WebResponse response = request.EndGetResponse(result);

                XDocument document = XDocument.Load(response.GetResponseStream());

                IDictionary<string, string> settings = ParseDocument(document);

                response.Close();

                this.GetSettingsCompleted.Invoke(this, new DataEventArgs<IDictionary<string, string>>(settings));
            }
            catch (Exception ex)
            {
                this.GetSettingsCompleted.Invoke(this, new DataEventArgs<IDictionary<string, string>>(ex));
            }
        }
    }
}
