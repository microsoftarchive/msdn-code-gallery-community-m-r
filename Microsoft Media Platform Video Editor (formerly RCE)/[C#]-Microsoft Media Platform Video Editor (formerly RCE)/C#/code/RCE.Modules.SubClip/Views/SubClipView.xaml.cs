// <copyright file="SubClipView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SubClipView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.SubClip.Views
{
    using System.Windows.Controls;
    using System.Windows.Data;

    using RCE.Infrastructure;

    public partial class SubClipView : UserControl, ISubClipView
    {
        public SubClipView()
        {
            this.Loaded += this.SubClipViewLoaded;
            InitializeComponent();
        }

        public void SetDataContext(object viewModel)
        {
            this.DataContext = viewModel;
        }

        private void SubClipViewLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SetUpCommandBindings();
        }

        private void SetUpCommandBindings()
        {
            Binding audioPreviewCommand = new Binding("AudioPreviewSelectionChangedCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["AudioPreviewSelectionChangedCommand"]).SetBinding(BindingHelper.ValueProperty, audioPreviewCommand);

            Binding videoPreviewCommand = new Binding("VideoPreviewSelectionChangedCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["VideoPreviewSelectionChangedCommand"]).SetBinding(BindingHelper.ValueProperty, videoPreviewCommand);

            Binding videoSequenceCommand = new Binding("VideoSequenceSelectionChangedCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["VideoSequenceSelectionChangedCommand"]).SetBinding(BindingHelper.ValueProperty, videoSequenceCommand);

            Binding audioSequenceCommand = new Binding("AudioSequenceSelectionChangedCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["AudioSequenceSelectionChangedCommand"]).SetBinding(BindingHelper.ValueProperty, audioSequenceCommand);
        }
    }
}
