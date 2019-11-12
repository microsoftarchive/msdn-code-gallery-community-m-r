// <copyright file="ISearchServiceBridge.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ISearchServiceBridge.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Search.Services
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;

    public interface ISearchServiceBridge
    {
        event EventHandler<DataEventArgs<List<Asset>>> ResultsAvailable;

        void OpenPopup();

        void SetSearchResults(string results);
    }
}