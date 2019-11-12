// <copyright file="ISecurityTokenService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ISecurityTokenService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Security
{
    using System;

    /// <summary>
    /// Defines a generic security token service.
    /// </summary>
    public interface ISecurityTokenService
    {
        /// <summary>
        /// Occurrs when a token has been issued.
        /// </summary>
        event EventHandler TokenIssued;

        /// <summary>
        /// Start requesting tokens.
        /// </summary>
        void Start();
    }
}