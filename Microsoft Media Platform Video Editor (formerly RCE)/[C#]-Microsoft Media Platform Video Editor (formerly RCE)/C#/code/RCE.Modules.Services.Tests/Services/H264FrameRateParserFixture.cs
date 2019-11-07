// <copyright file="H264FrameRateParserFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: H264FrameRateParserFixture.cs                     
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
    public class H264FrameRateParserFixture
    {
        [TestMethod]
        public void ShouldGetUnknownFrameRateFor2339Fps()
        {
            const string CodecPrivateData2339Fps = "0000000167640032AC2CA501E0089F97FF0400040052020202800233938066FF3031500016E360002DC6FF8C718A8000B71B00016E37FC6387684894580000000168E9093525";

            var parser = new H264FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData2339Fps);

            Assert.AreEqual(SmpteFrameRate.Unknown, frameRate);
        }

        [TestMethod]
        public void ShouldGet25FrameRate()
        {
            const string CodecPrivateData25Fps = "00000001274D401EB90A05819D80A5010101F00000030010000003032E0400055730000802CEF7B80F844228B00000000128E93BC8";

            var parser = new H264FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData25Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte25, frameRate);
        }

        [TestMethod]
        public void ShouldGet24FrameRate()
        {
            const string CodecPrivateData24Fps = "00000001674D4020965280A00B77FE08000800A10000030001000003003060400033E14000081B33FC638C0800067C28000103667F8C70ED0B168B0000000168E9093520";

            var parser = new H264FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData24Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte24, frameRate);
        }

        [TestMethod]
        public void ShouldGet2997FrameRate()
        {
            const string CodecPrivateData2997Fps = "00000001674D400D96560A8CDFF820001F8284001B7E44066FF30180000927000124F9FC638C00004938000927CFE31C3B4244A70000000168EA5352";

            var parser = new H264FrameRateParser();

            var frameRate = parser.GetFrameRate(CodecPrivateData2997Fps);

            Assert.AreEqual(SmpteFrameRate.Smpte2997NonDrop, frameRate);
        }
    }
}
