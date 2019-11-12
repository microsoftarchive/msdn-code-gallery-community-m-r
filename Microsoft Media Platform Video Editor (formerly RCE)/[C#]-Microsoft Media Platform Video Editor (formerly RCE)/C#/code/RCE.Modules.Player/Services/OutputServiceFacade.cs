// <copyright file="outputServiceFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: outputServiceFacade.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Services
{
    using System;
    using System.ServiceModel;

    using RCE.Infrastructure.Services;
    using RCE.Modules.Player.Models;
    using RCE.Modules.Player.OutputService;

    public class OutputServiceFacade : IOutputServiceFacade
    {
        private readonly ILogger logger;
        private readonly string serviceAddress;

        public OutputServiceFacade(IConfigurationService configurationService, ILogger logger)
        {
            this.logger = logger;
            this.serviceAddress = configurationService.GetParameterValue("OutputServiceUrl");
        }

        public event EventHandler<OutputEventArgs> PersistManifestCompleted;

        public void PersistManifestAsync(string manifest, object userState)
        {
            OutputServiceClient client = this.GetClient();
            client.PersistManifestCompleted += this.Client_PersistManifestCompleted;
            client.PersistManifestAsync(manifest, userState);
        }

        private void Client_PersistManifestCompleted(object sender, PersistManifestCompletedEventArgs e)
        {
            bool generationCompleted = true;

            string result = string.Empty;

            if (e.Error != null)
            {
                generationCompleted = false;

                this.logger.Log(this.GetType().Name, e.Error);
            }
            else
            {
                result = e.Result;
            }

            this.OnPersistManifestCompleted(generationCompleted, result, e.UserState);   
        }

        private void OnPersistManifestCompleted(bool value, string result, object userState)
        {
            EventHandler<OutputEventArgs> handler = this.PersistManifestCompleted;
            if (handler != null)
            {
                handler(this, new OutputEventArgs { Generated = value, Result = result, State = userState });
            }
        }

        private OutputServiceClient GetClient()
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
            {
                Name = "OutputServiceBinding",
                MaxReceivedMessageSize = 2147483647,
                MaxBufferSize = 2147483647,
            };

            EndpointAddress endpointAddress = new EndpointAddress(this.serviceAddress);

            return new OutputServiceClient(binding, endpointAddress);
        }
    }
}