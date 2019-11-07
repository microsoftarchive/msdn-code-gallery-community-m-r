// <copyright file="IShell.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IShell.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE
{
    using System;
    using RCE.Infrastructure.Models;

    /// <summary>
    /// Interface that defines a shell view.
    /// </summary>
    public interface IShell
    {
        /// <summary>
        /// Ocurrs when a keyboard command is executed.
        /// </summary>
        event EventHandler<KeyMappingActionEventArgs> KeyMappingActionEvent;

        /// <summary>
        /// Event to save project.
        /// </summary>
        event EventHandler SaveProject;

        /// <summary>
        /// Gets or sets the Model associated to the view.
        /// </summary>
        /// <value>The model associated to the view.</value>
        ShellPresenter Model { get; set; }

        /// <summary>
        /// Toggles the full screen selection of the application or the player depending on the <see cref="FullScreenMode"/> mode.
        /// </summary>
        /// <param name="mode">The mode to determine if the toggling of the fullscreen should be done in the application or in the player.</param>
        void ToggleFullScreen(FullScreenMode mode);
    }
}