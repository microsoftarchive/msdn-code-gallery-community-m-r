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

namespace RCE.Modules.MediaBin
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;

    using SMPTETimecode;

    /// <summary>
    /// View that allows to search for metadata.
    /// </summary>
    public partial class MetadataView : UserControl
    {
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

        /// <summary>
        /// Occurs when a metadata item is being selected.
        /// </summary>
        public event EventHandler<DataEventArgs<EventData>> MetadataSelected;

        /// <summary>
        /// Gets or sets the presentation model associated with the view.
        /// </summary>
        /// <value>The presentation model associated with the view.</value>
        private MetadataViewPresentationModel Model
        {
            get { return this.DataContext as MetadataViewPresentationModel; }
            set { this.DataContext = value; }
        }

        /// <summary>
        /// Sets the current in stream data to the model.
        /// </summary>
        /// <param name="logEntryCollection">The in stream data.</param>
        public void SetInStreamData(ILogEntryCollection logEntryCollection)
        {
            this.Model.SetInStreamData(logEntryCollection);
        }

        /// <summary>
        /// Sets the start offset to the model.
        /// </summary>
        /// <param name="startOffset">The start offset.</param>
        public void SetStartOffset(TimeCode startOffset)
        {
            this.Model.StartOffset = startOffset;
        }

        /// <summary>
        /// Handles the SelectionChangedEvent of the listbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                EventData eventData = e.AddedItems[0] as EventData;

                if (eventData != null)
                {
                    this.OnMetadataSelected(eventData);
                }
            }
        }

        /// <summary>
        /// Raises the MetadataSelected event.
        /// </summary>
        /// <param name="eventData">The metadata being added on the event args.</param>
        private void OnMetadataSelected(EventData eventData)
        {
            EventHandler<DataEventArgs<EventData>> selected = this.MetadataSelected;
            if (selected != null)
            {
                selected(this, new DataEventArgs<EventData>(eventData));
            }
        }
    }
}
