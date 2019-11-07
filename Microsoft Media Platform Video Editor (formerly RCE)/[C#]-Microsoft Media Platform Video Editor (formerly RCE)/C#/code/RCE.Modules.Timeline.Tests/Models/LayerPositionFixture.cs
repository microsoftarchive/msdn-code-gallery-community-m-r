// <copyright file="LayerPositionFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LayerPositionFixture.cs                     
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Contracts;
    using SMPTETimecode;
    using TrackType = RCE.Infrastructure.Models.TrackType;

    /// <summary>
    /// A class for testing the <see cref="LayerPosition"/>.
    /// </summary>
    [TestClass]
    public class LayerPositionFixture
    {
        /// <summary>
        /// Tests that the layer type can be retrieved.
        /// </summary>
        [TestMethod]
        public void ShouldGetTheLayerType()
        {
            var trackType = TrackType.Visual;

            var layerPosition = new LayerPosition { LayerType = trackType };

            Assert.AreEqual(trackType, layerPosition.LayerType);
        }

        /// <summary>
        /// Tests that the position can be retrieved.
        /// </summary>
        [TestMethod]
        public void ShouldGetTheNewPosition()
        {
            var position = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);

            var layerPosition = new LayerPosition { Position = position };

            Assert.AreEqual(position, layerPosition.Position);
        }
    }
}
