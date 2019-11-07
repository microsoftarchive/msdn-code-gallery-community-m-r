// <copyright file="SearchServiceBridge.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SearchServiceBridge.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Search.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Windows;
    using System.Windows.Browser;
    using System.Windows.Threading;
    using System.Xml;
    using Microsoft.Practices.Composite.Events;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class SearchServiceBridge : ISearchServiceBridge
    {
        private readonly IConfigurationService configurationService;
        private readonly IXmlAssetsDataParser xmlAssetsDataParser;
        private readonly IEventAggregator eventAggregator;

        public SearchServiceBridge(IConfigurationService configurationService, IXmlAssetsDataParser xmlAssetsDataParser, IEventAggregator eventAggregator)
        {
            if (HtmlPage.IsEnabled)
            {
                HtmlPage.RegisterScriptableObject("SearchServiceBridge", this);
            }

            this.configurationService = configurationService;

            this.xmlAssetsDataParser = xmlAssetsDataParser;
            this.xmlAssetsDataParser.ResultsAvailable += this.OnXmlAssetsDataParserResultsAvailable;

            this.eventAggregator = eventAggregator;
        }

        public event EventHandler<RCE.Infrastructure.DataEventArgs<List<Asset>>> ResultsAvailable;

        public void OpenPopup()
        {
            // Uri popupUri = this.configurationService.GetParameterValueAsUri("SearchPopupUrl");
            // int height = this.configurationService.GetParameterValueAsInt("SearchPopupHeight").GetValueOrDefault(200);
            // int width = this.configurationService.GetParameterValueAsInt("SearchPopupWidth").GetValueOrDefault(500);
            // HtmlPopupWindowOptions options = new HtmlPopupWindowOptions
            // {
            // Height = height,
            // Width = width,
            // Location = false,
            // Resizeable = true,
            // Scrollbars = true,
            // Toolbar = false,
            // Status = false
            // };
            // HtmlPage.PopupWindow(popupUri, "_blank", options);
            ScriptObject scriptObject = HtmlPage.Window.Eval("RCEBridge") as ScriptObject;

            if (scriptObject != null)
            {
                scriptObject.Invoke("OpenSearchPanel");
            }

            // HtmlPage.Window.Invoke("RCEBridge.OpenSearchPanel");
        }

        [ScriptableMember]
        public void SetSearchResults(string results)
        {
            try
            {
                this.eventAggregator.GetEvent<AssetsLoadingEvent>().Publish(null);

                results = HttpUtility.HtmlDecode(results);

                if (this.configurationService.GetEdgeTimeBeaconUri() != null)
                {
                    this.GetEdgeCdnTokenTimes(results);
                }
                else
                {
                    this.xmlAssetsDataParser.ParseAssets(results, 0);
                }
            }
            catch (XmlException exception)
            {
                this.OnResultsAvailable(new List<Asset>(), exception);
            }
        }

        private void GetEdgeCdnTokenTimes(string results)
        {
            WebClient wc = new WebClient();
            Uri requestUri = this.configurationService.GetEdgeTimeBeaconUri();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler((s, e) => this.GetEdgeCdnTimeCompleted(results, e));
            wc.DownloadStringAsync(requestUri);
        }

        private void OnXmlAssetsDataParserResultsAvailable(object sender, RCE.Infrastructure.DataEventArgs<List<Asset>> e)
        {
            Dispatcher dispatcher = Deployment.Current.Dispatcher;

            if (dispatcher != null)
            {
                dispatcher.BeginInvoke(() => this.OnResultsAvailable(e.Data, e.Error));
            }
            else
            {
                this.OnResultsAvailable(e.Data, e.Error);
            }
        }

        private void GetEdgeCdnTimeCompleted(string results, System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            StringReader stream = new StringReader(e.Result);
            XmlReader reader = XmlReader.Create(stream);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("tm"))
                {
                    var cdnTime = reader.ReadElementContentAsLong();
                    this.xmlAssetsDataParser.ParseAssets(results, cdnTime);
                }
            }
        }

        private void OnResultsAvailable(List<Asset> assets, Exception exception)
        {
            EventHandler<RCE.Infrastructure.DataEventArgs<List<Asset>>> handler = this.ResultsAvailable;
            if (handler != null)
            {
                handler(this, new RCE.Infrastructure.DataEventArgs<List<Asset>>(assets, exception));
            }
        }
    }
}