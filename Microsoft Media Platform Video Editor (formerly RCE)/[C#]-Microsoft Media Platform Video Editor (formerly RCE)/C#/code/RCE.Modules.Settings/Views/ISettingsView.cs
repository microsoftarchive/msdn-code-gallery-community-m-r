// <copyright file="ISettingsView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ISettingsView.cs                     
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
    using Microsoft.Practices.Composite;

    /// <summary>
    /// Interface that defines a settings view.
    /// </summary>
    public interface ISettingsView : IActiveAware
    {
        /// <summary>
        /// Gets or sets the <see cref="ISettingsViewPresentationModel"/> presentation model of the view.
        /// </summary>
        /// <value>A <see also="ISettingsViewPresentatinModel"/> that represents the presentation model of the view.</value>
        ISettingsViewPresentationModel Model { get; set; }
    }
}