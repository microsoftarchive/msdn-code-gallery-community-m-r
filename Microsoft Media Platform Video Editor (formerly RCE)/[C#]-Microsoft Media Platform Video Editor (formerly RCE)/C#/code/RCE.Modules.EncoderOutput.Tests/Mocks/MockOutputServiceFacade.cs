// <copyright file="MockOutputServiceFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockOutputServiceFacade.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.EncoderOutput.Tests.Mocks
{
    using System;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Modules.EncoderOutput.Models;

    using Services;

    public class MockOutputServiceFacade : IOutputServiceFacade
    {
        public event EventHandler<OutputEventArgs> GenerateOuputCompleted;

        public event EventHandler<OutputEventArgs> GenerateCompositestreamManifestCompleted;

        public bool GenerateOutputCalled { get; set; }

        public Project GenerateOutputArgument { get; set; }

        public bool GenerateCompositeStreamManifestCalled { get; set; }

        public Project GenerateCompositeStreamManifestProjectArgument { get; set; }

        public string GenerateCompositeStreamManifestPbpArgument { get; set; }

        public string GenerateCompositeStreamManifestAdsArgument { get; set; }

        public bool GenerateCompositeStreamManifestCompressManifestArgument { get; set; }

        public void GenerateOutputAsync(Project project)
        {
            this.GenerateOutputCalled = true;
            this.GenerateOutputArgument = project;
        }

        public void GenerateCompositeStreamManifestAsync(Project project, string pbpDataStreamName, string adsDataStreamName, bool compressManifest, string gapUriString, string gapCmsId, string gatAzureId)
        {
            this.GenerateCompositeStreamManifestCalled = true;
            this.GenerateCompositeStreamManifestProjectArgument = project;
            this.GenerateCompositeStreamManifestPbpArgument = pbpDataStreamName;
            this.GenerateCompositeStreamManifestAdsArgument = adsDataStreamName;
            this.GenerateCompositeStreamManifestCompressManifestArgument = compressManifest;
        }

        public void InvokeGenerateOuputCompleted(bool generated)
        {
            EventHandler<OutputEventArgs> handler = this.GenerateOuputCompleted;
            if (handler != null)
            {
                handler(this, new OutputEventArgs { Generated = generated });
            }
        }

        public void InvokeGenerateCompositestreamManifestCompleted(bool generated, string result)
        {
            EventHandler<OutputEventArgs> handler = this.GenerateCompositestreamManifestCompleted;
            if (handler != null)
            {
                handler(this, new OutputEventArgs { Generated = generated, Result = result });
            }
        }
    }
}