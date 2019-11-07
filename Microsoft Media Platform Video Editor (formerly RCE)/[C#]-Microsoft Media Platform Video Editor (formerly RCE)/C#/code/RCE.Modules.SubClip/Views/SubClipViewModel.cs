// <copyright file="SubClipViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SubClipViewModel.cs                     
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
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Presentation.Events;

    using RCE.Infrastructure;
    using RCE.Infrastructure.DragDrop;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.SubClip.Models;
    using RCE.Services.Contracts;

    public class SubClipViewModel : BaseModel, ISubClipViewModel, IWindowMetadataProvider, IKeyboardAware, IWindowAware
    {
        private readonly ISubClipView subClipView;

        private readonly IEventAggregator eventAggregator;

        private Asset asset;

        private ICommand dropCommand;

        private ObservableCollection<StreamOption> availableAudioStreams;

        private ObservableCollection<StreamOption> availableVideoStreams;

        private VideoAssetInOut videoAssetInOut;

        private object title;

        /// <summary>
        /// Contains the max number of Audio Tracks allowables.
        /// </summary>
        private readonly int maxNumberOfAudioTracks;

        public SubClipViewModel(ISubClipView subClipView, IEventAggregator eventAggregator, IConfigurationService configurationService)
        {
            this.subClipView = subClipView;
            this.eventAggregator = eventAggregator;

            this.maxNumberOfAudioTracks = configurationService.GetParameterValueAsInt("MaxNumberOfAudioTracks").GetValueOrDefault(1);

            this.DropCommand = new DelegateCommand<DropPayload>(this.HandleDrop, this.CanHandleDrop);
            this.AudioPreviewSelectionChangedCommand =
                new DelegateCommand<StreamOption>(this.HandleAudioPreviewSelectionChanged);

            this.VideoPreviewSelectionChangedCommand =
                new DelegateCommand<StreamOption>(this.HandleVideoPreviewSelectionChanged);

            this.VideoSequenceSelectionChangedCommand =
                new DelegateCommand<StreamOption>(this.HandleVideoSequenceSelectionChanged);

            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.HandleKeyboardAction);

            this.AudioSequenceSelectionChangedCommand = new DelegateCommand<StreamOption>(this.HandleAudioSequenceSelectionChanged);

            this.Title = "Sub-Clip Tool";

            this.AvailableVideoStreams = new ObservableCollection<StreamOption>();
            this.AvailableAudioStreams = new ObservableCollection<StreamOption>();

            this.eventAggregator.GetEvent<AddPreviewEvent>().Subscribe(this.AddPreview, ThreadOption.PublisherThread, true, this.FilterAddPreviewEvent);
            this.subClipView.SetDataContext(this);

            this.eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        public ObservableCollection<StreamOption> AvailableAudioStreams
        {
            get
            {
                return this.availableAudioStreams;
            }

            set
            {
                this.availableAudioStreams = value;
                this.OnPropertyChanged("AvailableAudioStreams");
            }
        }

        public ObservableCollection<StreamOption> AvailableVideoStreams
        {
            get
            {
                return this.availableVideoStreams;
            }

            set
            {
                this.availableVideoStreams = value;
                this.OnPropertyChanged("AvailableVideoStreams");
            }
        }

        public ISubClipView View
        {
            get
            {
                return this.subClipView;
            }
        }

        public Asset Asset
        {
            get
            {
                return this.asset;
            }

            set
            {
                if (this.asset != value)
                {
                    this.asset = value;
                    this.asset.PropertyChanged += this.HandleAssetPropertyChanged;
                    this.Title = string.Format("Source: {0}", this.asset.Title);
                    this.OnPropertyChanged("Asset");
                    this.OnPropertyChanged("HasAsset");
                    this.PopulateAudioStreams();
                    this.PopulateVideoStreams();
                }
            }
        }

        public VideoAssetInOut VideoAssetInOut
        {
            get
            {
                return this.videoAssetInOut;
            }

            set
            {
                this.videoAssetInOut = value;

                if (this.videoAssetInOut != null)
                {
                    this.videoAssetInOut.AddMarkersToSequence = true;
                }

                this.OnPropertyChanged("VideoAssetInOut");
            }
        }

        public ICommand DropCommand
        {
            get
            {
                return this.dropCommand;
            }

            set
            {
                if (this.dropCommand != value)
                {
                    this.dropCommand = value;
                    this.OnPropertyChanged("DropCommand");
                }
            }
        }

        public ICommand AudioPreviewSelectionChangedCommand { get; set; }

        public ICommand VideoSequenceSelectionChangedCommand { get; set; }

        public ICommand AudioSequenceSelectionChangedCommand { get; set; }

        public ICommand VideoPreviewSelectionChangedCommand { get; set; }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext 
        {
            get
            {
                return KeyboardActionContext.SubClip;
            }
        }

        public bool IsDisplayed { private get; set; }

        public EventData SelectedMetadataEvent { get; set; }

        public bool HasAsset
        {
            get
            {
                return this.Asset != null;
            }
        }

        public VerticalWindowPosition VerticalPosition
        {
            get
            {
                return VerticalWindowPosition.Top;
            }
        }

        public HorizontalWindowPosition HorizontalPosition
        {
            get
            {
                return HorizontalWindowPosition.Center;
            }
        }

        public object Title
        { 
            get
            {
                return this.title;
            }

            private set
            {
                this.title = value;
                this.RaiseTitleChanged();
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return ResizeDirection.None;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(674, 511);
            }
        }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        public void AddPreview(AddPreviewPayload payload)
        {
            PlayByPlay playByPlayMarker = payload.Value as PlayByPlay;

            if (playByPlayMarker == null)
            {
                return;
            }

            this.videoAssetInOut.AddPlayByPlayMarker(playByPlayMarker);
        }

        private void HandleDrop(DropPayload dropPayload)
        {
            Asset draggedAsset = dropPayload.DraggedItem as Asset;

            if (draggedAsset != null)
            {
                this.Asset = draggedAsset;
            }
        }

        private void HandleAssetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Title")
            {
                this.Title = string.Format("Source: {0}", this.asset.Title);
            }
        }

        private void HandleAudioPreviewSelectionChanged(StreamOption audioStream)
        {
            SmoothStreamingVideoAsset smoothStreamingVideoAsset = this.Asset as SmoothStreamingVideoAsset;
            
            if (smoothStreamingVideoAsset != null)
            {
                this.VideoAssetInOut.PreviewAudioStream = smoothStreamingVideoAsset.AudioStreams.First(a => a.Name == audioStream.Name);
            }
        }

        private void HandleVideoPreviewSelectionChanged(StreamOption videoCamera)
        {
            this.VideoAssetInOut.PreviewVideoCamera = videoCamera.Name;
        }

        private void HandleVideoSequenceSelectionChanged(StreamOption videoCamera)
        {
            this.VideoAssetInOut.SequenceVideoCamera = videoCamera.Name;
        }

        private void HandleAudioSequenceSelectionChanged(StreamOption audioStream)
        {
            if (this.availableAudioStreams.Count(s => s.SequenceSelected) > maxNumberOfAudioTracks + 1)
            {
                audioStream.SequenceSelected = false;
                return;
            }

            SmoothStreamingVideoAsset smoothStreamingVideoAsset = this.Asset as SmoothStreamingVideoAsset;
            
            if (smoothStreamingVideoAsset != null)
            {
                var audio = smoothStreamingVideoAsset.AudioStreams.First(a => a.Name == audioStream.Name);
                if (audioStream.SequenceSelected)
                {
                    this.VideoAssetInOut.SequenceAudioStreams.Add(audio);
                }
                else
                {
                    this.VideoAssetInOut.SequenceAudioStreams.Remove(audio);
                }
            }
        }

        private void RaiseTitleChanged()
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.TitleUpdated;
            if (handler != null)
            {
                handler(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }
        
        private void PopulateAudioStreams()
        {
            this.AvailableAudioStreams.Clear();
            
            SmoothStreamingVideoAsset smoothStreamingVideoAsset = this.Asset as SmoothStreamingVideoAsset;

            if (smoothStreamingVideoAsset != null && smoothStreamingVideoAsset.AudioStreams != null)
            {
                if (smoothStreamingVideoAsset.AudioStreams.Count == 0 || (smoothStreamingVideoAsset.AudioStreams.Count == 1 && string.IsNullOrEmpty(smoothStreamingVideoAsset.AudioStreams[0].Name)))
                {
                    this.AvailableAudioStreams.Add(
                        new StreamOption { Name = "Default", PreviewSelected = false, SequenceSelected = false });
                }
                else
                {
                    foreach (var audioStream in smoothStreamingVideoAsset.AudioStreams)
                    {
                        this.AvailableAudioStreams.Add(
                            new StreamOption { Name = audioStream.Name, PreviewSelected = false, SequenceSelected = false });
                    }

                    var firstAudioStream = this.AvailableAudioStreams[0];
                    var audio = smoothStreamingVideoAsset.AudioStreams.First(a => a.Name == firstAudioStream.Name);
                    this.VideoAssetInOut.PreviewAudioStream = audio;
                    this.VideoAssetInOut.SequenceAudioStreams.Add(audio);
                }
            }
            else
            {
                this.AvailableAudioStreams.Add(
                                        new StreamOption { Name = "Default", PreviewSelected = false, SequenceSelected = false });
            }

            var firstAudio = this.AvailableAudioStreams[0];
            firstAudio.PreviewSelected = true;
            firstAudio.SequenceSelected = true;
        }

        private void PopulateVideoStreams()
        {
            this.AvailableVideoStreams.Clear();

            SmoothStreamingVideoAsset smoothStreamingVideoAsset = this.Asset as SmoothStreamingVideoAsset;

            if (smoothStreamingVideoAsset != null && smoothStreamingVideoAsset.VideoStreams != null)
            {
                if (smoothStreamingVideoAsset.VideoStreams.Count == 0 || (smoothStreamingVideoAsset.VideoStreams.Count == 1 && string.IsNullOrEmpty(smoothStreamingVideoAsset.VideoStreams[0])))
                {
                    this.AvailableVideoStreams.Add(
                        new StreamOption { Name = "Default", PreviewSelected = false, SequenceSelected = false });
                }
                else
                {
                    foreach (var videoStream in smoothStreamingVideoAsset.VideoStreams)
                    {
                        this.AvailableVideoStreams.Add(
                            new StreamOption { Name = videoStream, PreviewSelected = false, SequenceSelected = false });
                    }

                    var firstVideoStream = this.AvailableVideoStreams[0];
                    this.VideoAssetInOut.PreviewVideoCamera = firstVideoStream.Name;
                    this.VideoAssetInOut.SequenceVideoCamera = firstVideoStream.Name;
                }
            }
            else
            {
                this.AvailableVideoStreams.Add(
                        new StreamOption { Name = "Default", PreviewSelected = false, SequenceSelected = false });
            }

            var firstVideoCamera = this.AvailableVideoStreams[0];
            firstVideoCamera.PreviewSelected = true;
            firstVideoCamera.SequenceSelected = true;
        }

        private void HandleKeyboardAction(Tuple<KeyboardAction, object> obj)
        {
            this.eventAggregator.GetEvent<VideoPreviewTimelineEvent>().Publish(obj.Item1);
        }

        private bool CanHandleDrop(DropPayload dropPayload)
        {
            return this.IsDisplayed && (dropPayload.DraggedItem != this.VideoAssetInOut);
        }

        private bool FilterAddPreviewEvent(AddPreviewPayload payload)
        {
            return payload.Source == CommentMode.SubClip;
        }
    }
}