// <copyright file="Marker.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Marker.cs                     
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
    /// Defines the Marker class.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    [KnownType(typeof(PlayByPlay))]
    public class Marker : INotifyPropertyChanged
    {
        private long time;

        private string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="Marker"/> class.
        /// </summary>
        public Marker()
        {
            this.ID = Guid.NewGuid();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the Marker id.
        /// </summary>
        /// <value>The unique identifier for the Marker.</value>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The Marker text.</value>
        [DataMember]
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.InvokePropertyChanged("Text");
            }
        }

        /// <summary>
        /// Gets or sets the position of the ad opportunity.
        /// </summary>
        /// <value>The absolute time in ticks.</value>
        [DataMember]
        public long Time
        {
            get
            {
                return this.time;
            }

            set
            {
                this.time = value;
                this.InvokePropertyChanged("Time");
                this.InvokePropertyChanged("Seconds");
            }
        }

        public double Seconds 
        { 
            get
            {
                return this.Time / 10000000.0;
            }
        }

        private void InvokePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
