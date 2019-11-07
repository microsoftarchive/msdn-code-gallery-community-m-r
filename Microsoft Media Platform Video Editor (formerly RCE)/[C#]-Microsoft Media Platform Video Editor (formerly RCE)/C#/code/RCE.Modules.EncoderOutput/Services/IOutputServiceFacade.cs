// <copyright file="IOutputServiceFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IOutputServiceFacade.cs                     
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

    using Infrastructure.Models;

    using RCE.Modules.EncoderOutput.Models;

    public interface IOutputServiceFacade
    {
        event EventHandler<OutputEventArgs> GenerateOuputCompleted;

        event EventHandler<OutputEventArgs> GenerateCompositestreamManifestCompleted;
        
        void GenerateOutputAsync(Project project);

        void GenerateCompositeStreamManifestAsync(Project project, string pbpDataStreamName, string adsDataStreamName, bool compressManifest, string gapUriString, string gapCmsId, string gapAzureId);
    }
}
