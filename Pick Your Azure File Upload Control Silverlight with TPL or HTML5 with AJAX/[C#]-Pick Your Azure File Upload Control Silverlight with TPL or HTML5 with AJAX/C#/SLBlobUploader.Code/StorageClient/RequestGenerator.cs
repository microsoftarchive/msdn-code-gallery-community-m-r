//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestGenerator.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
namespace SLBlobUploader.Code.StorageClient
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    /// <summary>
    /// Helper class for generating requests of file upload operations.
    /// </summary>
    internal sealed class RequestGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestGenerator"/> class.
        /// </summary>
        /// <param name="uploadUrl">The upload URL.</param>
        /// <param name="fileName">Name of the file.</param>
        internal RequestGenerator(Uri uploadUrl, string fileName)
        {
            var uriBuilder = new UriBuilder(uploadUrl);
            uriBuilder.Path += string.Format(CultureInfo.InvariantCulture, "/{0}", fileName);
            this.SASUrl = uriBuilder.Uri;
        }

        /// <summary>
        /// Gets the SAS URL.
        /// </summary>
        /// <value>The SAS URL.</value>
        internal Uri SASUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Uncommitted block BLOB list.
        /// </summary>
        /// <param name="uncommittedBlocksUpperLimit">The uncommitted blocks upper limit.</param>
        /// <returns>Document containing uncommitted block list</returns>
        internal static XDocument UncommittedBlockBlobList(int uncommittedBlocksUpperLimit)
        {
            int[] blockList = Enumerable.Range(1, uncommittedBlocksUpperLimit).ToArray<int>();
            var uncommitedBlocks = from blockId in blockList
                                   select new XElement("Uncommitted", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format(CultureInfo.InvariantCulture, "{0:D4}", blockId))));
            return new XDocument(new XElement("BlockList", uncommitedBlocks));
        }

        /// <summary>
        /// Gets the block BLOB URI.
        /// </summary>
        /// <param name="blockId">The block id.</param>
        /// <returns>Uri of block blob to upload</returns>
        internal Uri GetBlockBlobUri(string blockId)
        {
            var uriBuilder = new UriBuilder(this.SASUrl);
            uriBuilder.Query = uriBuilder.Query.TrimStart('?') + string.Format(CultureInfo.InvariantCulture, "&comp=block&blockid={0}", blockId);
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Puts the block BLOB list URI.
        /// </summary>
        /// <returns>Put request for committing blocks</returns>
        internal Uri PutBlockBlobListUri()
        {
            var uriBuilder = new UriBuilder(this.SASUrl);
            uriBuilder.Query = uriBuilder.Query.TrimStart('?') + "&comp=blocklist";
            return uriBuilder.Uri;
        }
    }
}
