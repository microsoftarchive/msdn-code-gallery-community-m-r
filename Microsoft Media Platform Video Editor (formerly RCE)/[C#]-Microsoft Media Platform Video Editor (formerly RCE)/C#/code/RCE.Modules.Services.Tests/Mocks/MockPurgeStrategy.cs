// <copyright file="MockPurgeStrategy.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockPurgeStrategy.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Mocks
{
    using System;
    using System.Collections.Generic;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Services;

    public class MockPurgeStrategy : IPurgeStrategy
    {
        public event EventHandler<DataEventArgs<IEnumerable<string>>> ItemsPurged;

        public void InvokeItemsPurged(IEnumerable<string> e)
        {
            EventHandler<DataEventArgs<IEnumerable<string>>> handler = this.ItemsPurged;
            if (handler != null)
            {
                handler(this, new DataEventArgs<IEnumerable<string>>(e));
            }
        }

        public void Purge(ICache cache)
        {
            throw new NotImplementedException();
        }
    }
}