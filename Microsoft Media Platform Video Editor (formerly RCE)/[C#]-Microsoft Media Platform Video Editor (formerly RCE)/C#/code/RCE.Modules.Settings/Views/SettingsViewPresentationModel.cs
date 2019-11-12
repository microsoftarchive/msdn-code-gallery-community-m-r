// <copyright file="SettingsViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SettingsViewPresentationModel.cs                     
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.Practices.Composite.Regions;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;
    using Silverlight.Samples;
    using SMPTETimecode;
    using Project = RCE.Infrastructure.Models.Project;

    /// <summary>
    /// Provides the implementation for <see cref="ISettingsViewPresentationModel"/>.
    /// </summary>
    public class SettingsViewPresentationModel : BaseModel, IWindowMetadataProvider, ISettingsViewPresentationModel
    {
        /// <summary>
        /// Default Timeline Duration. This value is common between timeline / setting presenter
        /// if there is any change in timeline value then we have to change this value too.
        /// </summary>
        public const int DefaultTimelineDuration = 7200;

        private const long OneMegabyteToByte = 1048576;

        /// <summary>
        /// Bytes to increase quota.
        /// </summary>
        private const long MinimumRequiredQuota = OneMegabyteToByte * 500;

        /// <summary>
        /// The <seealso cref="IEventAggregator"/> instance used to publish and subscribe to events.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// The <seealso cref="IProjectService"/> instance used to save the current project.
        /// </summary>
        private readonly IProjectService projectService;

        /// <summary>
        /// The <seealso cref="DispatcherTimer"/> used to save the project at a specified interval.
        /// </summary>
        private readonly DispatcherTimer timer;

        /// <summary>
        /// The <see cref="IRegionManager"/>.
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// The command to delete the current thumbnail.
        /// </summary>
        private readonly DelegateCommand<object> deleteThumbnailCommand;

        /// <summary>
        /// The command to pick a new thumbnail.
        /// </summary>
        private readonly DelegateCommand<object> pickThumbnailCommand;

        private readonly IUserSettingsService userSettingsService;

        private readonly IPersistenceService persistenceService;

        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The private bool used to track the selected aspect ratio.
        /// </summary>
        private bool isAspectRatio43Selected;

        /// <summary>
        /// The private bool used to track the selected aspect ratio.
        /// </summary>
        private bool isAspectRatio169Selected;

        /// <summary>
        /// The private string that represents available and cache sizes.
        /// </summary>
        private string storageSize;

        /// <summary>
        /// The private string used to track the selected Spmte timecode.
        /// </summary>
        private string selectedSmpteTimeCode;

        /// <summary>
        /// The private string used to track the selected start timecode.
        /// </summary>
        private string selectedStartTimeCode;

        /// <summary>
        /// The private string used to track the selected edit mode.
        /// </summary>
        private string selectedEditMode;

        /// <summary>
        /// The private string used to track the selected auto save time interval.
        /// </summary>
        private int selectedAutoSaveTimeInterval;

        /// <summary>
        /// The Project Thumbnail image.
        /// </summary>
        private ImageSource thumbImage;

        /// <summary>
        /// Name of the project.
        /// </summary>
        private string projectName;

        /// <summary>
        /// Wheter gaps should be treated as errors or not.
        /// </summary>
        private bool treatGapAsError;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewPresentationModel"/> class.
        /// </summary>
        /// <param name="view">The <see cref="ISettingsView"/> view instance.</param>
        /// <param name="eventAggregator">The <seealso cref="IEventAggregator"/> service used to publish and subscribe to events.</param>
        /// <param name="projectService">The <see cref="IProjectService"/> service instance used to save the current project.</param>
        /// <param name="regionManager">The <see cref="IRegionManager"/>.</param>
        /// <param name="userSettingsService">The <see cref="IUserSettingsService"/>.</param>
        /// <param name="persistenceService"></param>
        /// <param name="configurationService"></param>
        public SettingsViewPresentationModel(ISettingsView view, IEventAggregator eventAggregator, IProjectService projectService, IRegionManager regionManager, IUserSettingsService userSettingsService, IPersistenceService persistenceService, IConfigurationService configurationService)
        {
            this.eventAggregator = eventAggregator;
            this.projectService = projectService;

            this.regionManager = regionManager;
            this.userSettingsService = userSettingsService;
            this.persistenceService = persistenceService;
            this.configurationService = configurationService;

            this.selectedAutoSaveTimeInterval = 180;

            this.timer = new DispatcherTimer();
            this.timer.Tick += (sender, e) => this.SaveProject();

            this.deleteThumbnailCommand = new DelegateCommand<object>(this.DeleteThumbnail, this.CanDeleteThumbnail);
            this.pickThumbnailCommand = new DelegateCommand<object>(this.PickThumbnail);

            this.IncreaseStorageQuotaCommand = new DelegateCommand<object>(this.IncreaseStorageQuota);
            this.ClearStorageCommand = new DelegateCommand<object>(this.ClearStorage);
            this.ApplyKeyboardMappingCommand = new DelegateCommand<object>(this.ApplyKeyboardMapping);
            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.ExecuteKeyboardAction);
            this.ResetWindowsCommand = new DelegateCommand<object>(this.RaiseResetWindows);
            this.RefreshStorageSize();

            this.SmpteTimeCodeValues = new List<string> { "SMPTE Non-Drop", "SMPTE Drop", "SMPTE 30", "SMPTE EBU", "SMPTE Film Sync", "SMPTE Film Sync IVTC", "Seconds" };
            this.EditModeValues = new List<string> { "Gap Mode", "Ripple Mode" };

            this.KeyboardMappings = this.configurationService.GetKeyboardMappings();

            this.eventAggregator.GetEvent<EditModeChangedEvent>().Subscribe(this.SetEditingMode, true);
            this.eventAggregator.GetEvent<ThumbnailEvent>().Subscribe(this.SetThumbnail, ThreadOption.PublisherThread, true, this.FilterAddThumbnailEvent);
            this.eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);

            this.PropertyChanged += this.SettingsViewPresentationModel_PropertyChanged;

            this.IsAspectRatio169Selected = true;
            this.treatGapAsError = this.configurationService.GetTreatGapAsError();
            this.LoadSettings();
            this.View = view;
            this.View.IsActiveChanged += (sender, e) => this.RefreshStorageSize();

            this.UserSettings = this.userSettingsService.GetSettings();

            this.View.Model = this;
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        public DelegateCommand<object> ResetWindowsCommand { get; set; }

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
                return "Settings";
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return ResizeDirection.Vertical;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(450, 800);
            }
        }

        public UserSettings UserSettings { get; set; }

        /// <summary>
        /// Gets a collection of Smpte timecode values.
        /// </summary>
        /// <value>A <see also="IList{T}"/> of <see cref="string"/> with the Smpte timecode values.</value>
        public IList<string> SmpteTimeCodeValues { get; private set; }

        /// <summary>
        /// Gets a collection of Keyboard Mapping values.
        /// </summary>
        /// <value>A <see also="IList{T}"/> of <see cref="string"/> with the Keyboard Mapping values.</value>
        public IEnumerable<KeyboardMapping> KeyboardMappings { get; private set; }

        /// <summary>
        /// Gets a collection of Edit mode values.
        /// </summary>
        /// <value>A <see also="IList{T}"/> of <see cref="string"/> with the edit mode values.</value>
        public IList<string> EditModeValues { get; private set; }

        /// <summary>
        /// Gets string to show on UI.
        /// </summary>
        public string StorageSize
        {
            get
            {
                return this.storageSize;
            }

            private set
            {
                this.storageSize = value;
                this.OnPropertyChanged("StorageSize");
            }
        }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string ProjectName
        {
            get
            {
                return this.projectName;
            }

            set
            {
                ValidateProjectName(value);
                this.projectName = value;
                this.OnPropertyChanged("ProjectName");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the 4:3 aspect ratio is selected.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents if the 4:3 aspect ratio is selected. true, indicates that the 4:3 aspect ratio is selected. false, that is not selected.</value>
        public bool IsAspectRatio43Selected
        {
            get
            {
                return this.isAspectRatio43Selected;
            }

            set
            {
                this.isAspectRatio43Selected = value;
                if (value)
                {
                    this.OnPropertyChanged("IsAspectRatio43Selected");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the 16:9 aspect ratio is selected.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents if the 16:9 aspect ratio is selected. true, indicates that the 16:9 aspect ratio is selected. false, that is not selected.</value>
        public bool IsAspectRatio169Selected
        {
            get
            {
                return this.isAspectRatio169Selected;
            }

            set
            {
                this.isAspectRatio169Selected = value;
                if (value)
                {
                    this.OnPropertyChanged("IsAspectRatio169Selected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected Smpte timecode.
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the selected Smpte timecode.</value>
        public string SelectedSmpteTimeCode
        {
            get
            {
                return this.selectedSmpteTimeCode;
            }

            set
            {
                this.selectedSmpteTimeCode = value;
                this.OnPropertyChanged("SelectedSmpteTimeCode");
            }
        }

        /// <summary>
        /// Gets or sets if should treat GAPs as errors
        /// </summary>
        public bool TreatGapAsError
        {
            get
            {
                return this.treatGapAsError;
            }

            set
            {
                this.treatGapAsError = value;
                this.OnPropertyChanged("TreatGapAsError");
            }
        }

        /// <summary>
        /// Gets or sets the selected start timecode.
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the selected start timecode.</value>
        public string SelectedStartTimeCode
        {
            get
            {
                return this.selectedStartTimeCode;
            }

            set
            {
                ValidateStartTimeCode(value.Trim(), this.GetSmpteFrameRateValue());
                this.selectedStartTimeCode = value.Trim();
                this.OnPropertyChanged("SelectedStartTimeCode");
            }
        }

        /// <summary>
        /// Gets or sets the selected edit mode.
        /// </summary>
        /// <value>An <seealso cref="String" /> that represents the selected edit mode.</value>
        public string SelectedEditMode
        {
            get
            {
                return this.selectedEditMode;
            }

            set
            {
                this.selectedEditMode = value;
                this.OnPropertyChanged("SelectedEditMode");
            }
        }

        /// <summary>
        /// Gets the command used to pick a new thumbnail.
        /// </summary>
        /// <value>The command to pick a new thumbnail.</value>
        public DelegateCommand<object> PickThumbnailCommand
        {
            get { return this.pickThumbnailCommand; }
        }

        /// <summary>
        /// Gets or sets the selected auto save time interval used to save the project periodically.
        /// </summary>
        /// <value>An <seealso cref="int" /> that represents the selected auto save time interval.</value>
        public int SelectedAutoSaveTimeInterval
        {
            get
            {
                return this.selectedAutoSaveTimeInterval;
            }

            set
            {
                if (value != this.selectedAutoSaveTimeInterval)
                {
                    this.selectedAutoSaveTimeInterval = value;
                    this.OnPropertyChanged("SelectedAutoSaveTimeInterval");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ISettingsView"/> of the presentation model.
        /// </summary>
        /// <value>A <seealso cref="ISettingsView"/> that represents the current view of the presentation model.</value>
        public ISettingsView View { get; set; }

        /// <summary>
        /// Gets or sets the project thumbnail.
        /// </summary>
        /// <value>An ImageSource that represents the project thumbnail.</value>
        public ImageSource ThumbImage
        {
            get
            {
                return this.thumbImage;
            }

            set
            {
                this.thumbImage = value;
                this.OnPropertyChanged("ThumbImage");
            }
        }

        /// <summary>
        /// Gets the command used to delete the current thumbnail.
        /// </summary>
        /// <value>The command to delete the current thumbnail.</value>
        public DelegateCommand<object> DeleteThumbnailCommand
        {
            get { return this.deleteThumbnailCommand; }
        }

        /// <summary>
        /// Gets the header info.
        /// </summary>
        /// <value>A <seealso cref="string"/> that represents the header info.</value>
        public string HeaderInfo
        {
            get { return Resources.Resources.HeaderInfo; }
        }

        /// <summary>
        /// Gets the header icon (on status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon on resource.</value>
        public string HeaderIconOn
        {
            get { return Resources.Resources.HeaderIconOn; }
        }

        /// <summary>
        /// Gets the header icon (off status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon off resource.</value>
        public string HeaderIconOff
        {
            get { return Resources.Resources.HeaderIconOff; }
        }

        public DelegateCommand<object> IncreaseStorageQuotaCommand { get; private set; }

        public DelegateCommand<object> ClearStorageCommand { get; private set; }

        public DelegateCommand<object> ApplyKeyboardMappingCommand { get; private set; }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                return KeyboardActionContext.Settings;
            }
        }

        /// <summary>
        /// Save the current project.
        /// </summary>
        public void SaveProject()
        {
            this.userSettingsService.SaveSettings(this.UserSettings);
            this.FillProjectProperties();
            this.projectService.SaveProject();
        }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        /// <summary>
        /// Validates that the string provided is a valid TimeCode.
        /// </summary>
        /// <param name="timeCode">String that is the time code.</param>
        /// <param name="smpteRate">Current smpte rate.</param>
        /// <exception cref="InputValidationException"></exception>
        private static void ValidateStartTimeCode(string timeCode, SmpteFrameRate smpteRate)
        {
            try
            {
                if (!TimeCode.ValidateSmpte12MTimecode(timeCode))
                {
                    throw new InputValidationException(Resources.Resources.InvalidStartTimeCode);
                }

                if (TimeCode.FromAbsoluteTime(DefaultTimelineDuration, smpteRate) <= new TimeCode(timeCode, smpteRate))
                {
                    throw new InputValidationException(string.Format(CultureInfo.InvariantCulture, Resources.Resources.InvalidStartTimeCodeValue, TimeCode.FromAbsoluteTime(DefaultTimelineDuration, smpteRate)));
                }
            }
            catch (Exception ex)
            {
                throw new InputValidationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Validates the name of the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        private static void ValidateProjectName(string projectName)
        {
            if (string.IsNullOrEmpty(projectName) || projectName.Length > 100)
            {
                throw new InputValidationException(Resources.Resources.InvalidProjectName);
            }
        }

        /// <summary>
        /// Encodes the given writeable bitmap using a <seealso cref="PngEncoder"/>.
        /// </summary>
        /// <param name="writeableBitmap">The bitmap to encode.</param>
        /// <returns>A base 64 representation of the bitmap encoded.</returns>
        private static string EncodeImage(WriteableBitmap writeableBitmap)
        {
            string projectThumbnail = null;

            MemoryStream ms = null;

            try
            {
                ms = PngEncoder.Encode(writeableBitmap) as MemoryStream;
            }
            catch (Exception ex)
            {
                // TODO: log exception
            }

            if (ms != null)
            {
                BinaryReader binaryReader = new BinaryReader(ms);
                byte[] currentBytes = binaryReader.ReadBytes(1000);
                int bytesRead = currentBytes.Length;
                List<byte> byteList = currentBytes.ToList();

                while (bytesRead == 1000)
                {
                    currentBytes = binaryReader.ReadBytes(1000);
                    bytesRead = currentBytes.Length;
                    byteList.AddRange(currentBytes);
                }

                binaryReader.Close();
                ms.Close();

                projectThumbnail = Convert.ToBase64String(byteList.ToArray());
            }

            return projectThumbnail;
        }

        private void RefreshStorageSize()
        {
            this.StorageSize = string.Format(CultureInfo.InvariantCulture, "{0} MB / {1} MB", this.persistenceService.UsedSize / OneMegabyteToByte, this.persistenceService.Quota / OneMegabyteToByte);
        }

        private void IncreaseStorageQuota(object obj)
        {
            this.persistenceService.IncreaseQuota(MinimumRequiredQuota);
            this.RefreshStorageSize();
        }

        private void ClearStorage(object obj)
        {
            string settings = (string)this.persistenceService.GetApplicationSettings()["rceSettings"];
            IDictionary<string, string> queryString = (IDictionary<string, string>)this.persistenceService.GetApplicationSettings()["rceQueryString"];

            this.persistenceService.RemoveStorage();
            this.RefreshStorageSize();

            this.persistenceService.AddApplicationSettings("rceSettings", settings);
            this.persistenceService.AddApplicationSettings("rceQueryString", queryString);
        }

        private void ApplyKeyboardMapping(object obj)
        {
            this.userSettingsService.SaveSettings(this.UserSettings);
        }

        /// <summary>
        /// Determines if the thumbnail can be deleted or not.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>A true if the thumbnail can be deleted;otherwise false.</returns>
        private bool CanDeleteThumbnail(object parameter)
        {
            if (this.projectService.State != ProjectState.Retrieved || this.projectService.GetCurrentProject() != null)
            {
                return false;
            }

            return !string.IsNullOrEmpty(this.projectService.GetCurrentProject().ProjectThumbnail);
        }

        /// <summary>
        /// Deletes the current project thumbnail.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void DeleteThumbnail(object parameter)
        {
            this.projectService.GetCurrentProject().ProjectThumbnail = null;
            this.ThumbImage = new BitmapImage();
            this.DeleteThumbnailCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Publish an event to start the process of picking a thumbnail.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void PickThumbnail(object parameter)
        {
            this.eventAggregator.GetEvent<PickThumbnailEvent>().Publish(null);
        }

        /// <summary>
        /// Sets the current edit mode.
        /// </summary>
        /// <param name="editMode">New edit mode value.</param>
        private void SetEditingMode(EditMode editMode)
        {
            if (editMode != this.GetEditModeValue())
            {
                this.SelectedEditMode = this.GetEditModeValue(editMode);
            }
        }

        /// <summary>
        /// Loads the settings once th project was retrieved.
        /// </summary>
        private void LoadSettings()
        {
            if (this.projectService.State != ProjectState.Retrieved)
            {
                this.projectService.ProjectRetrieved += (sender, e) => this.LoadSettings(this.projectService.GetCurrentProject());
            }
            else
            {
                this.LoadSettings(this.projectService.GetCurrentProject());
            }
        }

        private void RaiseResetWindows(object parameter)
        {
            this.eventAggregator.GetEvent<ResetWindowsEvent>().Publish(null);
        }

        /// <summary>
        /// Loads the settings based on the project values.
        /// </summary>
        /// <param name="project">The project that contains the settings values.</param>
        private void LoadSettings(Project project)
        {
            if (project != null)
            {
                this.SelectedEditMode = project.RippleMode ? this.GetEditModeValue(EditMode.Ripple) : this.GetEditModeValue(EditMode.Gap);
                this.SelectedSmpteTimeCode = this.GetSmpteFrameRateValue(project.SmpteFrameRate);
                this.SelectedStartTimeCode = project.StartTimeCode.ToString();
                this.SelectedAutoSaveTimeInterval = (int)project.AutoSaveInterval;
                this.ProjectName = project.Name ?? project.Name;
                this.SetThumbnail(project.ProjectThumbnail);
                this.DeleteThumbnailCommand.RaiseCanExecuteChanged();

                this.timer.Interval = new TimeSpan(0, this.selectedAutoSaveTimeInterval, 0);
                this.timer.Start();
            }
        }

        private bool FilterAddThumbnailEvent(ThumbnailEventPayload payload)
        {
            return payload.Type == ThumbnailType.ProjectThumbnail;
        }

        /// <summary>
        /// Fills the settings project properties.
        /// </summary>
        private void FillProjectProperties()
        {
            Project project = this.projectService.GetCurrentProject();
            project.AutoSaveInterval = this.SelectedAutoSaveTimeInterval;
            project.SmpteFrameRate = this.GetSmpteFrameRateValue();
            project.StartTimeCode = new TimeCode(this.SelectedStartTimeCode, project.SmpteFrameRate);
            project.RippleMode = this.GetEditModeValue() == EditMode.Ripple;
        }

        /// <summary>
        /// This method is invoked whenever any property in the SettingsViewPresentationModel 
        /// changes. Based on the property modified, the same is published by invoking the 
        /// corresponding EventHandler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <seealso cref="PropertyChangedEventArgs"/> event args.</param>
        private void SettingsViewPresentationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsAspectRatio43Selected":
                    this.eventAggregator.GetEvent<AspectRatioChangedEvent>().Publish(AspectRatio.Square);
                    break;

                case "IsAspectRatio169Selected":
                    this.eventAggregator.GetEvent<AspectRatioChangedEvent>().Publish(AspectRatio.Wide);
                    break;

                case "SelectedSmpteTimeCode":
                    SmpteFrameRate frameRate = this.GetSmpteFrameRateValue();
                    this.projectService.GetCurrentProject().SmpteFrameRate = frameRate;
                    this.eventAggregator.GetEvent<SmpteTimeCodeChangedEvent>().Publish(frameRate);
                    break;

                case "SelectedStartTimeCode":
                    TimeCode startTimeCode = new TimeCode(this.SelectedStartTimeCode, this.GetSmpteFrameRateValue());
                    this.projectService.GetCurrentProject().StartTimeCode = startTimeCode;
                    this.eventAggregator.GetEvent<StartTimeCodeChangedEvent>().Publish(startTimeCode);
                    break;

                case "SelectedEditMode":
                    EditMode editMode = this.GetEditModeValue();
                    this.projectService.GetCurrentProject().RippleMode = editMode == EditMode.Ripple;
                    this.eventAggregator.GetEvent<EditModeChangedEvent>().Publish(editMode);
                    break;

                case "SelectedAutoSaveTimeInterval":
                    this.projectService.GetCurrentProject().AutoSaveInterval = this.selectedAutoSaveTimeInterval;
                    this.timer.Interval = new TimeSpan(0, this.selectedAutoSaveTimeInterval, 0);
                    break;
                case "TreatGapAsError":
                    this.UpdateSetting("TreatGapAsError", this.TreatGapAsError.ToString());
                    this.eventAggregator.GetEvent<CheckedTreatGapAsErrorEvent>().Publish(this.TreatGapAsError);
                    break;
                case "ProjectName":
                    this.projectService.GetCurrentProject().Name = string.IsNullOrEmpty(this.ProjectName) ? null : this.projectName;
                    break;
            }
        }

        private void UpdateSetting(string key, string value)
        {
            this.configurationService.UpdateParameters(
                new Dictionary<string, string> { { key, value } });
        }

        /// <summary>
        /// This method return the <see cref="SmpteFrameRate"/> values based on the SelectedSMPTETimecode value.
        /// </summary>
        /// <returns>Modifed SmpteFrameRate value.</returns>
        private SmpteFrameRate GetSmpteFrameRateValue()
        {
            switch (this.SelectedSmpteTimeCode)
            {
                case "SMPTE Film Sync":
                    return SmpteFrameRate.Smpte24;
                case "SMPTE EBU":
                    return SmpteFrameRate.Smpte25;
                case "SMPTE Drop":
                    return SmpteFrameRate.Smpte2997Drop;
                case "SMPTE Non-Drop":
                    return SmpteFrameRate.Smpte2997NonDrop;
                case "SMPTE 30":
                    return SmpteFrameRate.Smpte30;
                case "SMPTE Film Sync IVTC":
                    return SmpteFrameRate.Smpte2398;
                case "Seconds":
                    return SmpteFrameRate.Unknown;          // TODO: The SmpteFrameRate enum doesn't have corresponding definition. 
                default:
                    return SmpteFrameRate.Unknown;
            }
        }

        /// <summary>
        /// Gets the string value of the frame rate. 
        /// </summary>
        /// <param name="frameRate">SmpteFrameRate value.</param>
        /// <returns>The string corresponding to the frame rate.</returns>
        private string GetSmpteFrameRateValue(SmpteFrameRate frameRate)
        {
            switch (frameRate)
            {
                case SmpteFrameRate.Smpte2997NonDrop:
                    return this.SmpteTimeCodeValues[0];

                case SmpteFrameRate.Smpte2997Drop:
                    return this.SmpteTimeCodeValues[1];

                case SmpteFrameRate.Smpte30:
                    return this.SmpteTimeCodeValues[2];

                case SmpteFrameRate.Smpte25:
                    return this.SmpteTimeCodeValues[3];

                case SmpteFrameRate.Smpte24:
                    return this.SmpteTimeCodeValues[4];

                case SmpteFrameRate.Smpte2398:
                    return this.SmpteTimeCodeValues[5];

                case SmpteFrameRate.Unknown:
                    return this.SmpteTimeCodeValues[6];

                default:
                    return this.SmpteTimeCodeValues[6];
            }
        }

        /// <summary>
        /// This method return the <see cref="EditMode"/> value based on the SelectedEditMode value.
        /// </summary>
        /// <returns>Modified EditMode value.</returns>
        private EditMode GetEditModeValue()
        {
            switch (this.SelectedEditMode)
            {
                case "Ripple Mode":
                    return EditMode.Ripple;
                default:
                    return EditMode.Gap;
            }
        }

        /// <summary>
        /// Gets the string value of the edit mode.
        /// </summary>
        /// <param name="editMode">Edit mode value.</param>
        /// <returns>The string correspondig to the edit mode.</returns>
        private string GetEditModeValue(EditMode editMode)
        {
            switch (editMode)
            {
                case EditMode.Ripple:
                    return this.EditModeValues[1];
                default:
                    return this.EditModeValues[0];
            }
        }

        /// <summary>
        /// Sets the project thumbnail.
        /// </summary>
        /// <param name="writeableBitmap">The bitmap that represents the project thumbnail.</param>
        private void SetThumbnail(ThumbnailEventPayload payload)
        {
            WriteableBitmap writeableBitmap = payload.Bitmap;

            if (writeableBitmap != null)
            {
                string encodedImage = EncodeImage(writeableBitmap);

                if (!string.IsNullOrEmpty(encodedImage))
                {
                    this.projectService.GetCurrentProject().ProjectThumbnail = encodedImage;
                    this.ThumbImage = writeableBitmap;
                    this.DeleteThumbnailCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Sets the project thumbnail.
        /// </summary>
        /// <param name="thumbArray">The string that represents the project thumbnail.</param>
        private void SetThumbnail(string thumbArray)
        {
            if (!string.IsNullOrEmpty(thumbArray))
            {
                try
                {
                    BitmapImage image = new BitmapImage();

                    byte[] array = Convert.FromBase64String(thumbArray);

                    MemoryStream ms = new MemoryStream(array);
                    image.SetSource(ms);

                    this.ThumbImage = image;
                }
                catch (Exception ex)
                {
                    // TODO: log exception
                }
            }
        }

        /// <summary>
        /// Activates this Setting view.
        /// </summary>
        private void Activate()
        {
            this.regionManager.Regions[RegionNames.MainRegion].Activate(this.View);
        }

        private void ExecuteKeyboardAction(Tuple<KeyboardAction, object> parameter)
        {
            switch (parameter.Item1)
            {
                case KeyboardAction.ActivateModel:
                    this.Activate();
                    break;
            }
        }
    }
}