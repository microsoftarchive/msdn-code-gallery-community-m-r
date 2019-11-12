// <copyright file="SequencesView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SequencesView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Views
{
    using System.Windows.Data;

    using RCE.Infrastructure;

    public partial class SequencesView : ISequencesView
    {
        public SequencesView()
        {
            this.Loaded += this.HandleLoad;
            InitializeComponent();
        }

        public void SetViewModel(object viewModel)
        {
            this.DataContext = viewModel;
        }

        private void HandleLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SetUpCommandBindings();
        }

        private void SetUpCommandBindings()
        {
            Binding changeSequenceCommand = new Binding("ChangeCurrentSequenceCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["ChangeCurrentSequenceCommand"]).SetBinding(BindingHelper.ValueProperty, changeSequenceCommand);
        }
    }
}
