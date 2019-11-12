// <copyright file="MockLogManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockLogManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace LAgger.Test.Mocks
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class MockLogManager : LoggerManager
    {
        public MockLogManager()
        {
            this.ExecutePublishFilter = true;
        }

        public bool FilterCalled { get; set; }

        public bool ExecutePublishFilter { get; set; }

        public void ExecuteFilter()
        {
            this.FilterEntries();
        }

        public void PublishFilteredEntries()
        {
            this.LoadConfiguration(new Uri("http://test"));
            this.PublishEntries();
        }

        protected override void LoadConfiguration(Uri uri)
        {
            this.Configuration = new LoggingConfiguration { BatchSize = 2, LogLevel = 2, TracingEnabled = true };
        }

        protected override void FilterEntries()
        {
            this.FilterCalled = true;
            base.FilterEntries();
        }

        protected override void PublishEntries()
        {
            if (this.ExecutePublishFilter)
            {
                base.PublishEntries();
            }
        }

        protected override void SendEntries(System.Collections.ObjectModel.ObservableCollection<Entry> entries)
        {
            Assert.IsTrue(this.Configuration.BatchSize >= entries.Count, "Batch size should be greater than or equal to entries");
        }
    }
}
