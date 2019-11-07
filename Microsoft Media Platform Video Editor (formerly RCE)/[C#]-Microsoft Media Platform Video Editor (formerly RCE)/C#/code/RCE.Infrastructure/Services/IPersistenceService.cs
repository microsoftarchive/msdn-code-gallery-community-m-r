// <copyright file="IPersistenceService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IPersistenceService.cs                     
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
    using System.Collections.Generic;
    using System.IO;

    public interface IPersistenceService
    {
        event EventHandler StorageMaxSpaceReached;

        event EventHandler StorageRemoved;

        long UsedSize { get; }

        long AvailableFreeSpace { get; }

        long Quota { get; }

        Dictionary<string, object> GetApplicationSettings();

        void AddApplicationSettings(string key, object value);

        Stream Retrieve(string key);

        bool Persist(string key, Stream stream);

        bool IncreaseQuota(long bytesToIncrease);

        void RemoveStorage();

        void RemoveItem(string key);
    }
}
