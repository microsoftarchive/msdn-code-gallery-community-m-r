//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="UserFile.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
namespace SLBlobUploader.Code.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Browser;
    using System.Windows.Threading;
    using SLBlobUploader.Code.Abstract;
    using SLBlobUploader.Code.StorageClient;

    /// <summary>
    /// Encapsulates file to be transmitted and its attributes.
    /// </summary>
    public class UserFile : INotifyPropertyChanged
    {
        /// <summary>
        /// Name of file to upload
        /// </summary>
        private string fileName;

        /// <summary>
        /// The stream associated with this file.
        /// </summary>
        private Stream fileStream;

        /// <summary>
        /// Contains members that file uploader clients should implement
        /// </summary>
        private IFileUploader fileUploader;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when file upload has been completed.
        /// </summary>
        public event EventHandler<UploadCompletedEventArgs> UploadCompletedEvent;

        /// <summary>
        /// Gets or sets the UI dispatcher.
        /// </summary>
        /// <value>The UI dispatcher.</value>
        public Dispatcher UIDispatcher
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the upload container URL.
        /// </summary>
        /// <value>The upload container URL.</value>
        public Uri UploadContainerUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        [ScriptableMember()]
        public string FileName
        {
            get
            {
                return this.fileName;
            }

            set
            {
                this.fileName = value;
                this.NotifyPropertyChanged("FileName");
            }
        }

        /// <summary>
        /// Gets or sets the file stream.
        /// </summary>
        /// <value>The file stream.</value>
        public Stream FileStream
        {
            get
            {
                return this.fileStream;
            }

            set
            {
                this.fileStream = value;
            }
        }

        /// <summary>
        /// Uploads the specified init parameters.
        /// </summary>
        /// <param name="initParameters">The init parameters.</param>
        public void Upload(string initParameters)
        {
            ////Accepts single file now.
            List<UserFile> filesToUpload = new List<UserFile>();
            filesToUpload.Add(this);
            this.fileUploader = new CloudBlobClient(this.UploadContainerUrl, filesToUpload.FirstOrDefault());
            this.fileUploader.StartUpload(initParameters);
            this.fileUploader.UploadFinished += new EventHandler<UploadCompletedEventArgs>(this.OnUploadFinished);
        }

        /// <summary>
        /// Cancels the upload.
        /// </summary>
        public void CancelUpload()
        {
            if (this.fileUploader != null)
            {
                this.fileUploader.CancelUpload();
            }
        }

        /// <summary>
        /// Handles the UploadFinished event of the fileUploader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnUploadFinished(object sender, UploadCompletedEventArgs e)
        {
            this.fileUploader = null;
            EventHandler<UploadCompletedEventArgs> uiHandler = this.UploadCompletedEvent;
            if (uiHandler != null)
            {
                uiHandler(this, e);
            }
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="prop">The property which has been modified.</param>
        private void NotifyPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
