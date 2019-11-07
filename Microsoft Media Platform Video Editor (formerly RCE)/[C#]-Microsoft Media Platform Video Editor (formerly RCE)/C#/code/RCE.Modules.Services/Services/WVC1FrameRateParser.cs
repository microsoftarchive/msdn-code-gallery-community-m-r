// <copyright file="WVC1FrameRateParser.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: WVC1CodecPrivateDataParser.cs                     
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
    using SMPTETimecode;

    public class WVC1FrameRateParser : IFrameRateParser
    {
        internal const string WVC1FourCC = "WVC1";

        public string FourCC
        {
            get { return WVC1FourCC; }
        }

        public SmpteFrameRate GetFrameRate(string codecPrivateData)
        {
            IDictionary<uint, int> numeratorMappings = new Dictionary<uint, int>
                                                                   {
                                                                       { 1, 24 * 1000 },
                                                                       { 2, 25 * 1000 },
                                                                       { 3, 30 * 1000 },
                                                                       { 4, 50 * 1000 },
                                                                       { 5, 60 * 1000 },
                                                                       { 6, 48 * 1000 },
                                                                       { 7, 72 * 1000 }
                                                                   };

            IDictionary<uint, int> denominatorMappings = new Dictionary<uint, int>
                                                                     {
                                                                         { 1, 1000 },
                                                                         { 2, 1001 }
                                                                     };
            const int SkipBitsInAllCases = 5 * 8;

            byte[] bytes = CodecPrivateDataParser.StringToByteArray(codecPrivateData);

            int frameRateStartsAt = SkipBitsInAllCases;

            if (GetBitValue(bytes, SkipBitsInAllCases + 46))
            {
                if (GetBitValue(bytes, SkipBitsInAllCases + 75))
                {
                    if (GetBitValue(bytes, SkipBitsInAllCases + 79)
                        && GetBitValue(bytes, SkipBitsInAllCases + 80)
                        && GetBitValue(bytes, SkipBitsInAllCases + 81)
                        && GetBitValue(bytes, SkipBitsInAllCases + 82))
                    {
                        frameRateStartsAt += 96;
                    }
                    else
                    {
                        frameRateStartsAt += 80;
                    }
                }
                else
                {
                    frameRateStartsAt += 76;
                }
            }
            else
            {
                return SmpteFrameRate.Unknown;
            }

            bool framerateFlag = GetBitValue(bytes, frameRateStartsAt);

            if (framerateFlag)
            {
                frameRateStartsAt++;

                bool framerateInd = GetBitValue(bytes, frameRateStartsAt);

                if (framerateInd)
                {
                    uint frameRateValue = GetBits(bytes, frameRateStartsAt, 16);

                    decimal value = (frameRateValue * 0.03125M) + 0.03125M;

                    return CodecPrivateDataParser.GetFrameRate(value);
                }
                else
                {
                    uint frameRateNumerator = GetBits(bytes, frameRateStartsAt, 8);

                    frameRateStartsAt += 8;

                    uint frameRateDenominator = GetBits(bytes, frameRateStartsAt, 4);

                    if (numeratorMappings.ContainsKey(frameRateNumerator) && denominatorMappings.ContainsKey(frameRateDenominator))
                    {
                        decimal numerator = numeratorMappings[frameRateNumerator];
                        decimal denominator = denominatorMappings[frameRateDenominator];

                        decimal value = numerator / denominator;

                        return CodecPrivateDataParser.GetFrameRate(value);
                    }
                    else
                    {
                        return SmpteFrameRate.Unknown;
                    }
                }
            }

            return SmpteFrameRate.Unknown;
        }
        
        /// <summary>
        /// Gets the bit value of an specific position.
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <param name="p">The position to get the bit value.</param>
        /// <returns>The bit value.</returns>
        private static bool GetBitValue(byte[] bytes, int p)
        {
            int bytePos = p / 8;
            int bitPos = p % 8;

            return (bytes[bytePos] & (byte)(1 << (7 - bitPos))) != 0;
        }

        /// <summary>
        /// Gets the bits value of an specific range.
        /// </summary>
        /// <param name="bytes">The bytes array.</param>
        /// <param name="offset">The start position.</param>
        /// <param name="count">The number of bytes to parse.</param>
        /// <returns>The bits value.</returns>
        private static uint GetBits(byte[] bytes, int offset, int count)
        {
            uint value = 0;
            for (int i = 1; i <= count; i++)
            {
                if (GetBitValue(bytes, offset + i))
                {
                    value += (uint)Math.Pow(2, count - i);
                }
            }

            return value;
        }
    }
}
