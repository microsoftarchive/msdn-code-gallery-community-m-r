// <copyright file="Anchor.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Anchor.cs                     
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
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Base class for defining an mark-in/out points.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public class Anchor : RceObject
    {
        /// <summary>
        /// Gets or sets the mark-in point of the comment.
        /// </summary>
        /// <value>The mark-in point of the comment.</value>
        [DataMember]
        public double? MarkIn { get; set; }

        /// <summary>
        /// Gets or sets the mark-out point of the comment.
        /// </summary>
        /// <value>The mark-out point of the comment.</value>
        [DataMember]
        public double? MarkOut { get; set; }
    }
}