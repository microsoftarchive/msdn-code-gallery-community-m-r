// <copyright file="TitleTemplate.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TitleTemplate.cs                     
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
    using System.ComponentModel;

    /// <summary>
    /// Specifies the properties of the title templated.
    /// </summary>
    public class TitleTemplate : INotifyPropertyChanged
    {
        /// <summary>
        /// Main text of the title.
        /// </summary>
        private string mainText;

        /// <summary>
        /// Main sub text of the title.
        /// </summary>
        private string subText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleTemplate"/> class.
        /// </summary>
        public TitleTemplate()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The unique identifier for the <see cref="TitleTemplate"/>.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the provider URI.
        /// </summary>
        /// <value>The provider URI.</value>
        public Uri ProviderUri { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title of the <see cref="TitleTemplate"/>.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the main text.
        /// </summary>
        /// <value>The main text.</value>
        public string MainText
        {
            get
            {
                return this.mainText;
            }

            set
            {
                this.mainText = value;
                this.OnPropertyChanged("MainText");
            }
        }

        /// <summary>
        /// Gets or sets the sub text.
        /// </summary>
        /// <value>The sub text.</value>
        public string SubText
        {
            get
            {
                return this.subText;
            }

            set
            {
                this.subText = value;
                this.OnPropertyChanged("SubText");
            }
        }

        /// <summary>
        /// Gets or sets the xaml resource.
        /// </summary>
        /// <value>The xaml resource.</value>
        public string XamlResource { get; set; }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
