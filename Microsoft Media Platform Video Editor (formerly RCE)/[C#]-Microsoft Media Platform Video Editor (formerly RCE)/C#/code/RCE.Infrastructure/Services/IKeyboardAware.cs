// <copyright file="IKeyboardAware.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IKeyboardAware.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;

    using Microsoft.Practices.Composite.Presentation.Commands;

    /// <summary>
    /// Interface to manage keyboard events.
    /// </summary>
    public interface IKeyboardAware
    {
        /// <summary>
        /// Gets KeyboardActionCommand.
        /// </summary>
        /// <value>
        /// The keyboard action command.
        /// </value>
        DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; }

        KeyboardActionContext ActionContext { get; }
    }
}