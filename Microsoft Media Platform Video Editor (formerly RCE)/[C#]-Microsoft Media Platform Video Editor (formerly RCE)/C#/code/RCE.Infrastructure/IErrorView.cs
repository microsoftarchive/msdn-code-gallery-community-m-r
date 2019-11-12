// <copyright file="IErrorView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IErrorView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure
{
    /// <summary>
    /// Defines an interface for an error view.
    /// </summary>
    public interface IErrorView
    {
        /// <summary>
        /// Gets or sets the error message to show.
        /// </summary>
        /// <value>The error message to show.</value>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Shows the view.
        /// </summary>
        void Show();
    }
}