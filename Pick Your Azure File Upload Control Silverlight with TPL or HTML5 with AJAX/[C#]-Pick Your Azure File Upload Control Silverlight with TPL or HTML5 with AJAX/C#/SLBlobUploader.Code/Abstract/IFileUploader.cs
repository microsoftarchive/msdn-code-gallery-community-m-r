//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileUploader.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
namespace SLBlobUploader.Code.Abstract
{
    using System;
    using SLBlobUploader.Code.Infrastructure;

    /// <summary>
    /// Interface for different kind of file uploaders
    /// </summary>
    public interface IFileUploader
    {
        /// <summary>
        /// Occurs when all files have been uploaded.
        /// </summary>
        event EventHandler<UploadCompletedEventArgs> UploadFinished;

        /// <summary>
        /// Starts the upload.
        /// </summary>
        /// <param name="initParameters">The init parameters.</param>
        void StartUpload(string initParameters);

        /// <summary>
        /// Cancels the upload.
        /// </summary>
        void CancelUpload();
    }
}
