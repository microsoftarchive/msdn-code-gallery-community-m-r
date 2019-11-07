// <copyright file="IHeaderInfoProvider.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IHeaderInfoProvider.cs                     
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
    /// Provides an easy way to recognize a class that exposes a HeaderInfo that can be used to bind to a header from XAML.
    /// </summary>
    /// <typeparam name="T">The HeaderInfo type.</typeparam>
    public interface IHeaderInfoProvider<T>
    {
        /// <summary>
        /// Gets the header info.
        /// </summary>
        /// <value>A value that represents the header info.</value>
        T HeaderInfo { get; }
    }
}