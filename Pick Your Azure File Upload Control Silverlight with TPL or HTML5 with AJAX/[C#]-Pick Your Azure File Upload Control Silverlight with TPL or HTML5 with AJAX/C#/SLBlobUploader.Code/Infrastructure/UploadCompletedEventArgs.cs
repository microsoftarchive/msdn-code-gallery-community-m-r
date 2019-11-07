//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadCompletedEventArgs.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
namespace SLBlobUploader.Code.Infrastructure
{
    using System;

    /// <summary>
    /// Event arguments for upload completed event.
    /// </summary>
    public class UploadCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the uploaded file.
        /// </summary>
        /// <value>The uploaded file.</value>
        public UserFile UploadedFile
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public UploadCompleteReason Reason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage
        {
            get;
            set;
        }
    }
}
