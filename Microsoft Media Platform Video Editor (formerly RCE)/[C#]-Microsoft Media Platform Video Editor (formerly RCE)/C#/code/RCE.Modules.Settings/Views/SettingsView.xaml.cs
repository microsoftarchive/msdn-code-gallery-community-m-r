// <copyright file="SettingsView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SettingsView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings
{
    using System;
    using System.Windows.Browser;
    using System.Windows.Controls;

    /// <summary>
    /// Provides the implementation for <see cref="ISettingsView"/>.
    /// </summary>
    public partial class SettingsView : UserControl, ISettingsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsView"/> class.
        /// </summary>
        public SettingsView()
        {
            InitializeComponent();

            HtmlPage.RegisterScriptableObject("Settings", this);
        }

        /// <summary>
        /// Notifies that the value for <see cref="P:Microsoft.Practices.Composite.IActiveAware.IsActive"/> property has changed.
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary>
        /// Gets or sets the <see cref="ISettingsViewPresentationModel"/> presentation model of the view.
        /// </summary>
        /// <value>A <see also="ISettingsViewPresentatinModel"/> that represents the presentation model of the view.</value>
        public ISettingsViewPresentationModel Model
        {
            get
            {
               return this.DataContext as ISettingsViewPresentationModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the object is active; otherwise <see langword="false"/>.
        /// </value>
        public bool IsActive
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                if (value)
                {
                    this.OnIsActiveChanged();
                }
            }
        }

        /// <summary>
        /// An scriptable method that provides an entry point to save te current project. 
        /// </summary>
        [ScriptableMember]
        public void SaveProject()
        {
            this.Model.SaveProject();
        }

        private void OnIsActiveChanged()
        {
            EventHandler handler = this.IsActiveChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
