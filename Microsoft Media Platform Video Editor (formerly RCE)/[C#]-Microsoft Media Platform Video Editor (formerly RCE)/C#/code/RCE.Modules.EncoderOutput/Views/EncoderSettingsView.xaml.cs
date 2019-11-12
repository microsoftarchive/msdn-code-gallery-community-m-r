// <copyright file="EncoderSettingsView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: EncoderSettingsView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.EncoderOutput.Views
{
    using System.Windows.Controls;

    public partial class EncoderSettingsView : UserControl, IEncoderSettingsView
    {
        public EncoderSettingsView()
        {
            InitializeComponent();
        }

        public IEncoderSettingsPresentationModel Model
        {
            get { return this.DataContext as IEncoderSettingsPresentationModel; }
            set { this.DataContext = value; }
        }

        /// <summary>
        /// Shows a progress bar.
        /// </summary>
        public void ShowProgressBar()
        {
            this.Spinner.Visibility = System.Windows.Visibility.Visible;
            this.MessageTextBox.Visibility = System.Windows.Visibility.Visible;
            this.NoteTextBox.Visibility = System.Windows.Visibility.Collapsed;
            this.Spinner.BeginAnimation();
        }

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        public void HideProgressBar()
        {
            this.Spinner.Visibility = System.Windows.Visibility.Collapsed;
            this.NoteTextBox.Visibility = System.Windows.Visibility.Visible;
            this.Spinner.StopAnimation();
        }
    }
}
