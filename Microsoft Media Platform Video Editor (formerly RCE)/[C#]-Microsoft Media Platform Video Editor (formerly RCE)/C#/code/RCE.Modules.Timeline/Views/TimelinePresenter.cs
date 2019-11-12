// <copyright file="TimelinePresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelinePresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using Infrastructure.DragDrop;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.Timeline.Commands;
    using RCE.Modules.Timeline.Locking;
    using RCE.Modules.Timeline.Models;
    using Services.Contracts;
    using SMPTETimecode;
    using Comment = RCE.Infrastructure.Models.Comment;
    using Project = RCE.Infrastructure.Models.Project;
    using Sequence = RCE.Infrastructure.Models.Sequence;
    using TitleTemplate = RCE.Infrastructure.Models.TitleTemplate;
    using Track = RCE.Infrastructure.Models.Track;
    using TrackType = RCE.Infrastructure.Models.TrackType;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
    using Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming;

    /// <summary>
    /// Presenter for TimeLine View.
    /// </summary>
    public class TimelinePresenter : BaseModel, ITimelinePresenter, IWindowMetadataProvider, IWindowAware
    {
        /// <summary>
        /// The default timeline duration.
        /// </summary>
        public const int DefaultTimelineDuration = 7200;

        /// <summary>
        /// Minimum Element Duration for the asset in the timeline.
        /// </summary>
        public const int MinimumElementDuration = 1;

        /// <summary>
        /// Default Asset Duration (For Images).
        /// </summary>
        public const int DefaultAssetDuration = 10;

        /// <summary>
        /// The <seealso cref="IEventAggregator"/> used to publish and subscribe to events.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// The project service used to interact with the current project.
        /// </summary>
        private readonly IProjectService projectService;

        /// <summary>
        /// Used to manage the undo/redo operations.
        /// </summary>
        private readonly ICaretaker caretaker;

        private readonly ILockGroupManager lockGroupManager;

        /// <summary>
        /// Contains the max number of Audio Tracks allowables.
        /// </summary>
        private readonly int maxNumberOfAudioTracks;

        /// <summary>
        /// Default Timeline Duration.
        /// </summary>
        private readonly int defaultTimelineDuration;

        private Point prePoint = new Point(0, 0);

        private Point nextPoint = new Point(0, 0);

        private bool isRubberBandingEnabled;

        /// <summary>
        /// Contains the current edit mode of the timeline.
        /// </summary>
        private EditMode editMode;

        /// <summary>
        /// Contains a list of layer snapshots used for the undo/redo operatios that involves a track.
        /// </summary>
        private IList<TimelineElement> layerSnapshot;

        private bool channelsConventionEnabled;

        /// <summary>
        /// Contains the current selected elements.
        /// </summary>
        private ICollection<TimelineElement> selectedElements;

        private ISequenceRegistry sequencyRegistry;

        private MoveMarkersCommand moveMarkerscommand;

        private TrimMarkersCommand trimMarkersCommand;

        private ElementPositionType positionType;

        private long uniqueSourceLimit;

        private bool snapToFragmentBoundaries;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelinePresenter"/> class.
        /// </summary>
        /// <param name="view">The <see cref="ITimelineView"/>.</param>
        /// <param name="eventAggregator">The <see cref="IEventAggregator"/>.</param>
        /// <param name="sequenceRegistry">The <see cref="ISequenceRegistry"/>.</param>
        /// <param name="projectService">The <see cref="IProjectService"/>.</param>
        /// <param name="caretaker">The <see cref="ICaretaker"/>.</param>
        /// <param name="configurationService">The <see cref="IConfigurationService"/>.</param>
        /// <param name="lockGroupManager"></param>
        public TimelinePresenter(
            ITimelineView view,
            IEventAggregator eventAggregator,
            ISequenceRegistry sequenceRegistry,
            IProjectService projectService,
            ICaretaker caretaker,
            IConfigurationService configurationService,
            ILockGroupManager lockGroupManager)
        {
            this.positionType = ElementPositionType.None;
            this.caretaker = caretaker;
            this.lockGroupManager = lockGroupManager;
            this.caretaker.SetUndoLevel(configurationService.GetUndoLevel());
            this.eventAggregator = eventAggregator;
            this.sequencyRegistry = sequenceRegistry;
            this.AudioTracks = new ObservableCollection<Track>();
            this.VideoTracks = new ObservableCollection<Track>();
            this.selectedElements = new List<TimelineElement>();
            this.moveMarkerscommand = new MoveMarkersCommand(eventAggregator);
            this.trimMarkersCommand = new TrimMarkersCommand(eventAggregator);

            this.sequencyRegistry.CurrentSequenceChanged += this.HandleSequenceChanged;

            this.projectService = projectService;

            this.eventAggregator.GetEvent<PositionUpdatedEvent>().Subscribe(this.UpdatePlayHead, true);

            this.eventAggregator.GetEvent<EditModeChangedEvent>().Subscribe(this.SetEditingMode, true);

            this.eventAggregator.GetEvent<DeleteMediaBinAssetEvent>().Subscribe(this.DeleteAsset, true);

            this.eventAggregator.GetEvent<SmpteTimeCodeChangedEvent>().Subscribe(this.UpdateSmpteFrameRate, true);

            this.eventAggregator.GetEvent<AddAssetToTimelineEvent>().Subscribe(this.AddAssetAtCurrentPosition, true);

            this.eventAggregator.GetEvent<StartTimeCodeChangedEvent>().Subscribe(this.UpdateStartTimeCode, true);

            this.eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);

            this.editMode = configurationService.GetParameterValueAsBoolean("RippleModeEnabled").GetValueOrDefault() ? EditMode.Ripple : EditMode.Gap;

            this.IsInSnapMode = configurationService.GetParameterValueAsBoolean("SnapModeEnabled").GetValueOrDefault();

            this.maxNumberOfAudioTracks = configurationService.GetParameterValueAsInt("MaxNumberOfAudioTracks").GetValueOrDefault(1);

            this.uniqueSourceLimit =
                configurationService.GetParameterValueAsLong("MaxNumberOfSourcesPerSequence").GetValueOrDefault(
                    long.MaxValue);

            this.uniqueSourceLimit = this.uniqueSourceLimit == 0 ? long.MaxValue : this.uniqueSourceLimit;

            this.AddAudioTrackCommand = new DelegateCommand<object>(this.AddAudioTrack, this.CanAddAudioTrack);
            this.RemoveAudioTrackCommand = new DelegateCommand<object>(this.RemoveAudioTrack, this.CanRemoveAudioTrack);
            this.DropCommand = new DelegateCommand<DropPayload>(this.DropItem, this.CanDropItem);
            this.MoveFrameCommand = new DelegateCommand<object>(this.MoveFrame);
            this.MoveNextClipCommand = new DelegateCommand<object>(this.MoveToNextClip);
            this.MovePreviousClipCommand = new DelegateCommand<object>(this.MoveToPreviousClip);
            this.LockCommand = new DelegateCommand<object>(this.ExecuteLock);
            this.AlignCommand = new DelegateCommand<object>(this.ExecuteAlign);
            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.ExecuteKeyboardAction);
            this.RubberBandingToggleCommand = new DelegateCommand<object>(this.RubberBandingToggleChecked);

            this.View = view;
            this.View.Model = this;
            this.View.ElementPositionChange += this.View_ChangeElementPosition;
            this.View.SingleElementSelect += this.ViewSingleSingleElementSelect;
            this.View.MultipleElementSelect += this.View_MultipleElementSelect;
            this.View.ElementDelete += (s, e) => this.DeleteElement(e.Data);
            this.View.PositionChange += this.View_PositionChange;
            this.View.ShowingLinks += this.View_ShowingLinks;
            this.View.HidingLinks += this.View_HidingLinks;
            this.View.LinkingElement += this.View_LinkingElement;
            this.View.MovingPlayHead += this.View_MovingPlayhead;
            this.View.TopBarDoubleClicked += this.View_TopBarDoubleClicked;
            this.View.RefreshingElements += this.View_RefreshingElements;
            this.View.StartMoving += this.View_StartMoving;
            this.View.StopMoving += this.View_StopMoving;
            this.View.BalanceChanged += this.View_BalanceChanged;
            this.View.VolumeChanged += this.View_VolumeChanged;
            this.View.MuteChanged += this.View_MuteChanged;
            this.View.ElementLocked += this.View_ElementLocked;
            this.View.ElementUnlocked += this.View_ElementUnlocked;

            this.defaultTimelineDuration = configurationService.GetParameterValueAsInt("DefaultTimelineDurationInSeconds").GetValueOrDefault(DefaultTimelineDuration);

            bool timelineHandlersEnabled = configurationService.GetParameterValueAsBoolean("EnableTimelineHandlers").GetValueOrDefault(true);

            this.View.UpdateTimelineHandlers(timelineHandlersEnabled);

            this.channelsConventionEnabled = configurationService.GetParameterValueAsBoolean("EnableChannelsConvention").GetValueOrDefault(false);

            this.LoadTimeline();

            this.sequencyRegistry.CurrentSequenceChanged += this.CurrentSequenceChangedHandler;

            this.IsRubberBandingEnabled = true;

            var configurationSerive = ServiceLocator.Current.GetInstance<IConfigurationService>();
            if (configurationSerive != null)
            {
                this.snapToFragmentBoundaries = configurationSerive.GetSnapToFragmentBoundaries();
            }
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        public DelegateCommand<object> RubberBandingToggleCommand { get; set; }

        public DelegateCommand<object> AlignCommand { get; set; }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <value>The <see cref="ITimelineView"/>.</value>
        public ITimelineView View { get; private set; }

        public bool IsDisplayed { private get; set; }

        public bool IsRubberBandingEnabled
        {
            get
            {
                return this.isRubberBandingEnabled;
            }

            set
            {
                this.isRubberBandingEnabled = value;
                this.OnPropertyChanged("IsRubberBandingEnabled");
            }
        }

        /// <summary>
        /// Gets the command to add audio tracks.
        /// </summary>
        /// <value>The delegate command used to add audio tracks.</value>
        public DelegateCommand<object> AddAudioTrackCommand { get; private set; }

        /// <summary>
        /// Gets the command to remove audio tracks.
        /// </summary>
        /// <value>The delegate command used to remove audio tracks.</value>
        public DelegateCommand<object> RemoveAudioTrackCommand { get; private set; }

        /// <summary>
        /// Gets the command executed on drop elements.
        /// </summary>
        /// <value>The delegate command used to drop the elements.</value>
        public DelegateCommand<DropPayload> DropCommand { get; private set; }

        /// <summary>
        /// Gets the command to move frame backward and forward.
        /// </summary>
        /// <value>The move frame command.</value>
        public DelegateCommand<object> MoveFrameCommand { get; private set; }

        /// <summary>
        /// Gets the command to move to the next clip.
        /// </summary>
        /// <value>The command to move to the next clip.</value>
        public DelegateCommand<object> MoveNextClipCommand { get; private set; }

        /// <summary>
        /// Gets the command to move to the previous clip.
        /// </summary>
        /// <value>The command to move to the previous clip.</value>
        public DelegateCommand<object> MovePreviousClipCommand { get; private set; }

        /// <summary>
        /// Gets the list of available audio tracks.
        /// </summary>
        /// <value>The list of available audio tracks.</value>
        public ObservableCollection<Track> AudioTracks { get; private set; }

        public ObservableCollection<Track> VideoTracks { get; private set; }

        public DelegateCommand<object> LockCommand { get; set; }

        public VerticalWindowPosition VerticalPosition
        {
            get
            {
                return VerticalWindowPosition.Bottom;
            }
        }

        public HorizontalWindowPosition HorizontalPosition
        {
            get
            {
                return HorizontalWindowPosition.Left;
            }
        }

        public object Title
        {
            get
            {
                if (this.sequencyRegistry.CurrentSequence != null)
                {
                    return string.Format(
                        "Sequence: {0} (Max number of sources in sequence: {1}. Current: {2}.)",
                        this.sequencyRegistry.CurrentSequence.Name,
                        this.uniqueSourceLimit,
                        this.GetUniqueSources().Count());
                }

                return "Sequence";
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return Infrastructure.Windows.ResizeDirection.Horizontal;
            }
        }

        public Size Size
        {
            get
            {
                return System.Windows.Size.Empty;
            }
        }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                return KeyboardActionContext.Timeline;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the timeline is in snap mode or not.
        /// </summary>
        /// <value>A true if the timeline is in snap mode;otherwise false.</value>
        public bool IsInSnapMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the timeline is in ripple mode or not.
        /// </summary>
        /// <value>A true if the timeline is in ripple mode;otherwise false.</value>
        public bool IsInRippleMode
        {
            get
            {
                return this.editMode == EditMode.Ripple;
            }

            set
            {
                EditMode newEditMode = value ? EditMode.Ripple : EditMode.Gap;

                this.NotifyNewEditingMode(newEditMode);
            }
        }

        public bool IsTimelineLocked { get; set; }

        public TimeCode TimelineDuration
        {
            get
            {
                double timelineDuration = 0;
                TimelineElement lastElement;
                foreach (Track track in this.sequencyRegistry.CurrentSequenceModel.Tracks)
                {
                    if (track == null || track.Shots == null || track.Shots.Count == 0)
                    {
                        continue;
                    }

                    lastElement = track.Shots.Where(x => x.Position == track.Shots.Max(y => y.Position)).FirstOrDefault();
                    if (lastElement != null)
                    {
                        double duration = lastElement.Position.TotalSeconds + lastElement.OutPosition.TotalSeconds -
                                          lastElement.InPosition.TotalSeconds;

                        timelineDuration = Math.Max(timelineDuration, duration);
                    }
                }

                return TimeCode.FromSeconds(timelineDuration, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
            }
        }

        public void CurrentSequenceChangedHandler(object sender, Infrastructure.DataEventArgs<ISequenceModel> args)
        {
            TimeCode duration = this.sequencyRegistry.CurrentSequenceModel.Duration;
            this.View.SetDuration(duration);
        }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        public void AlignSelectedElementsToPlayheadPosition()
        {
            TimeCode alignPosition = this.sequencyRegistry.CurrentSequenceModel.CurrentPosition;
            var elements = this.selectedElements.OrderBy(e => e.Position);

            foreach (var element in elements)
            {
                Track track = this.GetElementTrack(element);
                this.layerSnapshot = track.GetMemento();
                this.MoveElement(element, track, alignPosition);
                LayerSnapshotCommand command = new LayerSnapshotCommand(this.sequencyRegistry.CurrentSequenceModel, track, this.layerSnapshot, track.GetMemento());
                this.caretaker.ExecuteCommand(command);

                this.layerSnapshot = null;
            }

            if (alignPosition != this.sequencyRegistry.CurrentSequenceModel.CurrentPosition)
            {
                this.SetCurrentPosition(alignPosition);
            }
        }

        /// <summary>
        /// Filters the dropped item if it has valid arguments.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns>true if the arguments are valid else false.</returns>
        private bool CanDropItem(DropPayload payload)
        {
            return this.IsDisplayed && payload.MouseEventArgs != null && payload.DraggedItem != null && this.IsUnderSourceLimit(payload.DraggedItem);
        }

        private bool IsUnderSourceLimit(object draggedItem)
        {
            var asset = draggedItem as Asset;

            if (asset != null)
            {
                IEnumerable<string> uniqueSources = GetUniqueSources();

                bool underLimit = !(uniqueSources.Count() >= this.uniqueSourceLimit && !uniqueSources.Contains(asset.Source.AbsoluteUri));

                return underLimit;
            }

            return true;
        }

        private IEnumerable<string> GetUniqueSources()
        {
            return this.sequencyRegistry.CurrentSequence.Tracks.SelectMany(t => t.Shots).Select(
                s => s.Asset.Source.AbsoluteUri).Distinct();
        }

        /// <summary>
        /// Sets the editing mode.
        /// </summary>
        /// <param name="mode">The <see cref="EditMode"/>.</param>
        private void SetEditingMode(EditMode mode)
        {
            this.editMode = mode;
            this.OnPropertyChanged("IsInRippleMode");
        }

        /// <summary>
        /// Adds the asset at current position.
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/>.</param>
        private void AddAssetAtCurrentPosition(Asset asset)
        {
            if (asset != null)
            {
                Track layer = this.GetAssetTrack(asset);
                this.AddAssetToLayer(this.sequencyRegistry.CurrentSequenceModel.CurrentPosition, layer, asset, true);
            }
        }

        /// <summary>
        /// Updates the play head position to the given position.
        /// </summary>
        /// <param name="payload">The <see cref="RCE.Infrastructure.Events.PositionPayloadEventArgs"/> instance containing the event data.</param>
        private void UpdatePlayHead(PositionPayloadEventArgs payload)
        {
            TimeCode timeCode = TimeCode.FromSeconds(payload.Position.TotalSeconds, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
            this.sequencyRegistry.CurrentSequenceModel.CurrentPosition = timeCode;
            this.GetElementsVolumeAtCurrentPosition();
            this.View.SetPlayHeadPosition(timeCode);
        }

        private void GetElementsVolumeAtCurrentPosition()
        {
            double currentFrame = this.sequencyRegistry.CurrentSequenceModel.CurrentPosition.TotalFrames;

            foreach (Track track in this.sequencyRegistry.CurrentSequenceModel.Tracks)
            {
                TimelineElement element = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(this.sequencyRegistry.CurrentSequenceModel.CurrentPosition, track, null);
                if (element != null && element.IsRubberbandingEnabled)
                {
                    if (currentFrame >= element.Position.TotalFrames)
                    {
                        currentFrame = currentFrame - element.Position.TotalFrames;
                    }

                    List<Point> elementVolumeCollection = element.VolumeNodeCollection;
                    element.Volume = this.GetCurrentElementVolume(elementVolumeCollection, currentFrame);
                }
            }
        }

        private double GetCurrentElementVolume(List<Point> elementVolumeCollection, double currentFrame)
        {
            for (int i = 0; i < elementVolumeCollection.Count; i++)
            {
                if (elementVolumeCollection[i].X < currentFrame)
                {
                    this.prePoint = elementVolumeCollection[i];
                }

                if (elementVolumeCollection[i].X > currentFrame)
                {
                    this.nextPoint = elementVolumeCollection[i];
                    break;
                }
            }

            double volumeLevel = this.nextPoint.Y - (((this.nextPoint.X - currentFrame) * (this.nextPoint.Y - this.prePoint.Y)) / (this.nextPoint.X - this.prePoint.X));

            volumeLevel = 1 - volumeLevel;

            if (volumeLevel > 0 & volumeLevel <= 1)
            {
                // volumelevel = 1 + Math.Log(volumelevel);*
            }

            return volumeLevel;
        }

        /// <summary>
        /// Adds the dropped item in the timeline to the appropiate layer(Visual/Audio).
        /// </summary>
        /// <param name="payload">The <see cref="DropPayload"/>.</param>
        private void DropItem(DropPayload payload)
        {
            TitleTemplate titleTemplate = payload.DraggedItem as TitleTemplate;
            Asset asset;

            if (titleTemplate != null)
            {
                asset = new TitleAsset
                {
                    Title = titleTemplate.Title,
                    MainText = titleTemplate.MainText,
                    SubText = titleTemplate.SubText,
                    TitleTemplate = titleTemplate
                };
            }
            else
            {
                asset = payload.DraggedItem as Asset;
            }

            if (asset != null)
            {
                LayerPosition layerPosition = this.View.ResolveLayerPositionFromRelativePosition(payload.MouseEventArgs);

                if (layerPosition == null)
                {
                    return;
                }

                Track track = layerPosition.Track ?? this.GetAssetTrack(asset);
                bool isAudioTrack = track.TrackType == TrackType.Audio;

                if (track != null)
                {
                    Dictionary<TimelineElement, Track> newElements = new Dictionary<TimelineElement, Track>();
                    VideoAsset videoAsset = asset as VideoAsset;
                    VideoAssetInOut videoAssetInOut = videoAsset as VideoAssetInOut;
                    AudioAssetInOut audioAssetInOut = asset as AudioAssetInOut;
                    OverlayAsset overlayAsset = asset as OverlayAsset;

                    if (overlayAsset != null)
                    {
                        asset = overlayAsset.Clone();
                    }

                    var smoothStreamingVideoAsset = videoAssetInOut != null ?
                        videoAssetInOut.VideoAsset as SmoothStreamingVideoAsset
                        : videoAsset as SmoothStreamingVideoAsset;

                    TimelineElement element;

                    if (videoAssetInOut != null)
                    {
                        videoAssetInOut = videoAssetInOut.Clone();
                        asset = videoAssetInOut;

                        // video might have selected camera or audio stream
                        if (track.TrackType == TrackType.Audio && smoothStreamingVideoAsset != null)
                        {
                            // adding a particular audio stream to an audio track
                            if (videoAssetInOut.SequenceAudioStreams.Count > 0)
                            {
                                asset =
                                    smoothStreamingVideoAsset.GetSmoothStreamingAudioAsset(
                                        videoAssetInOut.SequenceAudioStreams.First().Name);
                            }
                            else
                            {
                                asset = smoothStreamingVideoAsset.GetSmoothStreamingAudioAsset(string.Empty);
                            }
                        }

                        if (track.TrackType == TrackType.Visual && smoothStreamingVideoAsset != null)
                        {
                            // add audio tracks to related to the video asset to the timeline
                            var audioTracks = this.sequencyRegistry.CurrentSequenceModel.Tracks.Where(t => t.TrackType == TrackType.Audio);

                            int audioTracksCount = audioTracks.Count();
                            for (int i = 1; i < Math.Min(videoAssetInOut.SequenceAudioStreams.Count, audioTracksCount + 1); i++)
                            {
                                string streamName = videoAssetInOut.SequenceAudioStreams[i].Name;
                                Asset audioAsset = smoothStreamingVideoAsset.GetSmoothStreamingAudioAsset(streamName);
                                element = this.AddAssetToLayer(
                                        layerPosition.Position,
                                        audioTracks.ElementAt(i - 1),
                                        audioAsset,
                                        videoAssetInOut.InPosition,
                                        videoAssetInOut.OutPosition,
                                        true);
                                element.SelectedAudioStream = streamName;
                                newElements.Add(element, audioTracks.ElementAt(i - 1));
                            }
                        }

                        // add asset to desired track
                        element = this.AddAssetToLayer(layerPosition.Position, track, asset, videoAssetInOut.InPosition, videoAssetInOut.OutPosition, isAudioTrack);

                        if (track.TrackType == TrackType.Visual && !string.IsNullOrEmpty(videoAssetInOut.SequenceVideoCamera))
                        {
                            element.SelectedVideoStream = videoAssetInOut.SequenceVideoCamera;
                        }

                        if (videoAssetInOut.SequenceAudioStreams.Count > 0)
                        {
                            element.SelectedAudioStream = videoAssetInOut.SequenceAudioStreams[0].Name;
                        }
                    }
                    else if (audioAssetInOut != null)
                    {
                        element = this.AddAssetToLayer(layerPosition.Position, track, asset, audioAssetInOut.InPosition, audioAssetInOut.OutPosition, isAudioTrack);
                    }
                    else
                    {
                        if (track.TrackType == TrackType.Audio && videoAsset != null)
                        {
                            asset = smoothStreamingVideoAsset != null
                                ? smoothStreamingVideoAsset.GetSmoothStreamingAudioAsset(string.Empty)
                                : videoAsset.ConvertToAudioAsset();
                        }

                        if (track.TrackType == TrackType.Visual && smoothStreamingVideoAsset != null)
                        {
                            // add audio tracks to related to the video asset to the timeline
                            var audioTracks = this.sequencyRegistry.CurrentSequenceModel.Tracks.Where(t => t.TrackType == TrackType.Audio);

                            int audioTracksCount = audioTracks.Count();
                            for (int i = 1; i < Math.Min(smoothStreamingVideoAsset.AudioStreams.Count, audioTracksCount + 1); i++)
                            {
                                string streamName = smoothStreamingVideoAsset.AudioStreams[i].Name;
                                Asset audioAsset = smoothStreamingVideoAsset.GetSmoothStreamingAudioAsset(streamName);
                                element = this.AddAssetToLayer(layerPosition.Position, audioTracks.ElementAt(i - 1), audioAsset, true);
                                element.SelectedAudioStream = streamName;
                                newElements.Add(element, audioTracks.ElementAt(i - 1));
                            }
                        }

                        // add asset to desired track
                        element = this.AddAssetToLayer(layerPosition.Position, track, asset, isAudioTrack);

                        if (track.TrackType == TrackType.Visual && smoothStreamingVideoAsset != null && smoothStreamingVideoAsset.AudioStreams.Count > 0)
                        {
                            element.SelectedAudioStream = smoothStreamingVideoAsset.AudioStreams[0].Name;
                        }
                    }

                    newElements.Add(element, track);

                    if (this.snapToFragmentBoundaries)
                    {
                        // media plugin for chunk snapping
                        var mediaPlugin = payload.CustomData as IRCESmoothStreamingMediaPlugin;

                        if (asset.IsAdaptiveAsset && mediaPlugin == null)
                        {
                            // create media plugin
                            mediaPlugin = new RCESmoothStreamingMediaPlugin();
                            mediaPlugin.AutoPlay = false;
                            mediaPlugin.Volume = 0;
                            mediaPlugin.IsMuted = true;
                            mediaPlugin.VisualElement.Width = 0;
                            mediaPlugin.VisualElement.Height = 0;
                            mediaPlugin.AdaptiveSource = asset.Source;
                            mediaPlugin.VisualElement.Visibility = Visibility.Visible;

                            ((TimelineView)this.View).LayoutRoot.Children.Add(mediaPlugin.VisualElement);
                        }

                        foreach (var el in newElements.Keys)
                        {
                            el.MediaPlugin = mediaPlugin;

                            this.AdjustElementToChunkIfAdjacent(el, track);
                        }
                    }

                    // add audio tracks to related to the video asset to the timeline
                    foreach (var el in newElements)
                    {
                        if (el.Key.Duration.TotalSeconds > 0.0 && el.Value.Shots.SingleOrDefault(e => e.Id == el.Key.Id) == null)
                        {
                            AddElementCommand command = new AddElementCommand(this.sequencyRegistry.CurrentSequenceModel, el.Value, el.Key);

                            this.caretaker.ExecuteCommand(command);

                            this.OnPropertyChanged("TimelineDuration");
                        }
                    }

                    if (newElements.Keys.Count > 0 && newElements.Keys.Any(e => e.Duration.TotalSeconds > 0.0))
                    {
                        // new elements that belong to the same video should be locked together
                        this.LockElements(newElements.Keys);

                        // Don't publish the AddAssetEvent if the asset is Title asset
                        if (!(asset is TitleAsset || asset is OverlayAsset))
                        {
                            this.eventAggregator.GetEvent<AddAssetEvent>().Publish(asset);
                        }

                        this.InvokeTitleUpdated();
                    }
                }
            }
        }

        /// <summary>
        /// Updates the start time code from where the timeline starts.
        /// </summary>
        /// <param name="timeCode">The <see cref="TimeCode"/>.</param>
        private void UpdateStartTimeCode(TimeCode timeCode)
        {
            this.View.SetStartTimeCode(timeCode);
        }

        /// <summary>
        /// Deletes the given asset.
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/>.</param>
        private void DeleteAsset(Asset asset)
        {
            IEnumerable<Track> tracks = this.GetAssetTracks(asset);

            if (tracks != null)
            {
                foreach (Track track in tracks)
                {
                    track.Shots.Where(x => x.Asset.Id == asset.Id)
                        .ToList()
                        .ForEach(this.DeleteElement);
                }
            }

            this.InvokeTitleUpdated();
        }

        /// <summary>
        /// Updates the smpte frame rate to the given framerate.
        /// </summary>
        /// <param name="frameRate">The <see cref="SmpteFrameRate"/>.</param>
        private void UpdateSmpteFrameRate(SmpteFrameRate frameRate)
        {
            this.SetDuration(TimeCode.FromAbsoluteTime(this.sequencyRegistry.CurrentSequenceModel.Duration.TotalSeconds, frameRate));
        }

        /// <summary>
        /// Returns the track for the specified Asset.
        /// </summary>
        /// <param name="asset">Asset to validate.</param>
        /// <returns>Timeline's Model Track.</returns>
        private Track GetAssetTrack(Asset asset)
        {
            var videoAsset = asset as VideoAsset;
            var audioAsset = asset as AudioAsset;
            var imageAsset = asset as ImageAsset;
            var overlayAsset = asset as OverlayAsset;

            if (videoAsset != null || imageAsset != null)
            {
                Track track = this.sequencyRegistry.CurrentSequenceModel.Tracks.FirstOrDefault(x => x.TrackType == TrackType.Visual);

                if (track == null)
                {
                    track = new Track { TrackType = TrackType.Visual, Volume = 1 };
                    this.sequencyRegistry.CurrentSequenceModel.Tracks.Add(track);
                    this.sequencyRegistry.CurrentSequence.Tracks.Add(track);
                }

                return track;
            }

            if (audioAsset != null)
            {
                Track track = this.sequencyRegistry.CurrentSequenceModel.Tracks.FirstOrDefault(x => x.TrackType == TrackType.Audio);

                if (track == null)
                {
                    track = new Track { Number = 1, TrackType = TrackType.Audio, IsMuted = true, Volume = 1 };
                    this.sequencyRegistry.CurrentSequenceModel.Tracks.Add(track);
                    this.sequencyRegistry.CurrentSequence.Tracks.Add(track);
                }

                return track;
            }

            if (overlayAsset != null)
            {
                Track track = this.sequencyRegistry.CurrentSequenceModel.Tracks.FirstOrDefault(x => x.TrackType == TrackType.Overlay);

                if (track == null)
                {
                    track = new Track { TrackType = TrackType.Overlay };
                    this.sequencyRegistry.CurrentSequenceModel.Tracks.Add(track);
                    this.sequencyRegistry.CurrentSequence.Tracks.Add(track);
                }

                return track;
            }

            return null;
        }

        /// <summary>
        /// Returns the track for the specified Asset.
        /// </summary>
        /// <param name="asset">Asset to validate.</param>
        /// <returns>Timeline's Model Track.</returns>
        private IEnumerable<Track> GetAssetTracks(Asset asset)
        {
            var videoAsset = asset as VideoAsset;
            var audioAsset = asset as AudioAsset;
            var imageAsset = asset as ImageAsset;
            var overlayAsset = asset as OverlayAsset;

            if (videoAsset != null || imageAsset != null || overlayAsset != null)
            {
                Track track = this.GetAssetTrack(asset);
                return new List<Track> { track };
            }

            if (audioAsset != null)
            {
                return this.sequencyRegistry.CurrentSequenceModel.Tracks.Where(x => x.TrackType == TrackType.Audio).ToList();
            }

            return null;
        }

        /// <summary>
        /// Gets the track associated with the timeline element.
        /// </summary>
        /// <param name="element">The timeline element used to look for a track.</param>
        /// <returns>The track associated with the timeline element.</returns>
        private Track GetElementTrack(TimelineElement element)
        {
            if (element.Asset is AudioAsset)
            {
                Track track = this.sequencyRegistry.CurrentSequenceModel.Tracks.SingleOrDefault(x => x.TrackType == TrackType.Audio && x.Shots.Contains(element));

                return track;
            }
            else
            {
                return this.GetAssetTrack(element.Asset);
            }
        }

        /// <summary>
        /// Sets the duration of the timeline.
        /// </summary>
        /// <param name="timeCode">The duration.</param>
        private void SetDuration(TimeCode timeCode)
        {
            this.sequencyRegistry.CurrentSequenceModel.Duration = timeCode;
            this.View.SetDuration(timeCode);
            this.SetCurrentPosition(TimeCode.FromAbsoluteTime(0, timeCode.FrameRate));
        }

        /// <summary>
        /// Get all the elements at the current playhead position.
        /// </summary>
        /// <returns>A <see cref="IList{T}"/> of <seealso cref="TimelineElement"/>.</returns>
        private IList<TimelineElement> GetElementsAtCurrentPosition()
        {
            IList<TimelineElement> elements = new List<TimelineElement>();

            foreach (Track track in this.sequencyRegistry.CurrentSequenceModel.Tracks)
            {
                TimelineElement element = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(this.sequencyRegistry.CurrentSequenceModel.CurrentPosition, track, null);
                if (element != null)
                {
                    elements.Add(element);
                }
            }

            return elements;
        }

        /// <summary>
        /// Adds the asset to the timeline at the given position.
        /// </summary>
        /// <param name="position">Position where element is going to be added.</param>
        /// <param name="layer">Layer where the element is going to be added.</param>
        /// <param name="asset">Asset to be added in the timeline layer.</param>
        private TimelineElement AddAssetToLayer(TimeCode position, Track layer, Asset asset, bool addToSequence)
        {
            if (layer != null)
            {
                VideoAssetInOut videoAssetInOut = asset as VideoAssetInOut;

                if (videoAssetInOut != null)
                {
                    return this.AddAssetToLayer(position, layer, videoAssetInOut.VideoAsset, videoAssetInOut.InPosition, videoAssetInOut.OutPosition, addToSequence);
                }
                else
                {
                    TimeCode duration = this.GetAssetDuration(asset);

                    return this.AddElement(asset, layer, duration, position, addToSequence);
                }
            }

            return null;
        }

        /// <summary>
        /// Adds the asset to the timeline at the given position.
        /// </summary>
        /// <param name="position">Position where element is going to be added.</param>
        /// <param name="layer">Layer where the element is going to be added.</param>
        /// <param name="asset">Asset to be added in the timeline layer.</param>
        private TimelineElement AddAssetToLayer(TimeCode position, Track layer, Asset asset, double inPosition, double outPosition, bool addToSequence)
        {
            if (layer != null)
            {
                if (asset != null && inPosition != -1 && outPosition != -1)
                {
                    return this.AddElement(asset, layer, position, inPosition, outPosition, addToSequence);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the Asset's default duration.
        /// If the Asset does not contain a duration, the DefaultAssetDuration is returned.
        /// </summary>
        /// <param name="asset">Asset to validate.</param>
        /// <returns>TimeCode with default duration.</returns>
        private TimeCode GetAssetDuration(Asset asset)
        {
            var videoAsset = asset as VideoAsset;
            var audioAsset = asset as AudioAsset;
            var overlayAsset = asset as OverlayAsset;

            if (videoAsset != null)
            {
                return videoAsset.Duration;
            }

            if (audioAsset != null)
            {
                return TimeCode.FromAbsoluteTime(audioAsset.DurationInSeconds, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
            }

            if (overlayAsset != null)
            {
                return TimeCode.FromAbsoluteTime(overlayAsset.DurationInSeconds, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
            }

            return TimeCode.FromAbsoluteTime(DefaultAssetDuration, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
        }

        /// <summary>
        /// Returns the Asset maximun duration according to its type.
        /// </summary>
        /// <param name="asset">Asset to validate.</param>
        /// <returns>TimeCode with max duration.</returns>
        private TimeCode GetAssetMaxDuration(Asset asset)
        {
            var videoAsset = asset as VideoAsset;
            var audioAsset = asset as AudioAsset;

            if (videoAsset != null)
            {
                return videoAsset.Duration;
            }

            if (audioAsset != null)
            {
                return TimeCode.FromAbsoluteTime(audioAsset.DurationInSeconds, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
            }

            return TimeCode.FromAbsoluteTime(0, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
        }

        /// <summary>
        /// Delete the provided elements.
        /// </summary>
        /// <param name="elements">The elements to delete.</param>
        private void DeleteElements(IEnumerable<TimelineElement> elements)
        {
            if (elements.Count() == 0)
            {
                return;
            }

            List<TimelineElement> elementsToRemove = new List<TimelineElement>(elements);

            while (elementsToRemove.Count() > 0)
            {
                var timelineElement = elementsToRemove[0];

                if (timelineElement.MediaPlugin != null)
                {
                    ((TimelineView)this.View).LayoutRoot.Children.Remove(timelineElement.MediaPlugin.VisualElement);
                }

                Track layer = this.GetElementTrack(timelineElement);

                RemoveElementCommand command = new RemoveElementCommand(this.sequencyRegistry.CurrentSequenceModel, layer, this.editMode, timelineElement);

                this.caretaker.ExecuteCommand(command);
                elementsToRemove.Remove(timelineElement);
                this.lockGroupManager.UnlockElementLockGroup(timelineElement);
                this.OnPropertyChanged("TimelineDuration");
            }
        }

        /// <summary>
        /// Delete the element.
        /// </summary>
        /// <param name="element">The element to delete.</param>
        private void DeleteElement(TimelineElement element)
        {
            if (element == null)
            {
                return;
            }

            var elements = this.lockGroupManager.GetGroupedElements(element);
            this.DeleteElements(elements);

            this.InvokeTitleUpdated();
            this.UpdateTimelineDuration();
        }

        /// <summary>
        /// Add asset to timeline with In/Out Position.
        /// </summary>
        /// <param name="asset">Asset instance.</param>
        /// <param name="layer">Timeline layer.</param>
        /// <param name="duration">Duration of the element.</param>
        /// <param name="position">Position of drop.</param>
        /// <returns>The element added.</returns>
        private TimelineElement AddElement(Asset asset, Track layer, TimeCode duration, TimeCode position, bool addToSequence)
        {
            return this.AddElement(asset, layer, position, 0, duration.TotalSeconds, addToSequence);
        }

        /// <summary>
        /// Add asset to timeline with In/Out Position.
        /// </summary>
        /// <param name="asset">Asset instance.</param>
        /// <param name="layer">Timeline layer.</param>
        /// <param name="position">Position of drop.</param>
        /// <param name="inPosition">InPosition in second from the begining.</param>
        /// <param name="outPosition">OutPosition in second from the begining.</param>
        /// <returns>The element added.</returns>
        private TimelineElement AddElement(Asset asset, Track layer, TimeCode position, double inPosition, double outPosition, bool addToSequence)
        {
            TimeCode duration = TimeCode.FromAbsoluteTime(outPosition - inPosition, position.FrameRate);
            TimeCode outOffset = duration;
            bool offsetFix = false;

            if (this.editMode == EditMode.Ripple)
            {
                TimelineElement overlapElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(position, layer, null);

                if (overlapElement != null)
                {
                    TimelineElement nextOverlapElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(overlapElement.Position + overlapElement.Duration, layer, overlapElement);

                    if (nextOverlapElement != null)
                    {
                        TimeCode newEndPosition = nextOverlapElement.Position + duration;

                        while (nextOverlapElement != null)
                        {
                            TimeCode nextElementOldPosition = nextOverlapElement.Position;
                            TimeCode nextElementNewPosition = TimeCode.FromAbsoluteTime(newEndPosition.TotalSeconds, overlapElement.Position.FrameRate);

                            this.sequencyRegistry.CurrentSequenceModel.MoveElement(nextOverlapElement, layer, nextElementNewPosition);
                            newEndPosition = newEndPosition + nextOverlapElement.Duration;
                            this.View.RefreshElement(nextOverlapElement);
                            this.PublishElementMovedEvent(nextOverlapElement, ElementPositionType.Position, nextElementOldPosition, nextElementNewPosition);

                            nextOverlapElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(newEndPosition, layer, nextOverlapElement);
                        }

                        position = overlapElement.OutPosition - overlapElement.InPosition;
                    }
                }

                if (this.sequencyRegistry.CurrentSequenceModel.Duration.TotalSeconds <= this.TimelineDuration.TotalSeconds)
                {
                    double exceededDuration = this.TimelineDuration.TotalSeconds - this.sequencyRegistry.CurrentSequenceModel.Duration.TotalSeconds;
                    this.IncreaseTimelineDuration(exceededDuration);
                }
            }

            if (this.IsInSnapMode)
            {
                TimelineElement previousElement = this.sequencyRegistry.CurrentSequenceModel.GetPreviousElement(position, layer);

                if (previousElement == null)
                {
                    position = TimeCode.FromSeconds(0d, position.FrameRate);
                }
                else
                {
                    position = TimeCode.FromAbsoluteTime(previousElement.Position.TotalSeconds + previousElement.Duration.TotalSeconds, position.FrameRate);
                }
            }

            // fix start position (do not overlay with other assets, move to next available position)
            TimelineElement nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(position, layer, null);
            while (nextElement != null)
            {
                offsetFix = true;
                position = TimeCode.FromAbsoluteTime(nextElement.Position.TotalSeconds + nextElement.Duration.TotalSeconds, position.FrameRate);
                nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(position, layer, nextElement);
            }

            // fix end position (do not overlay with other assets, trim to fit)
            nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(position, position + duration, layer, null);
            if (nextElement != null)
            {
                outOffset = TimeCode.FromAbsoluteTime(nextElement.Position.TotalSeconds - position.TotalSeconds, position.FrameRate);
                offsetFix = true;
            }

            // do not exceed timeline duration (trim to fit)
            if (position + duration >= this.sequencyRegistry.CurrentSequenceModel.Duration)
            {
                if (!offsetFix)
                {
                    // move to fit
                    // TimeCode newPosition = TimeCode.FromAbsoluteTime(this.timelineModel.Duration.TotalSeconds - duration.TotalSeconds, this.timelineModel.Duration.FrameRate);
                    if (this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(position, this.sequencyRegistry.CurrentSequenceModel.Duration, layer, null) != null)
                    {
                        TimelineElement lastElement = layer.Shots[layer.Shots.Count - 1];

                        position = TimeCode.FromAbsoluteTime(lastElement.Position.TotalSeconds + lastElement.Duration.TotalSeconds, position.FrameRate);
                    }
                }

                if (outOffset == duration)
                {
                    // trim to fit
                    // outOffset = TimeCode.FromAbsoluteTime(duration.TotalSeconds - ((position.TotalSeconds + duration.TotalSeconds) - this.timelineModel.Duration.TotalSeconds), position.FrameRate);

                    // Increase Timeline Duration
                    double exceededDuration = (position.TotalSeconds + duration.TotalSeconds) - this.sequencyRegistry.CurrentSequenceModel.Duration.TotalSeconds;

                    this.IncreaseTimelineDuration(exceededDuration);
                }
            }

            TimelineElement element = new TimelineElement
            {
                Asset = asset,
                InPosition = TimeCode.FromAbsoluteTime(inPosition, position.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(outOffset.TotalSeconds + inPosition, position.FrameRate),
                Position = position,
                Balance = layer.Balance
            };

            if (addToSequence && element.Duration.TotalSeconds > 0.0)
            {
                AddElementCommand command = new AddElementCommand(this.sequencyRegistry.CurrentSequenceModel, layer, element);

                this.caretaker.ExecuteCommand(command);

                this.OnPropertyChanged("TimelineDuration");
            }

            return element;
        }

        private void AdjustElementToChunkIfAdjacent(TimelineElement element, Track layer)
        {
            var nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(element.Position + element.Duration, layer, element);
            if (nextElement != null)
            {
                if (element.Position + element.Duration == nextElement.Position)
                {
                    var chunkPos = this.SnapToChunk(element.OutPosition, element, true);
                    if (chunkPos < element.OutPosition && chunkPos > element.InPosition)
                    {
                        element.OutPosition = chunkPos;
                    }
                    else if (chunkPos == element.InPosition)
                    {
                        element.OutPosition = chunkPos;
                        var tooltipPosition = TimeCode.FromAbsoluteTime(element.Duration.TotalSeconds + element.Position.TotalSeconds, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
                        var layerPosition = new LayerPosition
                        {
                            Track = layer,
                            Position = tooltipPosition,
                            LayerType = layer.TrackType
                        };

                        this.View.ShowWarningTooltip("Not enough space to drop clip here!!", layerPosition);
                    }
                }
            }
        }

        private void IncreaseTimelineDuration(double exceededDuration)
        {
            // TimeCode newDuration = this.sequencyRegistry.CurrentSequenceModel.Duration + TimeCode.FromSeconds(exceededDuration, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate) + TimeCode.FromMinutes(15, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
            TimeCode newDuration = TimeCode.FromSeconds(
                this.sequencyRegistry.CurrentSequenceModel.Duration.TotalSeconds + exceededDuration + (15 * 60),
                this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);

            this.SetDuration(newDuration);
        }

        /// <summary>
        /// Move the selected element to a <paramref name="newPosition">new position.</paramref>
        /// </summary>
        /// <param name="newPosition">The new position where the selected element is being positioned.</param>
        private void MoveSelectedElement(TimeCode newPosition)
        {
            if (this.selectedElements.Count == 0)
            {
                throw new InvalidOperationException("No elements are selected");
            }

            // we get the right most and left most element to be moved. 
            // this is done to stop moving in group when another asset or timeline begin/end is reached. 
            TimelineElement rightMostElement = this.selectedElements.ElementAt(0);
            TimelineElement leftMostElement = this.selectedElements.ElementAt(0);

            TimeCode selectedLeftMostTime = this.sequencyRegistry.CurrentSequenceModel.Duration;
            TimeCode selectedRightMostTime = TimeCode.FromSeconds(0.0, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);

            var elementToMove = this.selectedElements.ElementAt(0);
            IEnumerable<TimelineElement> lockedElements = this.lockGroupManager.GetGroupedElements(elementToMove);

            TimeCode groupRightMost = lockedElements.Max(e => e.Position + e.Duration);
            TimeCode groupleftMost = lockedElements.Min(e => e.Position);

            if (groupRightMost > selectedRightMostTime)
            {
                selectedRightMostTime = groupRightMost;
                rightMostElement = lockedElements.First(e => e.Position + e.Duration == selectedRightMostTime);
            }

            if (groupleftMost < selectedLeftMostTime)
            {
                selectedLeftMostTime = groupleftMost;
                leftMostElement = lockedElements.First(e => e.Position == selectedLeftMostTime);
            }

            TimeCode oldPosition = elementToMove.Position;
            TimeCode delta = newPosition > oldPosition ? newPosition - oldPosition : oldPosition - newPosition;
            bool movingForward = newPosition > oldPosition;

            bool reachedEnd = movingForward && rightMostElement.Position + rightMostElement.Duration + delta > this.sequencyRegistry.CurrentSequenceModel.Duration;
            bool reachedBegin = !movingForward && delta > leftMostElement.Position;
            TimeCode delta1 = delta;
            IEnumerable<TimelineElement> reachedElements =
                lockedElements.Select(e => this.GetReachedElement(movingForward, e, delta1));

            bool elementsReachedAnotherone = reachedElements.Any(e => e != null);

            if (lockedElements.Count() > 1)
            {
                if (!elementsReachedAnotherone)
                {
                    if (reachedBegin)
                    {
                        // hit begin of timeline => the delta is the initial position of the leftmost element
                        delta = leftMostElement.Position;

                        newPosition = oldPosition - delta;
                    }
                    else if (reachedEnd)
                    {
                        delta = this.sequencyRegistry.CurrentSequenceModel.Duration
                                - (rightMostElement.Position + rightMostElement.Duration);

                        newPosition = oldPosition + delta;
                    }
                }
                else
                {
                    // the element that was reached
                    var reachedElement = reachedElements.First(e => e != null);
                    int reachedElementIndex = reachedElements.TakeWhile(element => element != reachedElement).Count();

                    // the element that was moving and reached another one
                    var reachingElement = lockedElements.ElementAt(reachedElementIndex);

                    if (!(reachedBegin || reachedEnd))
                    {
                        if (movingForward)
                        {
                            delta = reachedElement.Position - (reachingElement.Position + reachingElement.Duration);
                            newPosition = oldPosition + delta;
                        }
                        else
                        {
                            delta = reachingElement.Position - (reachedElement.Position + reachedElement.Duration);
                            newPosition = oldPosition - delta;
                        }
                    }
                    else if (reachedBegin)
                    {
                        // reached another element and the beginning, need to know which one has a lower delta.
                        TimeCode beginDelta = leftMostElement.Position;
                        TimeCode reachDelta = reachingElement.Position - (reachedElement.Position + reachingElement.Duration);

                        delta = beginDelta < reachDelta ? beginDelta : reachDelta;
                        newPosition = oldPosition - delta;
                    }
                    else
                    {
                        // if (reachedEnd)
                        // reached another element and the end, need to know which one has a lower delta.
                        TimeCode endDelta = this.sequencyRegistry.CurrentSequenceModel.Duration - (rightMostElement.Position + rightMostElement.Duration);
                        TimeCode reachDelta = reachedElement.Position - (reachingElement.Position + reachingElement.Duration);

                        delta = endDelta < reachDelta ? endDelta : reachDelta;
                        newPosition = oldPosition + delta;
                    }
                }
            }

            // store all initial positions to make sure the difference is constant
            IDictionary<TimelineElement, TimeCode> positionByElement = new Dictionary<TimelineElement, TimeCode>();

            foreach (var lockedElement in lockedElements)
            {
                positionByElement[lockedElement] = lockedElement.Position;
            }

            TimeCode referenceDiff = TimeCode.FromSeconds(0.0, SmpteFrameRate.Unknown);

            // start moving the elements
            Track track = this.GetElementTrack(elementToMove);

            if (track != null)
            {
                var startPosition = elementToMove.Position;
                this.MoveElement(elementToMove, track, newPosition);
                var endPosition = elementToMove.Position;
                double offset = endPosition.TotalSeconds - startPosition.TotalSeconds;
                this.moveMarkerscommand.UpdateOffset(elementToMove, offset);

                if (movingForward)
                {
                    referenceDiff = elementToMove.Position - positionByElement[elementToMove];
                }
                else
                {
                    referenceDiff = positionByElement[elementToMove] - elementToMove.Position;
                }
            }

            bool revertMovement = false;

            foreach (var lockedElement in lockedElements)
            {
                if (lockedElement == elementToMove)
                {
                    continue;
                }

                Track lockedTrack = this.GetElementTrack(lockedElement);
                TimeCode lockedNewPosition;

                if (movingForward)
                {
                    lockedNewPosition = lockedElement.Position + delta;
                }
                else
                {
                    lockedNewPosition = lockedElement.Position > delta ? lockedElement.Position - delta : TimeCode.FromSeconds(0.0, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
                }

                var startPosition = lockedElement.Position;
                this.MoveElement(lockedElement, lockedTrack, lockedNewPosition);
                var endPosition = lockedElement.Position;
                double offset = endPosition.TotalSeconds - startPosition.TotalSeconds;
                this.moveMarkerscommand.UpdateOffset(lockedElement, offset);

                if (movingForward)
                {
                    TimeCode difference = lockedElement.Position - positionByElement[lockedElement];
                    revertMovement = difference != referenceDiff;
                    if (revertMovement)
                    {
                        break;
                    }
                }
                else
                {
                    TimeCode difference = positionByElement[lockedElement] - lockedElement.Position;
                    revertMovement = difference != referenceDiff;
                    if (revertMovement)
                    {
                        break;
                    }
                }
            }

            if (revertMovement)
            {
                foreach (var element in lockedElements)
                {
                    Track elementTrack = this.GetElementTrack(element);
                    var startPosition = element.Position;
                    this.MoveElement(element, elementTrack, positionByElement[element]);
                    var endPosition = element.Position;
                    double offset = endPosition.TotalSeconds - startPosition.TotalSeconds;
                    this.moveMarkerscommand.UpdateOffset(element, offset);
                }
            }
        }

        private TimelineElement GetReachedElement(bool movingForward, TimelineElement element, TimeCode delta)
        {
            TimeCode offset = TimeCode.FromSeconds((element.Duration.TotalSeconds * 15) / 100, element.Duration.FrameRate);

            TimeCode newPosition;

            if (movingForward)
            {
                newPosition = element.Position + delta;
            }
            else
            {
                newPosition = element.Position > delta ? element.Position - delta : TimeCode.FromSeconds(0.0, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
            }

            var layer = this.GetElementTrack(element);

            var startNextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(newPosition, newPosition + element.Duration - offset, layer, element);

            var endNextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(newPosition, newPosition + element.Duration, layer, element);

            bool ranIntoOtherElement = (startNextElement != null && startNextElement != element)
                                       || (endNextElement != null && endNextElement != element);

            if (ranIntoOtherElement)
            {
                return endNextElement ?? startNextElement;
            }

            return null;
        }

        /// <summary>
        /// Moves an element of a specific layer to a new position.
        /// </summary>
        /// <param name="element">The element being moved|.</param>
        /// <param name="layer">The track where the element belongs to.</param>
        /// <param name="newPosition">The new position where the selected element ins being positioned.</param>
        private void MoveElement(TimelineElement element, Track layer, TimeCode newPosition)
        {
            var oldPosition = element.Position;
            var offsetFix = false;

            if (newPosition.TotalSeconds < 0)
            {
                newPosition = TimeCode.FromAbsoluteTime(0, newPosition.FrameRate);
            }

            if (newPosition > oldPosition)
            {
                element = this.sequencyRegistry.CurrentSequenceModel.FindLastElementLinking(element, layer);
                newPosition = TimeCode.FromSeconds(element.Position.TotalSeconds + (newPosition.TotalSeconds - oldPosition.TotalSeconds), newPosition.FrameRate);
                oldPosition = element.Position;
            }
            else if (newPosition < oldPosition)
            {
                element = this.sequencyRegistry.CurrentSequenceModel.FindFirstElementLinking(element, layer);
                newPosition = TimeCode.FromSeconds(element.Position.TotalSeconds - (oldPosition.TotalSeconds - newPosition.TotalSeconds), newPosition.FrameRate);
                oldPosition = element.Position;
            }

            // fix start position (do not overlay with other assets)
            // var nextElement = this.timelineModel.GetElementAtPosition(newPosition, layer, element);
            TimeCode offset = TimeCode.FromSeconds((element.Duration.TotalSeconds * 15) / 100, element.Duration.FrameRate);

            var nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(newPosition, newPosition + element.Duration - offset, layer, element);
            while (nextElement != null && nextElement != element)
            {
                offsetFix = true;
                newPosition = nextElement.Position + nextElement.Duration;

                // nextElement = this.timelineModel.GetElementAtPosition(newPosition, layer, nextElement);
                nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(newPosition, newPosition + element.Duration, layer, nextElement);
            }

            // fix end position (do not overlay with other assets)
            nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(newPosition, newPosition + element.Duration, layer, element);
            while (nextElement != null && nextElement != element)
            {
                offsetFix = true;
                newPosition = TimeCode.FromSeconds(nextElement.Position.TotalSeconds - element.Duration.TotalSeconds, newPosition.FrameRate);

                // nextElement = this.timelineModel.GetElementAtPosition(newPosition, layer, nextElement);
                nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(newPosition, newPosition + element.Duration, layer, nextElement);

                if (nextElement != null && nextElement.Position + nextElement.Duration == newPosition)
                {
                    nextElement = null;
                }
            }

            if (newPosition < TimeCode.FromAbsoluteTime(0, newPosition.FrameRate))
            {
                if (!offsetFix && this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(TimeCode.FromAbsoluteTime(0, newPosition.FrameRate), element.Duration, layer, element) == null)
                {
                    newPosition = TimeCode.FromAbsoluteTime(0, newPosition.FrameRate);
                }
                else
                {
                    // already fixed, invalidate new position
                    newPosition = oldPosition;
                }
            }
            else if (newPosition + element.Duration > this.sequencyRegistry.CurrentSequenceModel.Duration)
            {
                if (!offsetFix && this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(this.sequencyRegistry.CurrentSequenceModel.Duration - element.Duration, this.sequencyRegistry.CurrentSequenceModel.Duration, layer, element) == null)
                {
                    newPosition = TimeCode.FromAbsoluteTime(this.sequencyRegistry.CurrentSequenceModel.Duration.TotalSeconds - element.Duration.TotalSeconds, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
                }
                else
                {
                    // already fixed, invalidate new position
                    newPosition = oldPosition;
                }
            }

            this.sequencyRegistry.CurrentSequenceModel.MoveElement(element, layer, newPosition);
            this.View.RefreshElement(element);
            this.PublishElementMovedEvent(element, ElementPositionType.Position, oldPosition, newPosition);

            // tooltip
            var tooltipPosition = newPosition;
            var layerPosition = new LayerPosition
            {
                Track = layer,
                Position = tooltipPosition,
                LayerType = layer.TrackType
            };

            this.View.ShowTooltip(tooltipPosition.ToString(), layerPosition);

            TimelineElementLink link = this.sequencyRegistry.CurrentSequenceModel.GetElementLink(element);

            if (newPosition > oldPosition)
            {
                TimelineElement previousElement = layer.Shots.Where(e => e.Id == link.PreviousElementId).SingleOrDefault();

                while (previousElement != null)
                {
                    TimeCode previousElementOldPosition = previousElement.Position;
                    TimeCode previousElementNewPosition = previousElement.Position + (newPosition - oldPosition);
                    this.sequencyRegistry.CurrentSequenceModel.MoveElement(previousElement, layer, previousElementNewPosition);
                    this.View.RefreshElement(previousElement);
                    this.PublishElementMovedEvent(previousElement, ElementPositionType.Position, previousElementOldPosition, previousElementNewPosition);
                    link = this.sequencyRegistry.CurrentSequenceModel.GetElementLink(previousElement);
                    previousElement = layer.Shots.Where(e => e.Id == link.PreviousElementId).SingleOrDefault();
                }
            }
            else if (newPosition < oldPosition)
            {
                nextElement = layer.Shots.Where(e => e.Id == link.NextElementId).SingleOrDefault();

                while (nextElement != null)
                {
                    TimeCode nextElementOldPosition = nextElement.Position;
                    TimeCode nextElementNewPosition = nextElement.Position - (oldPosition - newPosition);
                    this.sequencyRegistry.CurrentSequenceModel.MoveElement(nextElement, layer, nextElementNewPosition);
                    this.View.RefreshElement(nextElement);
                    this.PublishElementMovedEvent(nextElement, ElementPositionType.Position, nextElementOldPosition, nextElementNewPosition);
                    link = this.sequencyRegistry.CurrentSequenceModel.GetElementLink(nextElement);
                    nextElement = layer.Shots.Where(e => e.Id == link.NextElementId).SingleOrDefault();
                }
            }
        }

        /// <summary>
        /// Publishes the ElementMovedEvent every time an element is being moved.
        /// </summary>
        /// <param name="element">The element that was moved.</param>
        /// <param name="positionType">The type of position.</param>
        /// <param name="oldPosition">The old position of the element.</param>
        /// <param name="newPosition">The new position of the element.</param>
        private void PublishElementMovedEvent(TimelineElement element, ElementPositionType positionType, TimeCode oldPosition, TimeCode newPosition)
        {
            ElementMovedPayload payload = new ElementMovedPayload(element, positionType, oldPosition, newPosition);
            this.eventAggregator.GetEvent<ElementMovedEvent>().Publish(payload);
            this.OnPropertyChanged("TimelineDuration");
        }

        /// <summary>
        /// Sets the current position of the timeline.
        /// </summary>
        /// <param name="timeCode">The new position.</param>
        private void SetCurrentPosition(TimeCode timeCode)
        {
            this.sequencyRegistry.CurrentSequenceModel.CurrentPosition = timeCode;
            this.eventAggregator.GetEvent<PlayheadMovedEvent>().Publish(new PositionPayloadEventArgs(TimeSpan.FromSeconds(timeCode.TotalSeconds)));
            this.View.SetPlayHeadPosition(timeCode);
        }

        /// <summary>
        /// Trims the left position of the selected element.
        /// </summary>
        /// <param name="absolutePosition">The new position of the element.</param>
        private void TrimLeftElement(TimeCode absolutePosition, TimelineElement elementToTrim)
        {
            if (elementToTrim == null)
            {
                throw new InvalidOperationException("No element is selected");
            }

            Track track = this.GetElementTrack(elementToTrim);
            double maxDuration = this.GetAssetMaxDuration(elementToTrim.Asset).TotalSeconds;

            TimeCode offset = TimeCode.FromAbsoluteTime(absolutePosition.TotalSeconds - elementToTrim.Position.TotalSeconds, absolutePosition.FrameRate);
            TimeCode newInPosition = elementToTrim.InPosition + offset;

            // snap in position to chunks boundaries and correct offset
            var snappedInPosition = this.SnapToChunk(newInPosition, elementToTrim);
            offset = TimeCode.FromAbsoluteTime(offset.TotalSeconds + snappedInPosition.TotalSeconds - newInPosition.TotalSeconds, offset.FrameRate);
            newInPosition = snappedInPosition;

            TimeCode newDuration = TimeCode.FromSeconds(elementToTrim.OutPosition.TotalSeconds - newInPosition.TotalSeconds, elementToTrim.Duration.FrameRate);

            // validate trim (MINIMAL LENGTH)
            TimeCode minimumDuration = TimeCode.FromAbsoluteTime(MinimumElementDuration, newDuration.FrameRate);
            if (newDuration < minimumDuration)
            {
                newInPosition = elementToTrim.OutPosition - minimumDuration;
                offset = TimeCode.FromSeconds(newInPosition.TotalSeconds - elementToTrim.InPosition.TotalSeconds, newInPosition.FrameRate);
                newDuration = minimumDuration;
            }

            // validate trim (VIDEO/AUDIO MAX LENGTH)
            if (maxDuration > 0)
            {
                if (newInPosition.TotalSeconds < 0)
                {
                    newInPosition = TimeCode.FromAbsoluteTime(0, newInPosition.FrameRate);
                    offset = TimeCode.FromSeconds(newInPosition.TotalSeconds - elementToTrim.InPosition.TotalSeconds, newInPosition.FrameRate);
                }
            }

            TimeCode newPosition = elementToTrim.Position + offset;

            if (newPosition.TotalSeconds < 0)
            {
                return;
            }

            EditMode currentEditMode = this.editMode;

            TimelineElementLink link = this.sequencyRegistry.CurrentSequenceModel.GetElementLink(elementToTrim);

            // If element has link, should behavior as in Ripple Mode
            if (link.PreviousElementId != Guid.Empty)
            {
                currentEditMode = EditMode.Ripple;
            }

            switch (currentEditMode)
            {
                case EditMode.Gap:
                    {
                        // GAP MODE, validate OVERLAPPING
                        TimelineElement prevElement = this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(newPosition, newPosition + newDuration, track, elementToTrim);
                        if (prevElement != null)
                        {
                            if (this.snapToFragmentBoundaries && newPosition < prevElement.Position + prevElement.Duration)
                            {
                                return;
                            }
                            else
                            {
                                TimeCode oldPosition = newPosition;
                                newPosition = prevElement.Position + prevElement.Duration;
                                offset = TimeCode.FromAbsoluteTime(oldPosition.TotalSeconds - newPosition.TotalSeconds, newPosition.FrameRate);
                                newInPosition = TimeCode.FromAbsoluteTime(newInPosition.TotalSeconds - offset.TotalSeconds, newInPosition.FrameRate);
                            }
                        }
                    }

                    break;
                case EditMode.Ripple:
                    {
                        TimelineElement currElement = elementToTrim;
                        TimelineElement prevElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(currElement.Position, track, currElement) ??
                                          this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(newPosition, newPosition + currElement.Duration, track, currElement);

                        if (prevElement != null)
                        {
                            // RIPPLE MODE, move any adjacent elements
                            if (newPosition < elementToTrim.Position)
                            {
                                // Move previous elements backward
                                while (prevElement != null)
                                {
                                    TimeCode prevElementOldPosition = prevElement.Position;
                                    TimeCode prevElementNewPosition = TimeCode.FromAbsoluteTime(currElement.Position.TotalSeconds - prevElement.Duration.TotalSeconds + offset.TotalSeconds, prevElement.Position.FrameRate);

                                    this.sequencyRegistry.CurrentSequenceModel.MoveElement(prevElement, track, prevElementNewPosition);
                                    this.View.RefreshElement(prevElement);
                                    this.PublishElementMovedEvent(prevElement, ElementPositionType.Position, prevElementOldPosition, prevElementNewPosition);

                                    currElement = prevElement;
                                    prevElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(currElement.Position, track, currElement);
                                    offset = TimeCode.FromAbsoluteTime(0, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
                                }
                            }
                            else
                            {
                                // Move previous elements forward
                                TimeCode newPrevPosition = newPosition;
                                while (prevElement != null)
                                {
                                    TimeCode prevElementOldPosition = prevElement.Position;
                                    TimeCode prevElementNewPosition = TimeCode.FromAbsoluteTime(newPrevPosition.TotalSeconds - prevElement.Duration.TotalSeconds, prevElement.Position.FrameRate);

                                    this.sequencyRegistry.CurrentSequenceModel.MoveElement(prevElement, track, prevElementNewPosition);
                                    newPrevPosition = prevElement.Position;
                                    this.View.RefreshElement(prevElement);
                                    this.PublishElementMovedEvent(prevElement, ElementPositionType.Position, prevElementOldPosition, prevElementNewPosition);

                                    currElement = prevElement;
                                    prevElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(prevElementOldPosition, track, currElement);
                                }
                            }
                        }
                    }

                    break;
            }

            this.sequencyRegistry.CurrentSequenceModel.MoveElement(elementToTrim, track, newPosition);
            TimeCode oldInPosition = elementToTrim.InPosition;
            elementToTrim.InPosition = newInPosition;
            this.View.RefreshElement(elementToTrim);
            this.trimMarkersCommand.UpdateInformation(elementToTrim, newInPosition.TotalSeconds - oldInPosition.TotalSeconds, elementToTrim.Position.TotalSeconds, ElementPositionType.InPosition);
            this.PublishElementMovedEvent(elementToTrim, ElementPositionType.InPosition, oldInPosition, newInPosition);

            // Tooltip
            LayerPosition layerPosition = new LayerPosition
            {
                Track = track,
                Position = newPosition,
                LayerType = track.TrackType
            };

            this.View.ShowTooltip(newPosition.ToString(), layerPosition);
        }

        private TimeCode SnapToChunk(TimeCode position, TimelineElement elementToTrim)
        {
            return this.SnapToChunk(position, elementToTrim, false);
        }

        private TimeCode SnapToChunk(TimeCode position, TimelineElement elementToTrim, bool useLeftChunk)
        {
            var asset = elementToTrim.Asset;

            if (this.snapToFragmentBoundaries && asset != null && asset.IsAdaptiveAsset)
            {
                if (elementToTrim.MediaPlugin != null)
                {
                    var currentSegment = elementToTrim.MediaPlugin.CurrentSegment;
                    if (currentSegment != null)
                    {
                        var firstVideoStream = currentSegment.SelectedStreams.Where(s => s.Type == StreamType.Video).FirstOrDefault();
                        if (firstVideoStream != null)
                        {
                            var timeScale = firstVideoStream.TimeScale.HasValue ? firstVideoStream.TimeScale.Value.Ticks : 10000000;
                            var decimalsTrim = timeScale.ToString().Length - 1;
                            var relativePosition = Math.Round(Convert.ToDouble(position.TotalSecondsPrecision) + firstVideoStream.GetStartOffset().TotalSeconds, decimalsTrim);
                            var chunks = firstVideoStream.DataChunks.FindChunks((t, d) => relativePosition >= Math.Round(t, decimalsTrim) && relativePosition <= Math.Round(t + d, decimalsTrim));

                            Tuple<double, double> chunk = null;
                            chunk = chunks.FirstOrDefault();

                            if (chunk != null)
                            {
                                var offset = firstVideoStream.GetStartOffset().TotalSeconds * timeScale;
                                var newTime = (Math.Abs(chunk.Item1 - relativePosition) < Math.Abs(chunk.Item1 + chunk.Item2 - relativePosition) || useLeftChunk)
                                            ? Convert.ToDecimal((chunk.Item1 * timeScale) - offset) / timeScale
                                            : Convert.ToDecimal((chunk.Item1 * timeScale) + (chunk.Item2 * timeScale) - offset) / timeScale;

                                return new TimeCode(newTime, position.FrameRate);
                            }
                        }
                    }
                }
            }

            return position;
        }

        /// <summary>
        /// Trims the right position of the element.
        /// </summary>
        /// <param name="absolutePosition">The new position of the element.</param>
        private void TrimRightElement(TimeCode absolutePosition, TimelineElement elementToTrim)
        {
            if (elementToTrim == null)
            {
                throw new InvalidOperationException("No element is selected");
            }

            var layer = this.GetElementTrack(elementToTrim);
            var maxDuration = this.GetAssetMaxDuration(elementToTrim.Asset).TotalSeconds;

            var newOutPosition = TimeCode.FromAbsoluteTime(absolutePosition.TotalSeconds - elementToTrim.Position.TotalSeconds + elementToTrim.InPosition.TotalSeconds, absolutePosition.FrameRate);

            newOutPosition = this.SnapToChunk(newOutPosition, elementToTrim);

            var newDuration = TimeCode.FromAbsoluteTime(newOutPosition.TotalSeconds - elementToTrim.InPosition.TotalSeconds, newOutPosition.FrameRate);

            // validate trim (MINIMAL LENGTH)
            var minimumDuration = TimeCode.FromAbsoluteTime(MinimumElementDuration, newDuration.FrameRate);
            if (newDuration < minimumDuration)
            {
                newOutPosition = TimeCode.FromAbsoluteTime(elementToTrim.InPosition.TotalSeconds + minimumDuration.TotalSeconds, newOutPosition.FrameRate);
            }

            // validate trim (VIDEO/AUDIO MAX LENGTH)
            if (maxDuration > 0)
            {
                if (newOutPosition.TotalSeconds > maxDuration)
                {
                    newOutPosition = TimeCode.FromAbsoluteTime(maxDuration, newOutPosition.FrameRate);
                }
            }

            var newEndPosition = elementToTrim.Position + (newOutPosition - elementToTrim.InPosition);
            var offset = TimeCode.FromSeconds(newOutPosition.TotalSeconds - elementToTrim.OutPosition.TotalSeconds, newOutPosition.FrameRate);

            var currentEditMode = this.editMode;

            TimelineElementLink link = this.sequencyRegistry.CurrentSequenceModel.GetElementLink(elementToTrim);

            // If element has link, shoud behavior as in Ripple Mode
            if (link.NextElementId != Guid.Empty)
            {
                currentEditMode = EditMode.Ripple;
            }

            switch (currentEditMode)
            {
                case EditMode.Gap:
                    {
                        // GAP MODE, validate trim (OVERLAPPING)
                        var nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(elementToTrim.Position, newEndPosition, layer, elementToTrim);
                        if (nextElement != null)
                        {
                            if (this.snapToFragmentBoundaries && newEndPosition > nextElement.Position)
                            {
                                return;
                            }
                            else
                            {
                                newEndPosition = nextElement.Position;

                                newOutPosition = TimeCode.FromAbsoluteTime(newEndPosition.TotalSeconds - elementToTrim.Position.TotalSeconds + elementToTrim.InPosition.TotalSeconds, elementToTrim.Position.FrameRate);
                            }
                        }
                    }

                    break;
                case EditMode.Ripple:
                    {
                        var currElement = elementToTrim;
                        var nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(currElement.Position + currElement.Duration, layer, currElement) ??
                                          this.sequencyRegistry.CurrentSequenceModel.GetElementWithinRange(currElement.Position + currElement.Duration, currElement.Position + newDuration, layer, currElement);

                        // RIPPLE MODE, move any adjacent elements
                        if (newOutPosition < elementToTrim.OutPosition)
                        {
                            // Move next elements backward
                            while (nextElement != null)
                            {
                                TimeCode nextElementOldPosition = nextElement.Position;
                                TimeCode nextElementNewPosition = TimeCode.FromAbsoluteTime(nextElement.Position.TotalSeconds + offset.TotalSeconds, nextElement.Position.FrameRate);

                                this.sequencyRegistry.CurrentSequenceModel.MoveElement(nextElement, layer, nextElementNewPosition);
                                this.View.RefreshElement(nextElement);
                                this.PublishElementMovedEvent(nextElement, ElementPositionType.Position, nextElementOldPosition, nextElementNewPosition);

                                currElement = nextElement;
                                nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(nextElementOldPosition + currElement.Duration, layer, currElement);

                                // offset = TimeCode.FromAbsoluteTime(0, this.Model.Duration.FrameRate);
                            }
                        }
                        else
                        {
                            // Move next elements fordward
                            while (nextElement != null)
                            {
                                TimeCode nextElementOldPosition = nextElement.Position;
                                TimeCode nextElementNewPosition = TimeCode.FromAbsoluteTime(newEndPosition.TotalSeconds, nextElement.Position.FrameRate);

                                this.sequencyRegistry.CurrentSequenceModel.MoveElement(nextElement, layer, nextElementNewPosition);
                                newEndPosition = newEndPosition + nextElement.Duration;
                                this.View.RefreshElement(nextElement);
                                this.PublishElementMovedEvent(nextElement, ElementPositionType.Position, nextElementOldPosition, nextElementNewPosition);

                                currElement = nextElement;
                                nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(newEndPosition, layer, currElement);
                            }
                        }
                    }

                    break;
            }

            TimeCode oldOutPosition = elementToTrim.OutPosition;
            elementToTrim.OutPosition = newOutPosition;
            this.View.RefreshElement(elementToTrim);
            this.trimMarkersCommand.UpdateInformation(elementToTrim, newOutPosition.TotalSeconds - oldOutPosition.TotalSeconds, elementToTrim.Position.TotalSeconds, ElementPositionType.OutPosition);
            this.PublishElementMovedEvent(elementToTrim, ElementPositionType.OutPosition, oldOutPosition, newOutPosition);

            // tooltip
            var tooltipPosition = TimeCode.FromAbsoluteTime(elementToTrim.Duration.TotalSeconds + elementToTrim.Position.TotalSeconds, this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate);
            var layerPosition = new LayerPosition
            {
                Track = layer,
                Position = tooltipPosition,
                LayerType = layer.TrackType
            };

            this.View.ShowTooltip(tooltipPosition.ToString(), layerPosition);
        }

        private void TrimElementsAtCurrentPosition(ElementPositionType elementPositionType)
        {
            IList<TimelineElement> elements = this.GetElementsAtCurrentPosition();

            if (elements != null && elements.Count > 0)
            {
                IList<LayerSnapshotCommand> layerSnapshotCommands = new List<LayerSnapshotCommand>();

                foreach (TimelineElement element in elements)
                {
                    Track track = this.GetElementTrack(element);
                    IList<TimelineElement> snapshot = track.GetMemento();

                    switch (elementPositionType)
                    {
                        case ElementPositionType.InPosition:
                            this.TrimLeftElement(this.sequencyRegistry.CurrentSequenceModel.CurrentPosition, element);
                            this.View.HideTooltip();
                            break;
                        case ElementPositionType.OutPosition:
                            this.TrimRightElement(this.sequencyRegistry.CurrentSequenceModel.CurrentPosition, element);
                            this.View.HideTooltip();
                            break;
                        default:
                            break;
                    }

                    LayerSnapshotCommand command = new LayerSnapshotCommand(this.sequencyRegistry.CurrentSequenceModel, track, snapshot, track.GetMemento());
                    layerSnapshotCommands.Add(command);
                }

                CompositeUndoableCommand<LayerSnapshotCommand> timelineSnapshotCommand = new CompositeUndoableCommand<LayerSnapshotCommand>(layerSnapshotCommands);
                this.caretaker.ExecuteCommand(timelineSnapshotCommand);
            }
        }

        /// <summary>
        /// Handles the PositionChange event of the View. Sets the current position to the model 
        /// and notifies about the new position to others.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the new position.</param>
        private void View_PositionChange(object sender, PositionChangeEventArgs e)
        {
            this.SetCurrentPosition(e.NewPosition);
        }

        /// <summary>
        /// Handles the SingleElementSelect event of the View. Unselects the current element and select the new one.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element to select.</param>
        private void ViewSingleSingleElementSelect(object sender, ElementSelectEventArgs e)
        {
            if (e.Element != null)
            {
                this.SelectSingleElement(e.Element);
            }
            else
            {
                this.View.HideTooltip();
            }
        }

        /// <summary>
        /// Handles the ChangeElementPosition event of the View. Based on the type of position change decides what action to do.
        /// (Moves the selected element, trims left the selected element or trims right the selected element).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element to select.</param>
        private void View_ChangeElementPosition(object sender, ElementPositionChangeEventArgs e)
        {
            this.positionType = e.PositionType;

            switch (e.PositionType)
            {
                case ElementPositionType.Position:
                    this.MoveSelectedElement(e.NewPosition);
                    break;
                case ElementPositionType.InPosition:
                    this.TrimLockedElementsLeftSide(e);
                    break;
                case ElementPositionType.OutPosition:
                    this.TrimLockedElementsRightSide(e);
                    break;
            }
        }

        /// <summary>
        /// Trims all locked elements to right position.
        /// </summary>
        /// <param name="e">The event args that contains the element to select.</param>
        private void TrimLockedElementsRightSide(ElementPositionChangeEventArgs e)
        {
            IEnumerable<TimelineElement> lockedElements = this.lockGroupManager.GetGroupedElements(this.selectedElements.First());

            foreach (var element in lockedElements)
            {
                this.TrimRightElement(e.NewPosition, element);
            }
        }

        /// <summary>
        /// Trims all locked elements to lef position.
        /// </summary>
        /// <param name="e">The event args that contains the element to select.</param>
        private void TrimLockedElementsLeftSide(ElementPositionChangeEventArgs e)
        {
            IEnumerable<TimelineElement> lockedElements = this.lockGroupManager.GetGroupedElements(this.selectedElements.First());

            foreach (var element in lockedElements)
            {
                this.TrimLeftElement(e.NewPosition, element);
            }
        }

        /// <summary>
        /// Handles the ShowingLinks event of the View. Shows the links of an element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element which links are going to be shown.</param>
        private void View_ShowingLinks(object sender, ElementLinkEventArgs e)
        {
            this.ShowLinks(e.Element);
        }

        /// <summary>
        /// Handles the HidingLinks event of the View. Hides the links of an element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element which links are going to be hidden.</param>
        private void View_HidingLinks(object sender, ElementLinkEventArgs e)
        {
            this.HideLinks(e.Element);
        }

        /// <summary>
        /// Handles the LinkingElement event of the View. Toggle the linking of an element by executing the a ToggleLinkElementCommand.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element.</param>
        private void View_LinkingElement(object sender, LinkElementEventArgs e)
        {
            if (e.Element == null)
            {
                return;
            }

            Track track = this.GetElementTrack(e.Element);

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequencyRegistry.CurrentSequenceModel, track, e.Element, e.LinkPosition);
            this.caretaker.ExecuteCommand(command);
        }

        /// <summary>
        /// Shows the links of the <paramref name="element"/> passed.
        /// </summary>
        /// <param name="element">The timeline element to show links.</param>
        private void ShowLinks(TimelineElement element)
        {
            if (element == null)
            {
                return;
            }

            Track track = this.GetElementTrack(element);

            TimelineElement previousElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(element.Position, track, element);

            if (previousElement != null)
            {
                bool linked = this.sequencyRegistry.CurrentSequenceModel.IsElementLinkedTo(element, previousElement);
                this.View.ShowLink(LinkPosition.In, linked, element);
            }

            TimelineElement nextElement = this.sequencyRegistry.CurrentSequenceModel.GetElementAtPosition(element.Position + element.Duration, track, element);

            if (nextElement != null)
            {
                bool linked = this.sequencyRegistry.CurrentSequenceModel.IsElementLinkedTo(element, nextElement);
                this.View.ShowLink(LinkPosition.Out, linked, element);
            }
        }

        /// <summary>
        /// Hides the links of the <paramref name="element"/> passed.
        /// </summary>
        /// <param name="element">The timeline element to hide links.</param>
        private void HideLinks(TimelineElement element)
        {
            if (element == null)
            {
                return;
            }

            this.View.HideLink(LinkPosition.In, element);
            this.View.HideLink(LinkPosition.Out, element);
        }

        /// <summary>
        /// Handles the MovingPlayhead event of the view. Publishes the <see cref="PauseEvent"/>.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void View_MovingPlayhead(object sender, EventArgs e)
        {
            this.eventAggregator.GetEvent<PauseEvent>().Publish(null);
        }

        /// <summary>
        /// Publishes the <see cref="PlayerEvent"/>.
        /// </summary>
        private void TogglePlay()
        {
            this.eventAggregator.GetEvent<PlayerEvent>().Publish(new PlayerEventPayload { PlayerMode = PlayerMode.Timeline });
        }

        /// <summary>
        /// Splits the elements under the current position.
        /// </summary>
        private void SplitElements()
        {
            TimeCode currentPosition = this.sequencyRegistry.CurrentSequenceModel.CurrentPosition;
            var originalSelectedElements = new List<TimelineElement>(this.selectedElements);

            foreach (Track track in this.sequencyRegistry.CurrentSequenceModel.Tracks)
            {
                IList<TimelineElement> elements = this.sequencyRegistry.CurrentSequenceModel.GetElementsAtPosition(currentPosition, track);

                if (elements != null && elements.Count > 0)
                {
                    foreach (TimelineElement element in elements)
                    {
                        var videoInOut = element.Asset as VideoAssetInOut;

                        VideoAssetInOut newVideoInOut = null;
                        if (videoInOut != null)
                        {
                            newVideoInOut = videoInOut.Clone();

                            if (videoInOut.AddMarkersToSequence)
                            {
                                this.eventAggregator.GetEvent<DeleteAllPreviewsEvent>().Publish(
                                      new DeleteAllPreviewsPayload(CommentMode.Timeline)
                                      {
                                          ItemsToErase = videoInOut.PlayByPlayFilteredMarkers
                                      });
                            }

                            videoInOut.OutPosition = videoInOut.InPosition + (currentPosition.TotalSeconds - element.Position.TotalSeconds);
                            newVideoInOut.InPosition = TimeCode.FromFrames(
                                TimeCode.FromSeconds(videoInOut.OutPosition, element.Position.FrameRate).TotalFrames + 1,
                                element.Position.FrameRate).TotalSeconds;

                            if (videoInOut.AddMarkersToSequence)
                            {
                                videoInOut.UpdateFilter(element.Position.TotalSeconds);

                                foreach (var playByPlay in videoInOut.PlayByPlayFilteredMarkers)
                                {
                                    this.eventAggregator.GetEvent<AddPreviewEvent>().Publish(new AddPreviewPayload("PlayByPlay", playByPlay, CommentMode.Timeline));
                                }
                            }
                        }

                        var newElement = new TimelineElement
                        {
                            Asset = newVideoInOut ?? element.Asset,
                            InPosition = element.InPosition,
                            OutPosition = element.OutPosition,
                            Position = element.Position,
                            Volume = element.Volume
                        };

                        element.OutPosition = TimeCode.FromSeconds(currentPosition.TotalSeconds - element.Position.TotalSeconds + element.InPosition.TotalSeconds, element.Position.FrameRate);

                        newElement.InPosition = TimeCode.FromFrames(TimeCode.FromSeconds(element.OutPosition.TotalSeconds, element.Position.FrameRate).TotalFrames + 1, element.Position.FrameRate);
                        newElement.Position += TimeCode.FromFrames(element.Duration.TotalFrames + 1, element.Position.FrameRate);

                        this.View.RefreshElement(element);
                        this.AddElement(newElement.Asset, track, newElement.Position, newElement.InPosition.TotalSeconds, newElement.OutPosition.TotalSeconds, true);
                    }
                }
            }

            this.selectedElements = originalSelectedElements;
            this.SelectElementsInView();

            this.SetCurrentPosition(currentPosition);
        }

        private void SelectElementsInView()
        {
            foreach (var element in this.selectedElements)
            {
                this.View.SelectElement(element);
            }
        }

        /// <summary>
        /// Handles the TopBarDoubleClicked event of the View. Publishes the <see cref="PositionDoubleClickedEvent"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the position being clicked.</param>
        private void View_TopBarDoubleClicked(object sender, PositionPayloadEventArgs e)
        {
            this.eventAggregator.GetEvent<PositionDoubleClickedEvent>().Publish(e);
        }

        /// <summary>
        /// Handles the RefreshingElements event of the View. Publishes the <see cref="RefreshElementsEvent"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void View_RefreshingElements(object sender, RefreshElementsEventArgs e)
        {
            this.eventAggregator.GetEvent<RefreshElementsEvent>().Publish(e);
        }

        /// <summary>
        /// Undo the latest operation done. 
        /// </summary>
        private void Undo()
        {
            this.caretaker.Undo();
        }

        /// <summary>
        /// Redo the latest operation undoned.
        /// </summary>
        private void Redo()
        {
            this.caretaker.Redo();
        }

        /// <summary>
        /// Handles the StopMoving event of the view. Executes a <see cref="LayerSnapshotCommand"/> using the track of the asset of the element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element.</param>
        private void View_StopMoving(object sender, Infrastructure.DataEventArgs<TimelineElement> e)
        {
            if (this.layerSnapshot != null)
            {
                Track track = this.GetElementTrack(e.Data);

                LayerSnapshotCommand command = new LayerSnapshotCommand(this.sequencyRegistry.CurrentSequenceModel, track, this.layerSnapshot, track.GetMemento());

                // move the markers and refresh the command. the old one is kept by the caretaker in case undo is required.
                CompositeUndoableCommand<UndoableCommand> compositeUndoableCommand = new CompositeUndoableCommand<UndoableCommand>();
                compositeUndoableCommand.AddCommand(command);

                switch (this.positionType)
                {
                    case ElementPositionType.None:
                        break;
                    case ElementPositionType.InPosition:
                    case ElementPositionType.OutPosition:
                        compositeUndoableCommand.AddCommand(this.trimMarkersCommand);
                        this.trimMarkersCommand = new TrimMarkersCommand(this.eventAggregator);
                        break;
                    case ElementPositionType.Position:
                        compositeUndoableCommand.AddCommand(this.moveMarkerscommand);
                        this.moveMarkerscommand = new MoveMarkersCommand(this.eventAggregator);
                        break;
                }

                this.positionType = ElementPositionType.None;

                this.caretaker.ExecuteCommand(compositeUndoableCommand);

                this.layerSnapshot = null;
            }
        }

        private void ToggleEditMode()
        {
            EditMode newEditMode = this.editMode == EditMode.Gap ? EditMode.Ripple : EditMode.Gap;

            this.NotifyNewEditingMode(newEditMode);
        }

        private void NotifyNewEditingMode(EditMode newEditMode)
        {
            this.editMode = newEditMode;
            this.eventAggregator.GetEvent<EditModeChangedEvent>().Publish(this.editMode);
            this.OnPropertyChanged("IsInRippleMode");
        }

        /// <summary>
        /// Handles the StartMoving event of the View. Gets a memento of the layer of the asset.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element being moved.</param>
        private void View_StartMoving(object sender, Infrastructure.DataEventArgs<TimelineElement> e)
        {
            Track track = this.GetElementTrack(e.Data);
            this.layerSnapshot = track.GetMemento();
        }

        private void PickThumbnail()
        {
            this.eventAggregator.GetEvent<PickThumbnailEvent>().Publish(null);
        }

        /// <summary>
        /// Handles the ElementAdded event of the TimelineModel. Addes the new element to the view, unselects the current element 
        /// and selects the just added element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element added.</param>
        private void TimelineModel_ElementAdded(object sender, TimelineElementEventArgs e)
        {
            if (e.Element != null)
            {
                this.View.AddElement(e.Element);

                this.SelectSingleElement(e.Element);
            }
        }

        private void SelectSingleElement(TimelineElement element)
        {
            foreach (var timelineElement in this.selectedElements)
            {
                this.View.UnselectElement(timelineElement);
            }

            this.selectedElements.Clear();
            this.selectedElements.Add(element);
            this.View.SelectElement(element);
            this.ShowMetadata(element);
        }

        /// <summary>
        /// Handles the ElementRemoved event of the TimelineModel. Removes the element from the view, 
        /// and cleans the unselected element if was the removed element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element removed.</param>
        private void TimelineModel_ElementRemoved(object sender, TimelineElementEventArgs e)
        {
            if (e.Element != null)
            {
                this.View.RemoveElement(e.Element);
                this.selectedElements.Remove(e.Element);

                if (this.selectedElements.Count > 0)
                {
                    this.ShowMetadata(this.selectedElements.FirstOrDefault());
                }
                else
                {
                    this.HideMetadata();
                }
            }
        }

        /// <summary>
        /// Handles the ElementMoved event of the TimelineModel. Refreshes the element moved.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element moved.</param>
        private void TimelineModel_ElementMoved(object sender, TimelineElementEventArgs e)
        {
            if (e.Element != null)
            {
                this.View.RefreshElement(e.Element);
            }
        }

        /// <summary>
        /// Handles the ElementUnliked event of the TimelineModel. Hides the link of the element unliked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element unlinked.</param>
        private void TimelineModel_ElementUnlinked(object sender, TimelineElementEventArgs e)
        {
            if (e.Element != null)
            {
                this.HideLinks(e.Element);
            }
        }

        /// <summary>
        /// Handles the ElementLinked event of the TimelineModel. Shows the links of the element linked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args that contains the element moved.</param>
        private void TimelineModel_ElementLinked(object sender, TimelineElementEventArgs e)
        {
            if (e.Element != null)
            {
                this.ShowLinks(e.Element);
            }
        }

        /// <summary>
        /// Loads the timeline of the current project.
        /// </summary>
        private void LoadTimeline()
        {
            if (this.projectService.State != ProjectState.Retrieved)
            {
                this.projectService.ProjectRetrieved += (sender, e) => this.LoadTimeline(this.projectService.GetCurrentProject());
            }
            else
            {
                this.LoadTimeline(this.projectService.GetCurrentProject());
            }
        }

        /// <summary>
        /// Loads the timeline of the given project.
        /// </summary>
        /// <param name="project">The project with the timeline to be loaded.</param>
        private void LoadTimeline(Project project)
        {
            if (project != null)
            {
                ////this.View.SetStartTimeCode(project.StartTimeCode);

                foreach (var timeline in project.Timelines)
                {
                    ISequenceModel sequence = this.sequencyRegistry.CreateSequence(timeline);

                    foreach (var track in timeline.Tracks)
                    {
                        ////sequence.AddTrack(track);

                        TimelineElement[] currentShots = new TimelineElement[track.Shots.Count];
                        track.Shots.CopyTo(currentShots);

                        track.Shots.Clear();

                        foreach (TimelineElement element in currentShots)
                        {
                            // TODO: Add overloads that receives an element
                            this.AddElement(element.Asset, track, element.Position, element.InPosition.TotalSeconds, element.OutPosition.TotalSeconds, true);

                            this.selectedElements.Last().Volume = element.Volume;
                            this.selectedElements.Last().ProviderUri = element.ProviderUri;

                            foreach (Comment comment in element.Comments)
                            {
                                this.selectedElements.Last().Comments.Add(comment);
                            }
                        }
                    }
                }

                this.SetDuration(TimeCode.FromAbsoluteTime(this.defaultTimelineDuration, SmpteFrameRate.Smpte2997NonDrop));

                this.View.SetStartTimeCode(project.StartTimeCode);

                ObservableCollection<AdOpportunity> adOpportunities = this.sequencyRegistry.CurrentSequence.AdOpportunities;
                this.PublishNewAdOpportunities(adOpportunities);

                ObservableCollection<Marker> markers = this.sequencyRegistry.CurrentSequence.Markers;
                this.PublishNewMarkers(markers);
            }
        }

        private void PublishNewMarkers(ObservableCollection<Marker> markers)
        {
            for (int i = 0; i < markers.Count(); i++)
            {
                this.eventAggregator.GetEvent<AddPreviewEvent>().Publish(new AddPreviewPayload("Marker", markers[i], CommentMode.Timeline));
            }
        }

        private void PublishNewAdOpportunities(ObservableCollection<AdOpportunity> adOpportunities)
        {
            for (int i = 0; i < adOpportunities.Count(); i++)
            {
                this.eventAggregator.GetEvent<AddPreviewEvent>().Publish(new AddPreviewPayload("Ad", adOpportunities[i], CommentMode.Timeline));
            }
        }

        /// <summary>
        /// Handles the CollectionChanged event of the tracks collection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args containing event data.</param>
        private void Tracks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems.Count > 0)
                    {
                        Track track = e.NewItems[0] as Track;

                        if (track != null)
                        {
                            if (track.TrackType == TrackType.Audio)
                            {
                                this.AudioTracks.Add(track);

                                this.AddAudioTrackCommand.RaiseCanExecuteChanged();
                                this.RemoveAudioTrackCommand.RaiseCanExecuteChanged();
                            }

                            if (track.TrackType == TrackType.Visual)
                            {
                                this.VideoTracks.Add(track);
                            }

                            if (this.channelsConventionEnabled)
                            {
                                track.ApplyChannelConvention();
                            }
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems.Count > 0)
                    {
                        Track track = e.OldItems[0] as Track;

                        if (track != null)
                        {
                            if (track.TrackType == TrackType.Audio)
                            {
                                this.AudioTracks.Remove(track);

                                this.AddAudioTrackCommand.RaiseCanExecuteChanged();
                                this.RemoveAudioTrackCommand.RaiseCanExecuteChanged();
                            }

                            if (track.TrackType == TrackType.Visual)
                            {
                                this.VideoTracks.Remove(track);
                            }
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Adds an audio track to the tracks collection.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void AddAudioTrack(object parameter)
        {
            int number = this.sequencyRegistry.CurrentSequenceModel.Tracks.Where(x => x.TrackType == TrackType.Audio).Max(x => x.Number);

            Track track = new Track { Number = number + 1, TrackType = TrackType.Audio, IsMuted = true, Volume = 1 };

            this.sequencyRegistry.CurrentSequence.Tracks.Add(track);
            this.sequencyRegistry.CurrentSequenceModel.Tracks.Add(track);
        }

        /// <summary>
        /// Evaluates if an audio track can be added or not.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>A true if the track can be added;otherwise false.</returns>
        private bool CanAddAudioTrack(object parameter)
        {
            return this.sequencyRegistry.CurrentSequenceModel != null && this.sequencyRegistry.CurrentSequenceModel.Tracks.Count(x => x.TrackType == TrackType.Audio) < this.maxNumberOfAudioTracks;
        }

        /// <summary>
        /// Removes the last audio tracks from the tracks collection.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void RemoveAudioTrack(object parameter)
        {
            int number = this.sequencyRegistry.CurrentSequenceModel.Tracks.Where(x => x.TrackType == TrackType.Audio).Max(x => x.Number);
            Track track = this.sequencyRegistry.CurrentSequenceModel.Tracks.Single(x => x.TrackType == TrackType.Audio && x.Number == number);

            TimelineElement[] currentShots = new TimelineElement[track.Shots.Count];
            track.Shots.CopyTo(currentShots);

            foreach (TimelineElement element in currentShots)
            {
                this.DeleteElement(element);
            }

            this.sequencyRegistry.CurrentSequence.Tracks.Remove(track);
            this.sequencyRegistry.CurrentSequenceModel.Tracks.Remove(track);
        }

        /// <summary>
        /// Evaluates if an audio track can be removed.
        /// </summary>
        /// <param name="parameter">The command paramter.</param>
        /// <returns>A true if the audio track can be removed;otherwise false.</returns>
        private bool CanRemoveAudioTrack(object parameter)
        {
            return this.sequencyRegistry.CurrentSequenceModel != null && this.sequencyRegistry.CurrentSequenceModel.Tracks.Count(x => x.TrackType == TrackType.Audio) > 1;
        }

        /// <summary>
        /// Move the current position by one frame (backward or forward).
        /// </summary>
        /// <param name="frames">The number of frames being added/removed to the current position.</param>
        private void MoveFrame(object frames)
        {
            TimeCode currentTimeCode = this.sequencyRegistry.CurrentSequenceModel.CurrentPosition;

            bool add = long.Parse(frames.ToString(), CultureInfo.InvariantCulture) > 0;

            TimeCode frameTimeCode = TimeCode.FromFrames(1, this.sequencyRegistry.CurrentSequenceModel.CurrentPosition.FrameRate);

            currentTimeCode = add ? currentTimeCode.Add(frameTimeCode) : currentTimeCode.Subtract(frameTimeCode);

            this.SetCurrentPosition(currentTimeCode);
        }

        private void MoveToNextClip(object payload)
        {
            Track track = this.sequencyRegistry.CurrentSequenceModel.Tracks.Single(x => x.TrackType == TrackType.Visual);

            TimeCode currentPosition = this.sequencyRegistry.CurrentSequenceModel.CurrentPosition;

            TimelineElement nextElement = this.sequencyRegistry.CurrentSequenceModel.GetNextElement(currentPosition, track);

            if (nextElement != null)
            {
                this.SetCurrentPosition(nextElement.Position.Add(TimeCode.FromFrames(1, nextElement.Position.FrameRate)));
            }
        }

        private void MoveToPreviousClip(object payload)
        {
            Track track = this.sequencyRegistry.CurrentSequenceModel.Tracks.Single(x => x.TrackType == TrackType.Visual);

            TimeCode currentPosition = this.sequencyRegistry.CurrentSequenceModel.CurrentPosition;

            TimelineElement previousElement = this.sequencyRegistry.CurrentSequenceModel.GetPreviousElement(currentPosition, track);

            if (previousElement != null)
            {
                TimeCode position = previousElement.Position.TotalSeconds == 0 ? previousElement.Position : previousElement.Position.Add(TimeCode.FromFrames(1, previousElement.Position.FrameRate));
                this.SetCurrentPosition(position);
            }
        }

        private void ExecuteKeyboardAction(Tuple<KeyboardAction, object> parameter)
        {
            switch (parameter.Item1)
            {
                case KeyboardAction.Align:
                    this.AlignSelectedElementsToPlayheadPosition();
                    break;

                case KeyboardAction.Split:
                    if (!this.IsTimelineLocked)
                    {
                        this.SplitElements();
                    }

                    break;

                case KeyboardAction.Delete:
                    this.DeleteSelectedElementAndElementsInGroup();
                    break;

                case KeyboardAction.TrimElementIn:
                    if (!this.IsTimelineLocked)
                    {
                        this.TrimElementsAtCurrentPosition(ElementPositionType.InPosition);
                    }

                    break;

                case KeyboardAction.TrimElementOut:
                    if (!this.IsTimelineLocked)
                    {
                        this.TrimElementsAtCurrentPosition(ElementPositionType.OutPosition);
                    }

                    break;

                case KeyboardAction.PreviousClip:
                    this.MoveToPreviousClip(null);
                    break;

                case KeyboardAction.NextClip:
                    this.MoveToNextClip(null);

                    break;

                case KeyboardAction.NextFrame:
                    this.MoveFrame(1);
                    break;

                case KeyboardAction.PreviousFrame:
                    this.MoveFrame(-1);
                    break;

                case KeyboardAction.ToggleEdit:
                    if (!this.IsTimelineLocked)
                    {
                        this.ToggleEditMode();
                    }

                    break;

                case KeyboardAction.PickThumbnail:
                    this.PickThumbnail();
                    break;

                case KeyboardAction.Undo:
                    if (!this.IsTimelineLocked)
                    {
                        this.Undo();
                    }

                    break;

                case KeyboardAction.Redo:
                    if (!this.IsTimelineLocked)
                    {
                        this.Redo();
                    }

                    break;

                case KeyboardAction.TogglePlay:
                    this.TogglePlay();
                    break;

                case KeyboardAction.PausePlayer:
                    this.eventAggregator.GetEvent<PauseEvent>().Publish(null);
                    break;

                case KeyboardAction.PlayTimeline:
                    this.eventAggregator.GetEvent<PlayEvent>().Publish(null);
                    break;

                case KeyboardAction.Lock:
                    this.LockCommand.Execute(null);
                    break;

                case KeyboardAction.ZoomIn:
                    this.View.ZoomHandler(TimelineView.Zoom.In);
                    break;

                case KeyboardAction.ZoomOut:
                    this.View.ZoomHandler(TimelineView.Zoom.Out);
                    break;
            }
        }

        private void DeleteSelectedElementAndElementsInGroup()
        {
            if (!this.IsTimelineLocked)
            {
                while (this.selectedElements.Count > 0)
                {
                    IEnumerable<TimelineElement> elements = this.lockGroupManager.GetGroupedElements(this.selectedElements.First());
                    this.DeleteElements(elements);
                }
            }

            this.InvokeTitleUpdated();
            this.UpdateTimelineDuration();
        }

        private void UpdateTimelineDuration()
        {
            if ((this.TimelineDuration.TotalSeconds <= 0) || (this.TimelineDuration.TotalSeconds < this.defaultTimelineDuration))
            {
                // Set the default duration.
                this.SetDuration(
                    TimeCode.FromAbsoluteTime(
                        this.defaultTimelineDuration,
                        this.sequencyRegistry.CurrentSequenceModel.Duration.FrameRate));
            }
            else
            {
                var offset = this.TimelineDuration.TotalSeconds - this.sequencyRegistry.CurrentSequenceModel.Duration.TotalSeconds;
                this.IncreaseTimelineDuration(offset);
            }
        }

        private void View_BalanceChanged(object sender, TrackEventArgs e)
        {
            if (e != null)
            {
                Track track = e.Track;

                if (track != null)
                {
                    track.Balance = e.Value;

                    foreach (TimelineElement timelineElement in track.Shots)
                    {
                        timelineElement.Balance = e.Value;
                    }
                }
            }
        }

        private void View_VolumeChanged(object sender, TrackEventArgs e)
        {
            if (e != null)
            {
                Track track = e.Track;
                if (track != null)
                {
                    track.Volume = e.Value;
                    foreach (TimelineElement timelineElement in track.Shots)
                    {
                        timelineElement.IsRubberbandingEnabled = false;
                        timelineElement.Volume = e.Value;
                    }
                }
            }
        }

        private void View_MuteChanged(object sender, TrackMuteEventArgs e)
        {
            if (e != null)
            {
                Track track = e.Track;
                if (track != null)
                {
                    this.eventAggregator.GetEvent<TrackMuteStateChangedEvent>().Publish(new TrackMuteStateChangedPayload(e.Track.Number, e.Track.IsMuted));
                }
            }
        }

        private void View_MultipleElementSelect(object sender, ElementSelectEventArgs e)
        {
            if (!this.selectedElements.Contains(e.Element))
            {
                this.selectedElements.Add(e.Element);
                this.View.SelectElement(e.Element);

                if (this.selectedElements.Count == 1)
                {
                    this.ShowMetadata(e.Element);
                }
            }
        }

        private void ExecuteAlign(object obj)
        {
            this.AlignSelectedElementsToPlayheadPosition();
        }

        private void ExecuteLock(object obj)
        {
            this.LockElements(this.selectedElements);
        }

        private void LockElements(IEnumerable<TimelineElement> elements)
        {
            int groupId = this.lockGroupManager.LockElements(elements);
            foreach (var element in elements)
            {
                this.View.UpdateElementLockGroup(element, groupId);
            }
        }

        private void Unlock(TimelineElement element)
        {
            // mark elements in group as unlocked
            var elementGroup = this.lockGroupManager.GetGroupedElements(element);
            foreach (var timelineElement in elementGroup)
            {
                // -1 means no group
                this.View.UpdateElementLockGroup(timelineElement, -1);
            }

            // unlock guop that element belongs to
            this.lockGroupManager.UnlockElementLockGroup(element);

            // update elements in other groups
            foreach (var group in this.lockGroupManager.LockGroups)
            {
                foreach (var timelineElement in group)
                {
                    this.View.UpdateElementLockGroup(timelineElement, group.Id);
                }
            }
        }

        private void View_ElementLocked(object sender, EventArgs e)
        {
            this.ExecuteLock(null);
        }

        private void View_ElementUnlocked(object sender, Infrastructure.DataEventArgs<TimelineElement> e)
        {
            this.Unlock(e.Data);
        }

        private void HandleSequenceChanged(object sender, Infrastructure.DataEventArgs<ISequenceModel> e)
        {
            if (e != null && e.Data != null)
            {
                this.RemoveElementFromPreviousSequence(e.Data);

                this.AudioTracks.Clear();
                this.VideoTracks.Clear();

                this.PopulateTracks(this.sequencyRegistry.CurrentSequenceModel);
                this.AddElementsFromNewSequence();
                this.LockElementFromNewSequence();

                this.UnsubscribeFromSequenceEvents(e.Data);
                this.selectedElements.Clear();
                this.HideMetadata();

                IEnumerable<FrameworkElement> trackDropZones = new List<FrameworkElement>(DragDropManager.DropZones.Where(dz => dz.DataContext is Track));
                foreach (var frameworkElement in trackDropZones)
                {
                    DragDropManager.DropZones.Remove(frameworkElement);
                }
            }

            this.UnsubscribeFromSequenceEvents(this.sequencyRegistry.CurrentSequenceModel);
            this.SubscribeToSequenceEvents(this.sequencyRegistry.CurrentSequenceModel, this.sequencyRegistry.CurrentSequence);
            this.InvokeTitleUpdated();
        }

        private void LockElementFromNewSequence()
        {
            foreach (var track in this.VideoTracks)
            {
                foreach (var element in track.Shots)
                {
                    this.LockElements(this.lockGroupManager.GetGroupedElements(element));
                }
            }

            foreach (var track in this.AudioTracks)
            {
                foreach (var element in track.Shots)
                {
                    this.LockElements(this.lockGroupManager.GetGroupedElements(element));
                }
            }
        }

        private void AddElementsFromNewSequence()
        {
            foreach (var track in this.sequencyRegistry.CurrentSequenceModel.Tracks)
            {
                foreach (var element in track.Shots)
                {
                    this.View.AddElement(element);
                }
            }
        }

        private void PopulateTracks(ISequenceModel sequence)
        {
            foreach (var track in sequence.Tracks)
            {
                if (track.TrackType == TrackType.Audio)
                {
                    this.AudioTracks.Add(track);
                }

                if (track.TrackType == TrackType.Visual)
                {
                    this.VideoTracks.Add(track);
                }
            }
        }

        private void SubscribeToSequenceEvents(ISequenceModel sequenceModel, Sequence sequence)
        {
            sequenceModel.ElementAdded += this.TimelineModel_ElementAdded;
            sequenceModel.ElementRemoved += this.TimelineModel_ElementRemoved;
            sequenceModel.ElementMoved += this.TimelineModel_ElementMoved;
            sequenceModel.ElementLinked += this.TimelineModel_ElementLinked;
            sequenceModel.ElementUnlinked += this.TimelineModel_ElementUnlinked;
            sequenceModel.Tracks.CollectionChanged += this.Tracks_CollectionChanged;
            sequence.PropertyChanged += this.CurrentSequencePropertyChanged;
        }

        private void UnsubscribeFromSequenceEvents(ISequenceModel sequenceModel)
        {
            sequenceModel.ElementAdded -= this.TimelineModel_ElementAdded;
            sequenceModel.ElementRemoved -= this.TimelineModel_ElementRemoved;
            sequenceModel.ElementMoved -= this.TimelineModel_ElementMoved;
            sequenceModel.ElementLinked -= this.TimelineModel_ElementLinked;
            sequenceModel.ElementUnlinked -= this.TimelineModel_ElementUnlinked;
            sequenceModel.Tracks.CollectionChanged -= this.Tracks_CollectionChanged;
        }

        private void CurrentSequencePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                this.InvokeTitleUpdated();
            }
        }

        private void InvokeTitleUpdated()
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.TitleUpdated;
            if (handler != null)
            {
                handler(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        private void RemoveElementFromPreviousSequence(ISequenceModel sequenceModel)
        {
            foreach (var videoTrack in this.VideoTracks)
            {
                foreach (var shot in videoTrack.Shots)
                {
                    this.View.RemoveElement(shot);
                }
            }

            foreach (var audioTrack in this.AudioTracks)
            {
                foreach (var shot in audioTrack.Shots)
                {
                    this.View.RemoveElement(shot);
                }
            }

            var overlayTracks = sequenceModel.Tracks.Where(t => t.TrackType == TrackType.Overlay);

            if (overlayTracks != null)
            {
                foreach (var track in overlayTracks)
                {
                    foreach (var shot in track.Shots)
                    {
                        this.View.RemoveElement(shot);
                    }
                }
            }
        }

        private void ShowMetadata(TimelineElement timelineElement)
        {
            this.eventAggregator.GetEvent<ShowMetadataEvent>().Publish(timelineElement);
        }

        private void HideMetadata()
        {
            this.eventAggregator.GetEvent<HideMetadataEvent>().Publish(null);
        }

        private void RubberBandingToggleChecked(object paramter)
        {
            double volume = this.VideoTracks[0].Volume;
            this.eventAggregator.GetEvent<RubberBandingStateChangedEvent>().Publish(new RubberBandingStateChangedPayload(this.IsRubberBandingEnabled, volume));
        }
    }
}