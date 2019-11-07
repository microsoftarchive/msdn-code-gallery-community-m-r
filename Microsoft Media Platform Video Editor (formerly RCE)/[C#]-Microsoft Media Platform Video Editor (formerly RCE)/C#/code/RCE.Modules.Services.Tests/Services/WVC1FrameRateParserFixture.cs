// <copyright file="WVC1FrameRateParserFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: WVC1FrameRateParserFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Services
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Modules.Services;
    using SMPTETimecode;

    [TestClass]
    public class WVC1FrameRateParserFixture
    {
        [TestMethod]
        public void ShouldGetUnknownFrameRateFor10Fps()
        {
            const string CodecPrivateData10Fps = "270000010fc28209f0778a09f81dec04fd08003a91d4bc0000010e5a67f840";

            var parser = new WVC1FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData10Fps);

            Assert.AreEqual(SmpteFrameRate.Unknown, frameRate);
        }

        [TestMethod]
        public void ShouldGetUnknownFrameRateFor15Fps()
        {
            const string CodecPrivateData15Fps = "250000010FE2FE27F15B8A27F856F1C077C80000010E5A0040";

            var parser = new WVC1FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData15Fps);

            Assert.AreEqual(SmpteFrameRate.Unknown, frameRate);
        }

        [TestMethod]
        public void ShouldGet2398FrameRate()
        {
            const string CodecPrivateData2398Fps = "250000010fdb4013f0b38a13f82ce804880000010e5a6040";

            var parser = new WVC1FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData2398Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte2398, frameRate);
        }

        [TestMethod]
        public void ShouldGet24FrameRate()
        {
            const string CodecPrivateData24Fps = "250000010FC38A09F0558A09F815680450800AADC015C00000010E5A47F840";

            var parser = new WVC1FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData24Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte24, frameRate);
        }

        [TestMethod]
        public void ShouldGet25FrameRate()
        {
            const string CodecPrivateData25Fps = "250000010fc38e09f0798a167847e80850810f41d265400000010e5207fc27c1e440";

            var parser = new WVC1FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData25Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte25, frameRate);
        }

        [TestMethod]
        public void ShouldGet2997NDFrameRate()
        {
            const string CodecPrivateData2997Fps = "250000010fcbec1a70ef8a1a783bf180c9081ac3fee9fc0000010e5a67f840";

            var parser = new WVC1FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData2997Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte2997NonDrop, frameRate);
        }
    }
}
