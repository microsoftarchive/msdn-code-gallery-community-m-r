// <copyright file="MetadataView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataView.xaml.cs                     
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
    using System.Windows;
    using System.Windows.Controls;

    using RCE.Infrastructure;

    using SMPTETimecode;

    /// <summary>
    /// View that allows to search for metadata.
    /// </summary>
    public partial class MetadataView : UserControl
    {
        /// <summary>
        /// DependencyProperty for Asset.
        /// </summary>
        private static readonly DependencyProperty InStreamDataProperty =
                DependencyProperty.RegisterAttached("InStreamData", typeof(ILogEntryCollection), typeof(MetadataView), new PropertyMetadata(InStreamDataChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataView"/> class.
        /// </summary>
        public MetadataView()
        {
            InitializeComponent();

            if (!DesignerProperties.IsInDesignMode)
            {
                MetadataViewPresentationModel model = new MetadataViewPresentationModel();

                this.Model = model;
            }
        }

        public ILogEntryCollection InStreamData
        {
            get
            {
                return GetInStreamData(this);
            }

            set
            {
                SetInStreamData(this, value);
            }
        }

        /// <summary>
        /// Gets or sets the presentation model associated with the view.
        /// </summary>
        /// <value>The presentation model associated with the view.</value>
        private MetadataViewPresentationModel Model
        {
            get { return this.DataContext as MetadataViewPresentationModel; }
            set { this.DataContext = value; }
        }

        public static ILogEntryCollection GetInStreamData(DependencyObject obj)
        {
            return obj.GetValue(InStreamDataProperty) as ILogEntryCollection;
        }

        public static void SetInStreamData(DependencyObject obj, ILogEntryCollection value)
        {
            obj.SetValue(InStreamDataProperty, value);
        }

        /// <summary>
        /// Sets the start offset to the model.
        /// </summary>
        /// <param name="startOffset">The start offset.</param>
        public void SetStartOffset(TimeCode startOffset)
        {
            this.Model.StartOffset = startOffset;
        }

        private static void InStreamDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var metadataView = d as MetadataView;
            var streamCollection = e.NewValue as ILogEntryCollection;
            if (metadataView != null && streamCollection != null)
            {
                metadataView.SetInStreamData(streamCollection);
            }
        }

        /// <summary>
        /// Sets the current in stream data to the model.
        /// </summary>
        /// <param name="streamData">The in stream data.</param>
        private void SetInStreamData(ILogEntryCollection streamData)
        {
            this.Model.SetInStreamData(streamData);
        }

        private void SearchTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // stop propagation of the keystroke
            e.Handled = true;
        }
    }
}
