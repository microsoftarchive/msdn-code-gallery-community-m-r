// <copyright file="ProjectState.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectState.cs                     
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
    /// Specifies the status of the project.
    /// </summary>
    public enum ProjectState
    {
        /// <summary>
        /// If project is being retrieving from the server.
        /// </summary>
        Retrieving,

        /// <summary>
        /// If the project have been retrieved from the server.
        /// </summary>
        Retrieved,

        /// <summary>
        /// If the project failed to be retrieved from the server.
        /// </summary>
        Error
    }
}