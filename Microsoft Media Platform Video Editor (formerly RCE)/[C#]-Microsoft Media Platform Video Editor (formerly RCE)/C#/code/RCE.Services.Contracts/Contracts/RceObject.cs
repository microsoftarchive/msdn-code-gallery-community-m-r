// <copyright file="RceObject.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: RceObject.cs                     
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
    /// An abstract class that all RCE items must inherit from.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public abstract class RceObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RceObject"/> class.
        /// </summary>
        protected RceObject()
        {
            this.IsLoaded = false;
        }

        /// <summary>
        /// Gets or sets the date/time that the object was created.
        /// </summary>
        /// <value>The DateTime that the object was created.</value>
        [DataMember]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the creator of the object.
        /// </summary>
        /// <value>The creator of the object.</value>
        [DataMember]
        public string Creator { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Id"/> of the object.
        /// </summary>
        /// <value>The id of the object.</value>
        [DataMember]
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the object has been loaded with data.
        /// </summary>
        /// <value>A true if the object was loaded;otherwise false.</value>
        [DataMember]
        public bool IsLoaded { get; set; }

        /// <summary>
        /// Gets or sets the last time the object was modified.
        /// </summary>
        /// <value>The last time the object was modified.</value>
        [DataMember]
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets the user that performed the last modification.
        /// </summary>
        /// <value>The user that performed the last modification.</value>
        [DataMember]
        public string ModifiedBy { get; set; }
    }
}
