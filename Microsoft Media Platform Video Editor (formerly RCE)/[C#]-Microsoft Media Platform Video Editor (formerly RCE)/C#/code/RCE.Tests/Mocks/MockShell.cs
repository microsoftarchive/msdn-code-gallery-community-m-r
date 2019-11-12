// <copyright file="MockShell.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockShell.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Tests.Mocks
{
    using System;
    using RCE.Infrastructure.Models;

    public class MockShell : IShell
    {
        /// <summary>
        /// Event to save project.
        /// </summary>
        public event EventHandler SaveProject;

        public event EventHandler<KeyMappingActionEventArgs> KeyMappingActionEvent;

        public ShellPresenter Model { get; set; }
        
        public bool ToggleFullScreenCalled { get; set; }

        public void ToggleFullScreen(FullScreenMode mode)
        {
            this.ToggleFullScreenCalled = true;
        }

        public void InvokeKeyMappingActionEvent(KeyMappingAction action)
        {
            if (this.KeyMappingActionEvent != null)
            {
                this.KeyMappingActionEvent(this, new KeyMappingActionEventArgs(action));
            }
        }

        public void InvokeSaveProjectEvent()
        {
            EventHandler saveProjectHandler = this.SaveProject;
            if (saveProjectHandler != null)
            {
                this.SaveProject(null, EventArgs.Empty);
            }
        }
    }
}