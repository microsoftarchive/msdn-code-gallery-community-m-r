// <copyright file="MockPersistenceService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockPersistenceService.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using RCE.Infrastructure.Services;

    public class MockPersistenceService : IPersistenceService
    {
        public event EventHandler StorageMaxSpaceReached;

        public event EventHandler StorageRemoved;

        public bool IncreaseQuotaCalled { get; set; }

        public bool RemoveStorageCalled { get; set; }

        public long UsedSize { get; set; }

        public long AvailableFreeSpace { get; set; }

        public long Quota { get; set; }

        public long QuotaToIncrease { get; set; }

        public Dictionary<string, object> GetApplicationSettings()
        {
            Dictionary<string, string> query = new Dictionary<string, string> { { "key", "value" } };
            return new Dictionary<string, object> { { "rceSettings", "Settings" }, { "rceQueryString", query } };
        }

        public void AddApplicationSettings(string key, object value)
        {
        }

        public Stream Retrieve(string key)
        {
            throw new NotImplementedException();
        }

        public bool Persist(string key, Stream stream)
        {
            throw new NotImplementedException();
        }

        public bool IncreaseQuota(long bytesToIncrease)
        {
            this.IncreaseQuotaCalled = true;
            this.QuotaToIncrease = bytesToIncrease;
            return true;
        }

        public void RemoveStorage()
        {
            this.RemoveStorageCalled = true;
        }

        public void RemoveItem(string key)
        {
            throw new NotImplementedException();
        }
    }
}