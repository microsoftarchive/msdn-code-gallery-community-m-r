//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
[assembly: System.CLSCompliant(true)]
namespace SLBlobUploader.Code.Infrastructure
{
    /// <summary>
    /// Reason of upload completion
    /// </summary>
    public enum UploadCompleteReason
    {
        /// <summary>
        /// All blocks were committed successfully
        /// </summary>
        UploadCommitted = 0,

        /// <summary>
        /// Error occurred while uploading blocks
        /// </summary>
        ErrorOccurred = 1,

        /// <summary>
        /// User canceled upload of file
        /// </summary>
        UserCanceled = 2
    }

    /// <summary>
    /// Request types that can be sent to Azure
    /// </summary>
    internal enum RequestType
    {
        /// <summary>
        /// GET request
        /// </summary>
        Get = 0,

        /// <summary>
        /// PUT request
        /// </summary>
        Put = 1,

        /// <summary>
        /// DELETE request
        /// </summary>
        Delete = 2
    }

    /// <summary>
    /// Constants used by the application.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Maximum file size allowed to upload.
        /// </summary>
        public const long MaxFileSizeKB = 204800;
        ////public const int MaxConcurrencyLevel = 70000;

        /// <summary>
        /// Gets the size of the chunk to upload.
        /// </summary>
        /// <value>The size of the chunk.</value>
        internal static int ChunkSize
        {
            get
            {
                return 1024 * 1024;
            }
        }

        /// <summary>
        /// Gets the retry limit.
        /// </summary>
        /// <value>The retry limit.</value>
        internal static int RetryLimit
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// Gets the x_ MS_ version.
        /// </summary>
        /// <value>The X_MS_Version.</value>
        internal static string HeaderVersion
        {
            get
            {
                return "2009-09-19";
            }
        }
    }
}
