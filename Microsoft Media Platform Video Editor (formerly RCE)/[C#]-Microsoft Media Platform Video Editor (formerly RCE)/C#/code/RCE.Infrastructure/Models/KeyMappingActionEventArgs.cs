// <copyright file="KeyMappingActionEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: KeyMappingActionEventArgs.cs                     
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

    /// <summary>
    /// Event argument used for sending the KeyMappingAction.
    /// </summary>
    public class KeyMappingActionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyMappingActionEventArgs"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public KeyMappingActionEventArgs(KeyMappingAction action)
        {
            this.KayMappingAction = action;
        }

        /// <summary>
        /// Gets the kay mapping action.
        /// </summary>
        /// <value>The kay mapping action.</value>
        public KeyMappingAction KayMappingAction { get; private set; }
    }
}
