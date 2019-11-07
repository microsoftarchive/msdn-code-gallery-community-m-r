// <copyright file="IOutputService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IOutputGeneratorService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System.ServiceModel;

    /// <summary>
    /// Defines the interface for the encoder service.
    /// </summary>
    [ServiceContract]
    public interface IOutputService
    {
        /// <summary>
        /// Saves the project on the server.
        /// </summary>
        /// <param name="project">The project being saved.</param>
        [OperationContract]
        void EnqueueJob(Project project);

        [OperationContract]
        string GenerateCompositeStreamManifest(Project project, string pbpDataStreamName, string adsDataStreamName, bool compressManifest, string gapUriString, string gapCmsId, string gapAzureId);

        [OperationContract]
        string PersistManifest(string manifest);
    }
}
