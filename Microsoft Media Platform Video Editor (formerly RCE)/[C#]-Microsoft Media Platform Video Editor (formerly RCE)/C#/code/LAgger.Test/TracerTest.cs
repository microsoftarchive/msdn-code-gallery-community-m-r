// <copyright file="TracerTest.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TracerTest.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace LAgger.Test
{
    using System;
    using System.Threading;
    using LAgger;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    /// <summary>
    /// A test class for ensuring that <see cref="Tracer"/> works correctly.
    /// </summary>
    [TestClass]
    public class TracerTest
    {
        /// <summary>
        /// Tests if the tracer can be initialized.
        /// </summary>
        [TestMethod]
        public void Tracer_Constructor()
        {
            Guid activityId = Guid.NewGuid();
            string operation = "tracer test";
            MockLoggerManager manager = new MockLoggerManager();
            LoggerManager.Start(manager, new Uri("http://test"));
            using (var tracer = new Tracer(operation, activityId))
            {
                Thread.Sleep(10);
            }
            
            Assert.IsNotNull(manager);
            Assert.IsNotNull(manager.Entry);
            Assert.IsNotNull(manager.Entry.Id);

            TraceEntry entry = manager.Entry as TraceEntry;
            Assert.IsNotNull(entry, "Entry is not of type TraceEntry");
            Assert.AreEqual(activityId, entry.ActivityId);
            Assert.AreEqual(operation, entry.Operation);
            Assert.AreEqual("Tracer_Constructor", entry.Method);
            Assert.IsTrue(entry.Duration > TimeSpan.Zero);
        }
    }
}
