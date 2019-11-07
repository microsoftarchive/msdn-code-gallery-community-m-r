// <copyright file="CodecPrivateDataParser.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CodecPrivateDataParser.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.Collections.Generic;
    using RCE.Infrastructure.Services;
    using SMPTETimecode;

    /// <summary>
    /// A Codec Private Data parser.
    /// </summary>
    public class CodecPrivateDataParser : ICodecPrivateDataParser
    {
        private readonly IDictionary<string, IFrameRateParser> frameRateParsers;

        public CodecPrivateDataParser()
        {
            // TODO: Get the frame rate parsers as parameters.
            var h264FrameRateParser = new H264FrameRateParser();
            var wvc1FrameRateParser = new WVC1FrameRateParser();

            this.frameRateParsers = new Dictionary<string, IFrameRateParser>(StringComparer.OrdinalIgnoreCase);

            this.frameRateParsers.Add(h264FrameRateParser.FourCC, h264FrameRateParser);
            this.frameRateParsers.Add(wvc1FrameRateParser.FourCC, wvc1FrameRateParser);
        }

        /// <summary>
        /// Parses a codec private data to get the frame rate.
        /// </summary>
        /// <param name="fourCC">The four-character code identifying data formats.</param>
        /// <param name="codecPrivateData">The codec private data to parse.</param>
        /// <returns>The frame rate.</returns>
        public SmpteFrameRate GetFrameRate(string fourCC, string codecPrivateData)
        {
            SmpteFrameRate frameRate = SmpteFrameRate.Unknown;
            IFrameRateParser frameRatePaser = null;

            if (this.frameRateParsers.TryGetValue(fourCC, out frameRatePaser))
            {
                frameRate = frameRatePaser.GetFrameRate(codecPrivateData);
            }

            return frameRate;
        }

        /// <summary>
        /// Converts a hex string into a byte array.
        /// </summary>
        /// <param name="hex">The hex string being converted.</param>
        /// <returns>The byte array.</returns>
        internal static byte[] StringToByteArray(string hex)
        {
            int numberOfChars = hex.Length;
            byte[] bytes = new byte[numberOfChars / 2];

            for (int i = 0; i < numberOfChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        /// <summary>
        /// Gets the frame rate.
        /// </summary>
        /// <param name="frameRate">The explicit frame rate value.</param>
        /// <returns>An <see cref="SmpteFrameRate"/> enum frameRate.</returns>
        internal static SmpteFrameRate GetFrameRate(decimal frameRate)
        {
            const int DecimalsToRound = 2;

            frameRate = decimal.Round(frameRate, DecimalsToRound);

            if (frameRate == decimal.Round((24M * 1000M) / 1001M, DecimalsToRound))
            {
                return SmpteFrameRate.Smpte2398;
            }

            if (frameRate == 24)
            {
                return SmpteFrameRate.Smpte24;
            }

            if (frameRate == 25)
            {
                return SmpteFrameRate.Smpte25;
            }

            if (frameRate == decimal.Round((30M * 1000M) / 1001M, DecimalsToRound))
            {
                return SmpteFrameRate.Smpte2997NonDrop;
            }

            if (frameRate == 30)
            {
                return SmpteFrameRate.Smpte30;
            }

            return SmpteFrameRate.Unknown;
        }
    }
}
