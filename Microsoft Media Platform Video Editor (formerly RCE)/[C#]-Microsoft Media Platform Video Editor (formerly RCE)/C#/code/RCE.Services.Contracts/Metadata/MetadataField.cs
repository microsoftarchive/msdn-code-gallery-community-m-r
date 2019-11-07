// <copyright file="MetadataField.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataField.cs                     
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
    using System.ComponentModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a metadata field.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class MetadataField : INotifyPropertyChanged
    {
        private string name;

        private object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataField"/> class.
        /// </summary>
        public MetadataField()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataField"/> class.
        /// </summary>
        /// <param name="name">The name of the metadata field.</param>
        /// <param name="value">The value of the metadata field.</param>
        public MetadataField(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the name of the metadata field.
        /// </summary>
        /// <value>The name of the metadata field.</value>
        [DataMember]
        public string Name
        {
            get
            {
                return this.name;
            }
            
            set
            {
                this.name = value;
                this.InvokePropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the value of the metadata field.
        /// </summary>
        /// <value>The value of the metadata field.</value>
        [DataMember]
        public object Value
        {
            get
            {
                return this.value;
            }
            
            set
            {
                this.value = value;
                this.InvokePropertyChanged("Value");
            }
        }

        private void InvokePropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}