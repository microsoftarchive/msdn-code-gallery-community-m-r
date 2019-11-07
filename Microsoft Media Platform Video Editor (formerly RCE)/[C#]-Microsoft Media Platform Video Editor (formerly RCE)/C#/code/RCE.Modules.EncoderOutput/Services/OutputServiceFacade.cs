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

namespace RCE.Modules.EncoderOutput.Services
{
    using System;
    using System.ServiceModel;
    using Infrastructure;
    using Infrastructure.Models;
    using Infrastructure.Translators;

    using RCE.Infrastructure.Services;
    using RCE.Modules.EncoderOutput.Models;
    using RCE.Modules.EncoderOutput.OutputService;

    public class OutputServiceFacade : IOutputServiceFacade
    {
        private readonly ILogger logger;
        private readonly string serviceAddress;

        public OutputServiceFacade(IConfigurationService configurationService, ILogger logger)
        {
            this.logger = logger;
            this.serviceAddress = configurationService.GetParameterValue("OutputServiceUrl");
        }

        public event EventHandler<OutputEventArgs> GenerateOuputCompleted;

        public event EventHandler<OutputEventArgs> GenerateCompositestreamManifestCompleted;

        public void GenerateOutputAsync(Project project)
        {
            OutputServiceClient client = this.GetClient();
            client.EnqueueJobCompleted += this.Client_EnqueueJobCompleted;

            RCE.Services.Contracts.Project dataProject = DataServiceTranslator.ConvertToDataServiceProject(project);

            client.EnqueueJobAsync(dataProject);
        }

        public void GenerateCompositeStreamManifestAsync(Project project, string pbpDataStreamName, string adsDataStreamName, bool compressManifest, string manifestUri, string gapCmsId, string gapAzureId)
        {
            OutputServiceClient client = this.GetClient();
            client.GenerateCompositeStreamManifestCompleted += this.Client_GenerateCompositeStreamManifestCompleted;

            RCE.Services.Contracts.Project dataProject = DataServiceTranslator.ConvertToDataServiceProject(project);

            client.GenerateCompositeStreamManifestAsync(dataProject, pbpDataStreamName, adsDataStreamName, compressManifest, manifestUri, gapCmsId, gapAzureId);
        }

        private void Client_EnqueueJobCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            bool generationCompleted = true;

            if (e.Error != null)
            {
                generationCompleted = false;

                this.logger.Log(this.GetType().Name, e.Error);
            }

            this.OnGenerateOutputCompleted(generationCompleted);
        }

        private void Client_GenerateCompositeStreamManifestCompleted(object sender, GenerateCompositeStreamManifestCompletedEventArgs e)
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

            this.OnGenerateCompositestreamManifestCompleted(generationCompleted, result);   
        }

        private void OnGenerateOutputCompleted(bool value)
        {
            EventHandler<OutputEventArgs> completed = this.GenerateOuputCompleted;
            if (completed != null)
            {
                completed(this, new OutputEventArgs { Generated = value });
            }
        }

        private void OnGenerateCompositestreamManifestCompleted(bool value, string result)
        {
            EventHandler<OutputEventArgs> handler = this.GenerateCompositestreamManifestCompleted;
            if (handler != null)
            {
                handler(this, new OutputEventArgs { Generated = value, Result = result });
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