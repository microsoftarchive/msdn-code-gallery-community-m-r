// <copyright file="Container.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Container.cs                     
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
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// An abstract class that is used to define a container.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    [KnownType(typeof(MediaBin))]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class Container : RceObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class with the default values.
        /// </summary>
        public Container()
        {
            this.Items = new ItemCollection();
            this.Containers = new ContainerCollection();
        }

        /// <summary>
        /// Gets or sets the description of the container.
        /// </summary>
        /// <value>The description of the container.</value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the title of the container.
        /// </summary>
        /// <value>The title of the container.</value>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Collection{Item}"/> that are contained in the <see cref="MediaBin"/>.
        /// </summary>
        /// <value>The collection of items.</value>
        [DataMember]
        public ItemCollection Items { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Collection{Container}"/>.
        /// </summary>
        /// <value>The collection of containers.</value>
        [DataMember]
        public ContainerCollection Containers { get; set; }
    }
}
