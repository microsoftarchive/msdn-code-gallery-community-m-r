// <copyright file="Audit.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Audit.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;

    /// <summary>
    /// Base class that provides audit members.
    /// </summary>
    public abstract class Audit : BaseModel
    {
        /// <summary>
        /// Specifies the creator of the object.
        /// </summary>
        private string creator;

        /// <summary>
        /// Gets or sets the date/time that the object was created.
        /// </summary>
        /// <value>The datetime that the object was created.</value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the creator of the object.
        /// </summary>
        /// <value>The creator of the object.</value>
        public string Creator
        {
            get
            {
                return this.creator;
            }

            set
            {
                this.creator = value;
                this.OnPropertyChanged("Creator");
            }
        }

        /// <summary>
        /// Gets or sets the last time the object was modified.
        /// </summary>
        /// <value>The last time the object was modified.</value>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets the user that performed the last modification.
        /// </summary>
        /// <value>The user that performed the last modification.</value>
        public string ModifiedBy { get; set; }
    }
}
