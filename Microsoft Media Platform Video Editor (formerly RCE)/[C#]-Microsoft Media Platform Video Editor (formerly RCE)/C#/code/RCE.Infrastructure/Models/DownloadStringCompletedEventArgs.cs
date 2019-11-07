// <copyright file="DownloadStringCompletedEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DownloadStringCompletedEventArgs.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Event args for DownloadStringCompleted event.
    /// </summary>
    public class DownloadStringCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadStringCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="error">The error.</param>
        /// <param name="canceled">If set to <c>true</c> [canceled].</param>
        /// <param name="userState">State of the user.</param>
        public DownloadStringCompletedEventArgs(string result, Exception error, bool canceled, object userState)
            : base(error, canceled, userState)
        {
            this.Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadStringCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="args">The <see cref="System.Net.DownloadStringCompletedEventArgs"/> instance containing the event data.</param>
        public DownloadStringCompletedEventArgs(System.Net.DownloadStringCompletedEventArgs args) : base(args.Error, args.Cancelled, args.UserState)
        {
            this.Result = args.Result;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public string Result { get; private set; }
    }
}