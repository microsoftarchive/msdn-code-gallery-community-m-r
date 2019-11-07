// <copyright file="TransitionsMetadataView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TransitionsMetadataView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Views
{
    using System.Windows.Controls;

    public partial class TransitionsMetadataView : UserControl, ITransitionsMetadataView
    {
        public TransitionsMetadataView()
        {
            InitializeComponent();
        }

        public ITransitionsMetadataViewPresentationModel Model
        {
            get
            {
                return this.DataContext as ITransitionsMetadataViewPresentationModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        private void OnFadeInTextChanged(object sender, TextChangedEventArgs e)
        {
            this.FadeOut.Focus();
            this.FadeIn.Focus();
        }

        private void OnFadeOutTextChanged(object sender, TextChangedEventArgs e)
        {
            this.FadeIn.Focus();
            this.FadeOut.Focus();
        }
    }
}
