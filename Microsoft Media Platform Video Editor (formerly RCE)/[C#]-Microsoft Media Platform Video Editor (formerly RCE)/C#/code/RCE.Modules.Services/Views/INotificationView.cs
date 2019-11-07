// <copyright file="INotificationView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: INotificationView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Views
{
    /// <summary>
    /// Defines the common operations for a notification view.
    /// </summary>
    public interface INotificationView
    {
        /// <summary>
        /// Shows a progress bar.
        /// </summary>
        void ShowProgressBar();

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        void HideProgressBar();
    }
}