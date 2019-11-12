//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="BlobUploaderControl.xaml.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
namespace SLBlobUploader.Control
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;
    using SLBlobUploader.Code.Infrastructure;

    /// <summary>
    /// Code file for BlobUpload control.
    /// </summary>
    public partial class MainPage : UserControl
    {
        /// <summary>
        /// Maximum file size in Kb allowed to be uploaded.
        /// </summary>
        private const long MaxFileSizeKb = Constants.MaxFileSizeKB;

        /// <summary>
        /// Text to appear on upload button.
        /// </summary>
        private const string UploadButtonText = "Upload";

        /// <summary>
        /// Text to appear on cancel button.
        /// </summary>
        private const string CancelButtonText = "Cancel";

        /// <summary>
        /// Number of bytes per Kb.
        /// </summary>
        private const long BytesPerKb = 1024;

        /// <summary>
        /// List of files to upload.
        /// </summary>
        private List<UserFile> files = null;

        /// <summary>
        /// Shared access signature URL of the container where to upload files.
        /// </summary>
        private string sasUrl = string.Empty;

        /// <summary>
        /// Value indicates whether SAS URL has expired
        /// </summary>
        private bool sasExpired = false;

        /// <summary>
        /// Timer to time operation.
        /// </summary>
        private DateTime operationStartTime;

        /// <summary>
        /// User file to upload.
        /// </summary>
        private UserFile userFile = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        /// <param name="sasUrl">The SAS URL.</param>
        /// <param name="timeoutSeconds">The time out interval of SAS URL in seconds.</param>
        public MainPage(Uri sasUrl, string timeoutSeconds)
        {
            int timeoutInterval = 0;
            if (sasUrl != null && !string.IsNullOrWhiteSpace(sasUrl.AbsoluteUri) && int.TryParse(timeoutSeconds, out timeoutInterval))
            {
                this.sasUrl = sasUrl.AbsoluteUri;
                var sasExpiryTimer = new DispatcherTimer();
                sasExpiryTimer.Interval = new TimeSpan(0, 0, timeoutInterval);
                sasExpiryTimer.Tick += new EventHandler((o, e) =>
                {
                    this.sasExpired = true;
                    if (this.userFile != null)
                    {
                        this.userFile.CancelUpload();
                    }

                    this.lblMessage.Text = ApplicationResources.SASExpired;
                    this.btnBrowse.IsEnabled = false;
                    this.btnUpload.IsEnabled = false;
                    this.prgUpload.IsIndeterminate = false;
                    this.txtFileName.Text = string.Empty;
                });
                sasExpiryTimer.Start();
                this.InitializeComponent();
                this.btnBrowse.IsEnabled = true;
                this.btnUpload.IsEnabled = false;
            }
        }

        /// <summary>
        /// Called when Browse file button is clicked.
        /// </summary>
        /// <param name="sender">The sender of event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnBrowseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            ////Keeping multi select off for v1.0
            fileDialog.Multiselect = false;
            if ((bool)fileDialog.ShowDialog())
            {
                if (this.files == null)
                {
                    this.files = new List<UserFile>();
                }

                this.txtFileName.Text = fileDialog.File.Name.Substring(
                    0, fileDialog.File.Name.Length > 30 ? 30 : fileDialog.File.Name.Length);
                this.userFile = new UserFile();
                this.userFile.FileName = fileDialog.File.Name;
                this.userFile.FileStream = fileDialog.File.OpenRead();
                this.userFile.UIDispatcher = this.Dispatcher;
                this.userFile.UploadContainerUrl = new Uri(this.sasUrl);
                this.userFile.UploadCompletedEvent += new EventHandler<UploadCompletedEventArgs>(this.OnUploadCompleted);
                if ((this.userFile.FileStream.Length / BytesPerKb) <= MaxFileSizeKb && this.userFile.FileStream.Length > 0)
                {
                    this.files.Add(this.userFile);
                    this.btnUpload.IsEnabled = true;
                }
                else
                {
                    this.lblMessage.Text = this.userFile.FileStream.Length > 0 ?
                        string.Format(CultureInfo.CurrentCulture, ApplicationResources.IllegalMaxFileSize, MaxFileSizeKb / BytesPerKb) : string.Format(CultureInfo.CurrentCulture, ApplicationResources.IllegalMinFileSize, MaxFileSizeKb / 1024);
                    this.lblMessage.Visibility = System.Windows.Visibility.Visible;
                    this.btnUpload.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Called when upload completes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SLBlobUploader.Code.Infrastructure.UploadCompletedEventArgs"/> instance containing the event data.</param>
        private void OnUploadCompleted(object sender, UploadCompletedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
                {
                    if (this.userFile != null)
                    {
                        switch (e.Reason)
                        {
                            case UploadCompleteReason.UploadCommitted:
                                var duration = DateTime.Now - this.operationStartTime;
                                var fileSizeInKB = (float)userFile.FileStream.Length / BytesPerKb;
                                var fileSizeMessage = fileSizeInKB > BytesPerKb ? string.Concat(((float)fileSizeInKB / BytesPerKb).ToString(CultureInfo.CurrentCulture), " MB") : string.Concat(fileSizeInKB.ToString(CultureInfo.CurrentCulture), " KB");
                                this.lblMessage.Text = string.Format(CultureInfo.CurrentCulture, ApplicationResources.SuccessfulUpload, fileSizeMessage, duration.TotalSeconds);
                                break;
                            case UploadCompleteReason.ErrorOccurred:
                                this.lblMessage.Text = string.Format(CultureInfo.CurrentCulture, ApplicationResources.UploadFailed, e.ErrorMessage);
                                break;
                            case UploadCompleteReason.UserCanceled:
                                if (!sasExpired)
                                {
                                    this.lblMessage.Text = string.Format(CultureInfo.CurrentCulture, ApplicationResources.UploadCancelled);
                                }

                                break;
                            default:
                                this.lblMessage.Text = string.Format(CultureInfo.CurrentCulture, ApplicationResources.UnknownErrorOccured);
                                break;
                        }

                        this.userFile = null;
                    }

                    this.btnUpload.Content = UploadButtonText;
                    this.txtFileName.Text = string.Empty;
                    this.prgUpload.IsIndeterminate = false;
                    this.btnUpload.IsEnabled = false;
                    this.btnBrowse.IsEnabled = true;
                });
        }

        /// <summary>
        /// Called when upload file button is clicked.
        /// </summary>
        /// <param name="sender">The sender of event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnUploadFile(object sender, RoutedEventArgs e)
        {
            Button callerButton = sender as Button;
            if (callerButton.Content.ToString().Equals(UploadButtonText, StringComparison.OrdinalIgnoreCase))
            {
                callerButton.Content = CancelButtonText;
                this.prgUpload.IsIndeterminate = true;
                this.btnUpload.IsEnabled = true;
                this.btnBrowse.IsEnabled = false;
                this.operationStartTime = DateTime.Now;
                this.lblMessage.Text = string.Empty;
                if (this.files != null)
                {
                    foreach (var file in this.files)
                    {
                        file.Upload(string.Empty);
                    }
                }
                else
                {
                    this.lblMessage.Text = ApplicationResources.NoFileSelected;
                    this.prgUpload.IsIndeterminate = false;
                    this.btnUpload.Content = UploadButtonText;
                    this.btnUpload.IsEnabled = false;
                    this.btnBrowse.IsEnabled = true;
                }
            }
            else
            {
                this.userFile.CancelUpload();
                this.btnUpload.IsEnabled = false;
                this.btnBrowse.IsEnabled = true;
            }

            this.files = null;
        }
    }
}