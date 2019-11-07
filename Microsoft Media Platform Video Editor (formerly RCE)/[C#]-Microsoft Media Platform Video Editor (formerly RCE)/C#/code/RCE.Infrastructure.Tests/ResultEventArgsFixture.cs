// <copyright file="ResultEventArgsFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ResultEventArgsFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="ResultEventArgsFixture"/>.
    /// </summary>
    [TestClass]
    public class ResultEventArgsFixture
    {
        /// <summary>
        /// Tests if the passed data is returned correctly.
        /// </summary>
        [TestMethod]
        public void ShouldReturnPassedValue()
        {
            var result = new object();
            var resultEventArgs = new DataEventArgs<object>(result);

            Assert.AreEqual(result, resultEventArgs.Data);
            Assert.AreSame(result, resultEventArgs.Data);
        }
    }
}
