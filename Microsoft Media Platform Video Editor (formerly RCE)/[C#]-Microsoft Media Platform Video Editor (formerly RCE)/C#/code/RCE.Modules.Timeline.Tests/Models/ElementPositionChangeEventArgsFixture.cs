// <copyright file="ElementPositionChangeEventArgsFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ElementPositionChangeEventArgsFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Models
{
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Contracts;
    using SMPTETimecode;

    /// <summary>
    /// A class for testing the <see cref="ElementPositionChangeEventArgs"/>.
    /// </summary>
    [TestClass]
    public class ElementPositionChangeEventArgsFixture
    {
        /// <summary>
        /// Tests that the position type can be retrieved.
        /// </summary>
        [TestMethod]
        public void ShouldGetThePositionType()
        {
            var positionType = ElementPositionType.Position;

            var eventArgs = new ElementPositionChangeEventArgs { PositionType = positionType };

            Assert.AreEqual(positionType, eventArgs.PositionType);
        }

        /// <summary>
        /// Tests that the new position can be retrieved.
        /// </summary>
        [TestMethod]
        public void ShouldGetTheNewPosition()
        {
            var newPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);

            var eventArgs = new ElementPositionChangeEventArgs { NewPosition = newPosition };

            Assert.AreEqual(newPosition, eventArgs.NewPosition);
        }
    }
}
