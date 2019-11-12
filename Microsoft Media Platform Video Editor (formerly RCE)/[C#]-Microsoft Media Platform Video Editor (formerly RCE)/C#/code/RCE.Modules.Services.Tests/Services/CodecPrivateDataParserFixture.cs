// <copyright file="CodecPrivateDataParserFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CodecPrivateDataParserFixture.cs                     
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
    public class CodecPrivateDataParserFixture
    {
        private const string WVC1FourCC = "WVC1";
        private const string H264FourCC = "H264";

        [TestMethod]
        public void ShouldGetUnknownFrameRateForUnknownFourCC()
        {
            const string CodecPrivateData2398Fps = "250000010fdb4013f0b38a13f82ce804880000010e5a6040";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate("UNKW", CodecPrivateData2398Fps);

            Assert.AreEqual(SmpteFrameRate.Unknown, frameRate);
        }

        [TestMethod]
        public void ShouldGetUnknownFrameRateFor10FpsFromWVC1CodecPrivateData()
        {
            const string CodecPrivateData10Fps = "270000010fc28209f0778a09f81dec04fd08003a91d4bc0000010e5a67f840";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(WVC1FourCC, CodecPrivateData10Fps);

            Assert.AreEqual(SmpteFrameRate.Unknown, frameRate);
        }

        [TestMethod]
        public void ShouldGetUnknownFrameRateFor15FpsFromWVC1CodecPrivateData()
        {
            const string CodecPrivateData15Fps = "250000010FE2FE27F15B8A27F856F1C077C80000010E5A0040";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(WVC1FourCC, CodecPrivateData15Fps);

            Assert.AreEqual(SmpteFrameRate.Unknown, frameRate);
        }

        [TestMethod]
        public void ShouldGet2398FrameRateFromWVC1CodecPrivateData()
        {
            const string CodecPrivateData2398Fps = "250000010fdb4013f0b38a13f82ce804880000010e5a6040";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(WVC1FourCC, CodecPrivateData2398Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte2398, frameRate);
        }

        [TestMethod]
        public void ShouldGet24FrameRateFromWVC1CodecPrivateData()
        {
            const string CodecPrivateData24Fps = "250000010FC38A09F0558A09F815680450800AADC015C00000010E5A47F840";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(WVC1FourCC, CodecPrivateData24Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte24, frameRate);
        }

        [TestMethod]
        public void ShouldGet25FrameRateFromWVC1CodecPrivateData()
        {
            const string CodecPrivateData25Fps = "250000010fc38e09f0798a167847e80850810f41d265400000010e5207fc27c1e440";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(WVC1FourCC, CodecPrivateData25Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte25, frameRate);
        }

        [TestMethod]
        public void ShouldGet2997NDFrameRateFromWVC1CodecPrivateData()
        {
            const string CodecPrivateData2997Fps = "250000010fcbec1a70ef8a1a783bf180c9081ac3fee9fc0000010e5a67f840";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(WVC1FourCC, CodecPrivateData2997Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte2997NonDrop, frameRate);
        }

        [TestMethod]
        public void ShouldGetUnknownFrameRateFor2339FpsFromH264CodecPrivateData()
        {
            const string CodecPrivateData2339Fps = "0000000167640032AC2CA501E0089F97FF0400040052020202800233938066FF3031500016E360002DC6FF8C718A8000B71B00016E37FC6387684894580000000168E9093525";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(H264FourCC, CodecPrivateData2339Fps);

            Assert.AreEqual(SmpteFrameRate.Unknown, frameRate);
        }

        [TestMethod]
        public void ShouldGet25FrameRateFromH264CodecPrivateData()
        {
            const string CodecPrivateData25Fps = "00000001274D401EB90A05819D80A5010101F00000030010000003032E0400055730000802CEF7B80F844228B00000000128E93BC8";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(H264FourCC, CodecPrivateData25Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte25, frameRate);
        }

        [TestMethod]
        public void ShouldGet24FrameRateFromH264CodecPrivateData()
        {
            const string CodecPrivateData24Fps = "00000001674D4020965280A00B77FE08000800A10000030001000003003060400033E14000081B33FC638C0800067C28000103667F8C70ED0B168B0000000168E9093520";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(H264FourCC, CodecPrivateData24Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte24, frameRate);
        }

        [TestMethod]
        public void ShouldGet2997FrameRateFromH264CodecPrivateData()
        {
            const string CodecPrivateData2997Fps = "00000001674D400D96560A8CDFF820001F8284001B7E44066FF30180000927000124F9FC638C00004938000927CFE31C3B4244A70000000168EA5352";

            var parser = new CodecPrivateDataParser();

            var frameRate = parser.GetFrameRate(H264FourCC, CodecPrivateData2997Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte2997NonDrop, frameRate);
        }
    }
}
