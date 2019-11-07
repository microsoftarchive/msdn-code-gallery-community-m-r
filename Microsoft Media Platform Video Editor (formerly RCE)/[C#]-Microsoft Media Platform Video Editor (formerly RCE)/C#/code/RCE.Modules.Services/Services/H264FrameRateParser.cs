// <copyright file="H264FrameRateParser.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: H264FrameRateParser.cs                     
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
    using System.Diagnostics;
    using SMPTETimecode;

    public class H264FrameRateParser : IFrameRateParser
    {
        internal const string H264FourCC = "H264";

        public string FourCC
        {
            get { return H264FourCC; }
        }

        public SmpteFrameRate GetFrameRate(string codecPrivateData)
        {
            var frameRate = SmpteFrameRate.Unknown;
            
            var bytes = CodecPrivateDataParser.StringToByteArray(codecPrivateData);
            var h264CodecPrivateDataParser = new H264CodecPrivateDataParser();
            
            h264CodecPrivateDataParser.Parse(bytes);
            var parameterSet = h264CodecPrivateDataParser.ParsedSPS;
            
            if (parameterSet.Vui_parameters_present_flag && parameterSet.Vui_parameters != null && parameterSet.Vui_parameters.Timing_info_present_flag)
            {
                var timeScale = Convert.ToDecimal(parameterSet.Vui_parameters.Time_scale);
                var numUnitsInTick = Convert.ToDecimal(parameterSet.Vui_parameters.Num_units_in_tick);
                var frameRateValue = timeScale / (2 * numUnitsInTick);

                frameRate = CodecPrivateDataParser.GetFrameRate(frameRateValue);
            }

            return frameRate;
        }

        /// <summary>
        /// This class is used to read from a bitstream
        /// </summary>
        internal class BitReader
        {
            private readonly byte[] RGC_8BIT_NUM_LEAD0_TABLE =
            {
              0x00, 0x07, 0x06, 0x06, 0x05, 0x05, 0x05, 0x05, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03, 0x03
            , 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02, 0x02
            , 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01
            , 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01
            , 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            , 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            , 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            , 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            private const int MAX_EONALU_RSIZE = 16;
            private byte[] _buffer;
            private uint _bitMask;
            private int _bitsLeft;
            private int _currentByteIndex;
            private int _lastByteIndex;
            private int _ei_flag;
            private uint _total;

            public bool AvailableData
            {
                get
                {
                    return (_currentByteIndex <= _lastByteIndex) ? true : false;
                }
            }

            public uint AvailableBytes
            {
                get
                {
                    return (uint)_lastByteIndex - (uint)_currentByteIndex;
                }
            }

            /// <summary>
            /// Constructor
            /// </summary>
            public BitReader()
            {
            }

            /// <summary>
            /// Flushes a specified number of bits from the bit stream
            /// </summary>
            /// <param name="numBits">Number of bits to be flushed</param>
            private void Flush(uint numBits)
            {
                _bitMask <<= (int)numBits;

                // Adjust counter of avaiable bits
                if ((_bitsLeft -= (int)numBits) < 0)
                {
                    Load();
                }
                _total += numBits;
            }

            /// <summary>
            /// peeks less than 16 bit from the bitstream
            /// </summary>
            /// <param name="numBits"></param>
            /// <returns></returns>
            private uint Peek16(uint numBits)
            {
                // Return numBits most significant bits
                return (uint)(_bitMask >> (((int)sizeof(uint) * 8) - (int)numBits));
            }

            /// <summary>
            /// reads one exp-golomb VLC symbol 
            /// </summary>
            /// <param name="len">The length of the symbol</param>
            /// <returns></returns>
            private uint GetVLSSymbol(out int len)
            {
                int numLeadingZeroBits = 0;
                int bitPattern;
                int numZero2;

                while (0 == (bitPattern = (int)Peek16(8)) && 0 == _ei_flag)
                {
                    numLeadingZeroBits += 8;
                    Flush(8);
                }

                numZero2 = RGC_8BIT_NUM_LEAD0_TABLE[bitPattern];
                Flush((uint)(numZero2 + 1));

                len = ((numLeadingZeroBits + numZero2) << 1) + 1;
                //Tracing.Trace(TraceArea.Manifest, TraceLevel.Verbose, "len {0}", len);

                return GetBits((uint)(numLeadingZeroBits + numZero2));
            }

            /// <summary>
            /// reads the unsigned exp golomb symbol value
            /// </summary>
            /// <returns></returns>
            public uint GetUEValue()
            {
                int len;
                uint vlc = GetVLSSymbol(out len);

                return ((uint)(1 << (len >> 1)) + (uint)(vlc) - 1);
            }

            /// <summary>
            /// reads the signed exp golomb symbol value
            /// </summary>
            /// <returns></returns>
            public int GetSEValue()
            {
                int len;
                int val;
                uint vlc = GetVLSSymbol(out len);
                uint n;

                n = (uint)(1 << (len >> 1)) + (uint)vlc - 1;
                val = (int)((n + 1) / 2);
                if (0 == (n & 0x01))                           // lsb is signed bit
                {
                    val = -val;
                }

                return val;
            }

            /// <summary>
            /// gets a number of bits(<32) from the bit stream
            /// </summary>
            /// <param name="numBits">Number of bits to get from the bit stream</param>
            /// <returns></returns>
            public uint GetBits(uint numBits)
            {
                uint mask = 0;
                uint bitsLeft = (uint)(_bitsLeft + 16);

                Debug.Assert(numBits <= 32);

                if (0 == numBits)
                {
                    //
                    // special case -- cannot shift 32-bit value right by 32 bits
                    // because result is hardware-dependent
                    //
                    return 0;
                }

                if (numBits > bitsLeft && bitsLeft > 0)
                {
                    do
                    {
                        numBits -= bitsLeft;
                        mask += Peek16(bitsLeft) << (int)numBits;
                        Flush(bitsLeft);
                        bitsLeft = (uint)(_bitsLeft + 16);
                    }
                    while (numBits > bitsLeft && bitsLeft > 0);

                    mask += Peek16(numBits);
                    Flush(numBits);
                }
                else
                {
                    mask = Peek16(numBits);
                    Flush(numBits);
                }

                //Tracing.Trace(TraceArea.Manifest, TraceLevel.Verbose, "{0} bits, len {1}", _total, numBits);

                return mask;
            }

            /// <summary>
            /// Get boolean from 1 bit
            /// </summary>
            /// <returns></returns>
            public bool GetBool()
            {
                return (0 == GetBits(1)) ? false : true;
            }

            /// <summary>
            /// Convert from big-endian to host endian.
            /// </summary>
            /// <param name="ushrtBigEndian">The value which is known to be big-endian (but is currently in host endian).</param>
            /// <returns>The corrected value of ushrtBigEndian.</returns>
            private ushort BigEndianToHost(ushort ushrtBigEndian)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return (ushort)((((uint)ushrtBigEndian & 0xFF) << 8) | (uint)((ushrtBigEndian >> 8) & 0xFF));
                }
                else
                {
                    return ushrtBigEndian; // No conversion necessary
                }
            }

            /// <summary>
            /// Convert from big-endian to host endian.
            /// </summary>
            /// <param name="uintBigEndian">The value which is known to be big-endian (but is currently in host endian).</param>
            /// <returns>The corrected value of uintBigEndian.</returns>
            private uint BigEndianToHost(uint uintBigEndian)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return (((uint)BigEndianToHost((ushort)uintBigEndian) & 0xFFFF) << 16)
                            | ((uint)BigEndianToHost((ushort)(uintBigEndian >> 16)) & 0xFFFF);
                }
                else
                {
                    return uintBigEndian; // No conversion necessary
                }
            }

            /// <summary>
            /// Convert from big-endian to host endian.
            /// </summary>
            /// <param name="ulongBigEndian">The value which is known to be big-endian (but is currently in host endian).</param>
            /// <returns>The corrected value of ulongBigEndian.</returns>
            private ulong BigEndianToHost(ulong ulongBigEndian)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return (((ulong)BigEndianToHost((uint)ulongBigEndian) & 0xFFFFFFFF) << 32)
                            | ((ulong)BigEndianToHost((uint)(ulongBigEndian >> 32)) & 0xFFFFFFFF);
                }
                else
                {
                    return ulongBigEndian; // No conversion necessary
                }
            }

            /// <summary>
            /// Load more bits from the bit stream
            /// </summary>
            private void Load()
            {
                //
                // Lookup mask has less than 16 bits left -- time to reload
                //
                int byteIndex = _currentByteIndex;

                byte[] bytesToLoad = new byte[4];
                for (int i = 0; i < 4 && i <= _lastByteIndex - byteIndex; ++i)
                {
                    bytesToLoad[i] = _buffer[byteIndex + i];
                }

                if (byteIndex < _lastByteIndex)
                {
                    int bitsLeft = _bitsLeft + 16;
                    uint loadedBits;

                    loadedBits = (uint)(BigEndianToHost(BitConverter.ToUInt32(bytesToLoad, 0)) & 0xffff0000);

                    _bitMask = _bitMask | (loadedBits >> bitsLeft);
                    byteIndex += 2;
                    _bitsLeft = bitsLeft;
                }
                else if (byteIndex <= _lastByteIndex)
                {
                    // Shift amount is X:
                    // |--(bitsLeft+16)--|------X-----|----8 bits loaded---|
                    // ShiftAmount = (sizeof(uint)*8) - ((_bitsLeft + 16) + 8);

                    // For uint==32bits, shiftamount is:
                    // = 32 - ((_bitsLeft + 16) + 8)
                    // = 32 - (_bitsLeft + 24)
                    // = 8 - _bitsLeft

                    // For uint==64bits, shiftamount is:
                    // = 64 - ((_bitsLeft + 16) + 8)
                    // = 64 - (_bitsLeft + 24)
                    // = 24 - _bitsLeft
                    uint loadedByte;
                    int bit2Load = (_lastByteIndex - byteIndex + 1) * 8;
                    if (MAX_EONALU_RSIZE < bit2Load)
                    {
                        bit2Load = MAX_EONALU_RSIZE;
                    }

                    loadedByte = BigEndianToHost(BitConverter.ToUInt32(bytesToLoad, 0)) & (unchecked((uint)~0) << (sizeof(uint) * 8 - bit2Load));

                    _bitMask += (loadedByte >> (_bitsLeft + 16));
                    byteIndex += (bit2Load >> 3);
                    _bitsLeft += bit2Load;
                }
                else if (byteIndex > _lastByteIndex && 0 == _bitMask)
                {
                    _ei_flag = -1;
                }

                _currentByteIndex = byteIndex;
            }

            /// <summary>
            /// Reset the bit stream
            /// </summary>
            /// <param name="buffer">The byte array to be converted to bit stream</param>
            public void Reset(byte[] buffer)
            {
                int bitsLeft = -16;
                uint bitMask = 0;

                int lastByteIndex = buffer.Length - 1;
                int baseBits = ((int)(sizeof(uint) - 4)) * 8 + 8;

                // load up to 32 bits
                int byteIndex = 0;
                while (byteIndex <= lastByteIndex && bitsLeft <= baseBits)
                {
                    bitMask |= ((uint)(buffer[byteIndex++])) << (baseBits - bitsLeft);
                    bitsLeft += 8;
                }

                _currentByteIndex = byteIndex;
                _lastByteIndex = lastByteIndex;
                _bitMask = bitMask;
                _bitsLeft = bitsLeft;
                _ei_flag = 0;
                _total = 0;

                _buffer = buffer;
            }
        }

        internal enum NALU_TYPE
        {
            NALU_TYPE_SLICE = 1,
            NALU_TYPE_DPA = 2,
            NALU_TYPE_DPB = 3,
            NALU_TYPE_DPC = 4,
            NALU_TYPE_IDR = 5,
            NALU_TYPE_SEI = 6,
            NALU_TYPE_SPS = 7,
            NALU_TYPE_PPS = 8,
            NALU_TYPE_AUD = 9,
            NALU_TYPE_EOSEQ = 10,
            NALU_TYPE_EOSTREAM = 11,
            NALU_TYPE_FILL = 12
        }

        internal class NalUnit
        {
            public uint Forbidden_zero_bit { get; set; }
            public uint Nal_ref_idc { get; set; }
            public NALU_TYPE Nal_unit_type { get; set; }
        }

        internal class SequenceParameterSet
        {
            public static int MAX_REF_FRAMES_IN_PIC_ORDER_CNT_CYCLE = 256;

            public byte Profile_idc { get; set; }                             // u(8)
            public bool Constrained_set0_flag { get; set; }                   // u(1)
            public bool Constrained_set1_flag { get; set; }                   // u(1)
            public bool Constrained_set2_flag { get; set; }                   // u(1)
            public bool Constrained_set3_flag { get; set; }                   // u(1)
            public uint Reserved_zero_4bits { get; set; }                     // u(4)


            public byte Level_idc { get; set; }                               // u(8)
            public uint Seq_parameter_set_id { get; set; }                    // ue(v)

            public uint Chroma_format_idc { get; set; }                       // ue(v)
            public bool Separate_colour_plane_flag { get; set; }              // u(1)
            public uint Bit_depth_luma_minus8 { get; set; }                   // ue(v)
            public uint Bit_depth_chroma_minus8 { get; set; }                 // ue(v)

            public bool Lossless_qpprime_flag { get; set; }                   // u(1)

            public bool Seq_scaling_matrix_present_flag { get; set; }         // u(1)
            public List<bool> Seq_scaling_list_present_flag { get; set; }     // u(1)
            public List<List<short>> ScalingList4x4 { get; set; }           // se(v)
            public List<List<short>> ScalingList8x8 { get; set; }           // se(v)
            public List<bool> UseDefaultScalingMatrix4x4 { get; set; }
            public List<bool> UseDefaultScalingMatrix8x8 { get; set; }

            public uint Log2_max_frame_num_minus4 { get; set; }               // ue(v)

            public uint Pic_order_cnt_type { get; set; }                      // ue(v)
            // if( pic_order_cnt_type == 0 )
            public uint Log2_max_pic_order_cnt_lsb_minus4 { get; set; }       // ue(v)
            // else if( pic_order_cnt_type == 1 )
            public bool Delta_pic_order_always_zero_flag { get; set; }        // u(1)
            public int Offset_for_non_ref_pic { get; set; }                   // se(v)
            public int Offset_for_top_to_bottom_field { get; set; }           // se(v)
            public uint Num_ref_frames_in_pic_order_cnt_cycle { get; set; }   // ue(v)
            public int[] Offset_for_ref_frame { get; set; }                   // se(v)

            public uint Num_ref_frames { get; set; }                          // ue(v)

            public bool Gaps_in_frame_num_value_allowed_flag { get; set; }    // u(1)
            public uint Pic_width_in_mbs_minus1 { get; set; }                 // ue(v)
            public uint Pic_height_in_map_units_minus1 { get; set; }          // ue(v)
            public bool Frame_mbs_only_flag { get; set; }                     // u(1)
            public bool Mb_adaptive_frame_field_flag { get; set; }            // u(1)
            public bool Direct_8x8_inference_flag { get; set; }               // u(1)
            public bool Frame_cropping_flag { get; set; }                     // u(1)

            public uint Frame_crop_left_offset { get; set; }                  // ue(v)
            public uint Frame_crop_right_offset { get; set; }                 // ue(v)
            public uint Frame_crop_top_offset { get; set; }                   // ue(v)
            public uint Frame_crop_bottom_offset { get; set; }                // ue(v)

            public bool Vui_parameters_present_flag { get; set; }             // u(1)
            public VuiParameters Vui_parameters { get; set; }

            //
            // TODO: There are still more fields after this but we are interested up to here for now.
            //
        }

        internal class VuiParameters
        {
            public bool Aspect_ratio_info_present_flag { get; set; }        // u(1)
            public uint Aspect_ratio_idc { get; set; }                      // u(8)
            public uint Sar_width { get; set; }                             // u(16)
            public uint Sar_height { get; set; }                            // u(16)

            public bool Overscan_info_present_flag { get; set; }            // u(1)
            public bool Overscan_appropriate_flag { get; set; }             // u(1)

            public bool Video_signal_type_present_flag { get; set; }        // u(1)
            public uint Video_format { get; set; }                          // u(3)
            public bool Video_full_range_flag { get; set; }                 // u(1)

            public bool Color_description_present_flag { get; set; }        // u(1)
            public uint Color_primaries { get; set; }                       // u(8)
            public uint Transfer_characteristics { get; set; }              // u(8)
            public uint Matrix_coefficients { get; set; }                   // u(8)

            public bool Chroma_loc_info_present_flag { get; set; }          // u(1)
            public uint Chroma_sample_loc_type_top_field { get; set; }      // ue(v)
            public uint Chroma_sample_loc_type_bottom_field { get; set; }   // ue(v)

            public bool Timing_info_present_flag { get; set; }              // u(1)
            public uint Num_units_in_tick { get; set; }                     // u(32)
            public uint Time_scale { get; set; }                            // u(32)
            public bool Fixed_frame_rate_flag { get; set; }                 // u(1)

            public bool Nal_hrd_parameters_present_flag { get; set; }       // u(1)
            public HrdParameters Nal_hrd_parameters { get; set; }

            public bool Vcl_hrd_parameters_present_flag { get; set; }       // u(1)
            public HrdParameters Vcl_hrd_parameters { get; set; }

            public bool Low_delay_hrd_flag { get; set; }                    // u(1)

            public bool Pic_struct_present_flag { get; set; }               // u(1)

            public bool Bitstream_restriction_flag { get; set; }            // u(1)
            public bool Motion_vectors_over_pic_boundaries_flag { get; set; }   // u(1)
            public uint Max_bytes_per_pic_denom { get; set; }               // ue(v)
            public uint Max_bits_per_mb_denom { get; set; }                 // ue(v)
            public uint Log2_max_mv_length_horizontal { get; set; }         // ue(v)
            public uint Log2_max_mv_length_vertical { get; set; }           // ue(v)
            public uint Num_reorder_frames { get; set; }                    // ue(v)
            public uint Max_dec_frame_buffering { get; set; }               // ue(v)

        }

        internal class HrdParameters
        {
            public uint Cpb_cnt_minus1 { get; set; }                        // ue(v)
            public uint Bit_rate_scale { get; set; }                        // u(4)
            public uint Cpb_size_scale { get; set; }                        // u(4)

            public uint[] Bit_rate_value_minus1 { get; set; }               // Array of ue(v)
            public uint[] Cpb_size_value_minus1 { get; set; }               // Array of ue(v)
            public bool[] Cbr_flag { get; set; }                            // Array of u(1)

            public uint Initial_cpb_removal_delay_length_minus1 { get; set; }   // u(5)
            public uint Cpb_removal_delay_length_minus1 { get; set; }           // u(5)
            public uint Dpb_output_delay_length_minus1 { get; set; }            // u(5)
            public uint Time_offset_length { get; set; }                        // u(5)
        }

        /// <summary>
        /// This class is used to parse the CodecPrivateData of H264 stream
        /// </summary>
        internal class H264CodecPrivateDataParser
        {
            //FREXT Profile IDC definitions
            private const int FREXT_HP = 100;         //!< YUV 4:2:0/8 "High"
            private const int FREXT_Hi10P = 110;      //!< YUV 4:2:0/10 "High 10"
            private const int FREXT_Hi422 = 122;      //!< YUV 4:2:2/10 "High 4:2:2"
            private const int FREXT_Hi444 = 144;      //!< YUV 4:4:4/12 "High 4:4:4"

            // very conservative max
            private const int MAX_ENCODED_SIZE_IN_BYTES = 4;
            private const int NUMBER_OF_ENCODED_ITEMS_IN_SPS = 14;

            private readonly byte[] ZZ_SCAN =
        {  0,  1,  4,  8,  5,  2,  3,  6,  9, 12, 13, 10,  7, 11, 14, 15
        };

            private readonly byte[] ZZ_SCAN8 =
        {  0,  1,  8, 16,  9,  2,  3, 10, 17, 24, 32, 25, 18, 11,  4,  5,
           12, 19, 26, 33, 40, 48, 41, 34, 27, 20, 13,  6,  7, 14, 21, 28,
           35, 42, 49, 56, 57, 50, 43, 36, 29, 22, 15, 23, 30, 37, 44, 51,
           58, 59, 52, 45, 38, 31, 39, 46, 53, 60, 61, 54, 47, 55, 62, 63
        };

            private BitReader _bitReader;

            public SequenceParameterSet ParsedSPS { get; set; }

            /// <summary>
            /// Constructor
            /// 
            public H264CodecPrivateDataParser()
            {
                _bitReader = new BitReader();

                ParsedSPS = new SequenceParameterSet();
                ParsedSPS.Seq_scaling_list_present_flag = new List<bool>(8);
                for (int i = 0; i < 8; ++i)
                {
                    ParsedSPS.Seq_scaling_list_present_flag.Add(false);
                }

                ParsedSPS.ScalingList4x4 = new List<List<short>>(6);
                for (int i = 0; i < 6; ++i)
                {
                    List<short> scalingListItem = new List<short>(16);
                    for (int j = 0; j < 16; ++j)
                    {
                        scalingListItem.Add(0);
                    }
                    ParsedSPS.ScalingList4x4.Add(scalingListItem);
                }

                ParsedSPS.ScalingList8x8 = new List<List<short>>(2);
                for (int i = 0; i < 2; ++i)
                {
                    List<short> scalingListItem = new List<short>(64);
                    for (int j = 0; j < 64; ++j)
                    {
                        scalingListItem.Add(0);
                    }
                    ParsedSPS.ScalingList8x8.Add(scalingListItem);
                }

                ParsedSPS.UseDefaultScalingMatrix4x4 = new List<bool>(6);
                for (int i = 0; i < 6; ++i)
                {
                    ParsedSPS.UseDefaultScalingMatrix4x4.Add(false);
                }
                ParsedSPS.UseDefaultScalingMatrix8x8 = new List<bool>(2);
                for (int i = 0; i < 2; ++i)
                {
                    ParsedSPS.UseDefaultScalingMatrix8x8.Add(false);
                }
            }

            /// <summary>
            /// Parse the CodecPrivateData
            /// </summary>
            /// <returns></returns>
            public bool Parse(byte[] codecPrivateData)
            {
                bool success = false;

                if (codecPrivateData.Length > 1)
                {
                    _bitReader.Reset(codecPrivateData);
                }

                NalUnit nalUnit = new NalUnit();

                if (!ParseNalStartCode())
                {
                    //Tracing.Trace(TraceArea.Manifest, TraceLevel.Warning, "Failed to parse the NAL StartCode");
                    goto Finished;
                }

                nalUnit.Forbidden_zero_bit = _bitReader.GetBits(1);
                nalUnit.Nal_ref_idc = _bitReader.GetBits(2);
                nalUnit.Nal_unit_type = (NALU_TYPE)_bitReader.GetBits(5);

                if (NALU_TYPE.NALU_TYPE_SPS == nalUnit.Nal_unit_type)
                {
                    // This is SPS data
                    byte[] rbspBytes = new byte[_bitReader.AvailableBytes];

                    //Tracing.Trace(TraceArea.Manifest, TraceLevel.Informational, "Found SPS {0} bytes", _bitReader.AvailableBytes);

                    if (!FilterNal(rbspBytes))
                    {
                        //Tracing.Trace(TraceArea.Manifest, TraceLevel.Warning, "Failed to filter the NAL StartCode");
                        goto Finished;
                    }

                    _bitReader.Reset(rbspBytes);

                    if (!ParseSPS())
                    {
                        //Tracing.Trace(TraceArea.Manifest, TraceLevel.Warning, "Failed to parse the SPS info");
                        goto Finished;
                    }
                }
                else
                {
                    //Tracing.Trace(TraceArea.Manifest, TraceLevel.Informational, "Not SPS - {0} bytes", codecPrivateData.Length);
                    goto Finished;
                }

                success = true;

            Finished:
                return success;
            }

            /// <summary>
            /// Bypass the NAL start code
            /// </summary>
            /// <returns></returns>
            private bool FilterNal(byte[] rbspBytes)
            {
                uint zeroCount = 0;
                uint numBytes = 0;

                for (uint i = 0; i < rbspBytes.Length; ++i)
                {
                    uint val = _bitReader.GetBits(8);

                    if (2 <= zeroCount && 3 == val)
                    {
                        // emulation_prevention_three_byte, skip it
                        zeroCount = 0;
                    }
                    else
                    {
                        zeroCount = (0 == val) ? zeroCount + 1 : 0;
                        rbspBytes[numBytes] = (byte)val;
                        ++numBytes;
                    }
                }

                return true;
            }

            /// <summary>
            /// Parse the NAL start code
            /// </summary>
            /// <returns></returns>
            private bool ParseNalStartCode()
            {
                bool success = false;
                uint leadingZeros = 0;
                uint val = 0;

                // optional leading zeros + startcode: [00]+ 00 00 01
                while (_bitReader.AvailableData)
                {
                    val = _bitReader.GetBits(8);

                    if (0 == val)
                    {
                        ++leadingZeros;
                    }
                    else if (1 == val && 2 <= leadingZeros)
                    {
                        // found start code
                        break;
                    }
                    else
                    {
                        //Tracing.Trace(TraceArea.Manifest, TraceLevel.Warning, "NAL start code not found");
                        goto Finished;
                    }
                }

                success = true;

            Finished:
                return success;
            }

            /// <summary>
            /// Parse the SPS info
            /// </summary>
            /// <returns></returns>
            private bool ParseSPS()
            {
                bool success = false;

                uint profile_idc = _bitReader.GetBits(8);
                ParsedSPS.Profile_idc = Convert.ToByte(profile_idc);
                //Tracing.Trace(TraceArea.Manifest, TraceLevel.Verbose, "SPS.Profile_idc: {0}", ParsedSPS.Profile_idc);

                if ((profile_idc != 66) &&
                    (profile_idc != 77) &&
                    (profile_idc != 88) &&
                    (profile_idc != 100) &&
                    (profile_idc != 110) &&
                    (profile_idc != 122) &&
                    (profile_idc != 144))
                {
                    goto Finished;
                }

                ParsedSPS.Constrained_set0_flag = _bitReader.GetBool();
                ParsedSPS.Constrained_set1_flag = _bitReader.GetBool();
                ParsedSPS.Constrained_set2_flag = _bitReader.GetBool();
                ParsedSPS.Constrained_set3_flag = _bitReader.GetBool();

                uint reserved_zero = _bitReader.GetBits(4);
                if (0 != reserved_zero)
                {
                    //Tracing.Trace(TraceArea.Manifest, TraceLevel.Warning, "reserved 0 is {0} instead of 0", reserved_zero);
                    goto Finished;
                }

                ParsedSPS.Level_idc = Convert.ToByte(_bitReader.GetBits(8));
                //Tracing.Trace(TraceArea.Manifest, TraceLevel.Verbose, "SPS.Level_idc: {0}", ParsedSPS.Level_idc);

                uint seq_parameter_set_id = _bitReader.GetUEValue();
                ParsedSPS.Seq_parameter_set_id = seq_parameter_set_id;
                //Tracing.Trace(TraceArea.Manifest, TraceLevel.Verbose, "SPS.Seq_parameter_set_id: {0}", ParsedSPS.Seq_parameter_set_id);

                if (seq_parameter_set_id > 31)
                {
                    // sequence_parameter_set_id shall be in the range of 0 to 31, inclusive. Reject anything else.
                    goto Finished;
                }

                // Fidelity Range Extensions stuff
                ParsedSPS.Chroma_format_idc = 1;
                ParsedSPS.Bit_depth_luma_minus8 = 0;
                ParsedSPS.Bit_depth_chroma_minus8 = 0;
                ParsedSPS.Lossless_qpprime_flag = false;

                // Residue Color Transform
                ParsedSPS.Separate_colour_plane_flag = false;

                if ((ParsedSPS.Profile_idc == FREXT_HP)
                   || (ParsedSPS.Profile_idc == FREXT_Hi10P)
                   || (ParsedSPS.Profile_idc == FREXT_Hi422)
                   || (ParsedSPS.Profile_idc == FREXT_Hi444))
                {
                    uint chroma_format_idc;

                    chroma_format_idc = _bitReader.GetUEValue();
                    ParsedSPS.Chroma_format_idc = chroma_format_idc;
                    if (3 == chroma_format_idc)
                    {
                        ParsedSPS.Separate_colour_plane_flag = _bitReader.GetBool();
                    }

                    ParsedSPS.Bit_depth_luma_minus8 = _bitReader.GetUEValue();
                    ParsedSPS.Bit_depth_chroma_minus8 = _bitReader.GetUEValue();

                    ParsedSPS.Lossless_qpprime_flag = _bitReader.GetBool();
                    ParsedSPS.Seq_scaling_matrix_present_flag = _bitReader.GetBool();

                    if (ParsedSPS.Seq_scaling_matrix_present_flag)
                    {
                        int i;
                        for (i = 0; i < 8; ++i)
                        {
                            ParsedSPS.Seq_scaling_list_present_flag[i] = _bitReader.GetBool();
                            if (ParsedSPS.Seq_scaling_list_present_flag[i])
                            {
                                bool useDefaultScalingMatrix;
                                if (i < 6)
                                {
                                    // make sure i < 6 so that there is no overflow
                                    if (!ParseScalingList(ParsedSPS.ScalingList4x4[i], out useDefaultScalingMatrix))
                                    {
                                        goto Finished;
                                    }
                                    else
                                    {
                                        ParsedSPS.UseDefaultScalingMatrix4x4[i] = useDefaultScalingMatrix;
                                    }
                                }
                                else
                                {
                                    if (!ParseScalingList(ParsedSPS.ScalingList8x8[i - 6], out useDefaultScalingMatrix))
                                    {
                                        goto Finished;
                                    }
                                    else
                                    {
                                        ParsedSPS.UseDefaultScalingMatrix8x8[i - 6] = useDefaultScalingMatrix;
                                    }
                                }
                            }
                        }
                    }
                }

                ParsedSPS.Log2_max_frame_num_minus4 = _bitReader.GetUEValue();
                //Tracing.Trace(TraceArea.Manifest, TraceLevel.Verbose, "SPS.Log2_max_frame_num_minus4: {0}", ParsedSPS.Log2_max_frame_num_minus4);
                if (ParsedSPS.Log2_max_frame_num_minus4 > 12)
                {
                    goto Finished;
                }

                ParsedSPS.Pic_order_cnt_type = _bitReader.GetUEValue();
                //Tracing.Trace(TraceArea.Manifest, TraceLevel.Verbose, "SPS.Pic_order_cnt_type: {0}", ParsedSPS.Pic_order_cnt_type);
                if (ParsedSPS.Pic_order_cnt_type > 2)
                {
                    goto Finished;
                }

                if (0 == ParsedSPS.Pic_order_cnt_type)
                {
                    ParsedSPS.Log2_max_pic_order_cnt_lsb_minus4 = _bitReader.GetUEValue();
                    //Tracing.Trace(TraceArea.Manifest, TraceLevel.Verbose, "SPS.Log2_max_pic_order_cnt_lsb_minus4: {0}", ParsedSPS.Log2_max_pic_order_cnt_lsb_minus4);
                    if (ParsedSPS.Log2_max_pic_order_cnt_lsb_minus4 > 12)
                    {
                        goto Finished;
                    }
                }
                else if (1 == ParsedSPS.Pic_order_cnt_type)
                {
                    uint i;

                    ParsedSPS.Delta_pic_order_always_zero_flag = _bitReader.GetBool();
                    ParsedSPS.Offset_for_non_ref_pic = _bitReader.GetSEValue();
                    ParsedSPS.Offset_for_top_to_bottom_field = _bitReader.GetSEValue();
                    ParsedSPS.Num_ref_frames_in_pic_order_cnt_cycle = _bitReader.GetUEValue();

                    if (SequenceParameterSet.MAX_REF_FRAMES_IN_PIC_ORDER_CNT_CYCLE < ParsedSPS.Num_ref_frames_in_pic_order_cnt_cycle)
                    {
                        goto Finished;
                    }

                    for (i = 0; i < ParsedSPS.Num_ref_frames_in_pic_order_cnt_cycle; ++i)
                    {
                        ParsedSPS.Offset_for_ref_frame[i] = _bitReader.GetSEValue();
                    }
                }

                ParsedSPS.Num_ref_frames = _bitReader.GetUEValue();
                //Tracing.Trace(TraceArea.Manifest, TraceLevel.Verbose, "SPS.Num_ref_frames: {0}", ParsedSPS.Num_ref_frames);
                if (ParsedSPS.Num_ref_frames > 16)
                {
                    goto Finished;
                }

                ParsedSPS.Gaps_in_frame_num_value_allowed_flag = _bitReader.GetBool();

                ParsedSPS.Pic_width_in_mbs_minus1 = _bitReader.GetUEValue();
                ParsedSPS.Pic_height_in_map_units_minus1 = _bitReader.GetUEValue();
                ParsedSPS.Frame_mbs_only_flag = _bitReader.GetBool();
                if (false == ParsedSPS.Frame_mbs_only_flag)
                {
                    ParsedSPS.Mb_adaptive_frame_field_flag = _bitReader.GetBool();
                }

                ParsedSPS.Direct_8x8_inference_flag = _bitReader.GetBool();
                ParsedSPS.Frame_cropping_flag = _bitReader.GetBool();
                if (ParsedSPS.Frame_cropping_flag)
                {
                    ParsedSPS.Frame_crop_left_offset = _bitReader.GetUEValue();
                    ParsedSPS.Frame_crop_right_offset = _bitReader.GetUEValue();
                    ParsedSPS.Frame_crop_top_offset = _bitReader.GetUEValue();
                    ParsedSPS.Frame_crop_bottom_offset = _bitReader.GetUEValue();
                }

                ParsedSPS.Vui_parameters_present_flag = _bitReader.GetBool();
                if (ParsedSPS.Vui_parameters_present_flag)
                {
                    ParsedSPS.Vui_parameters = ParseVuiParameters();
                    if (null == ParsedSPS.Vui_parameters)
                    {
                        goto Finished;
                    }
                }

                // Ignore rbsp_trailing_bits, we don't care

                success = true;

            Finished:
                return success;
            }

            private VuiParameters ParseVuiParameters()
            {
                VuiParameters VUI = new VuiParameters();
                bool success = false;

                VUI.Aspect_ratio_info_present_flag = _bitReader.GetBool();
                if (VUI.Aspect_ratio_info_present_flag)
                {
                    VUI.Aspect_ratio_idc = _bitReader.GetBits(8);

                    const uint extended_SAR = 255;
                    if (VUI.Aspect_ratio_idc == extended_SAR)
                    {
                        VUI.Sar_width = _bitReader.GetBits(16);
                        VUI.Sar_height = _bitReader.GetBits(16);
                    }
                }

                VUI.Overscan_info_present_flag = _bitReader.GetBool();
                if (VUI.Overscan_info_present_flag)
                {
                    VUI.Overscan_appropriate_flag = _bitReader.GetBool();
                }

                VUI.Video_signal_type_present_flag = _bitReader.GetBool();
                if (VUI.Video_signal_type_present_flag)
                {
                    VUI.Video_format = _bitReader.GetBits(3);
                    VUI.Video_full_range_flag = _bitReader.GetBool();
                    VUI.Color_description_present_flag = _bitReader.GetBool();
                    if (VUI.Color_description_present_flag)
                    {
                        VUI.Color_primaries = _bitReader.GetBits(8);
                        VUI.Transfer_characteristics = _bitReader.GetBits(8);
                        VUI.Matrix_coefficients = _bitReader.GetBits(8);
                    }
                }

                VUI.Chroma_loc_info_present_flag = _bitReader.GetBool();
                if (VUI.Chroma_loc_info_present_flag)
                {
                    VUI.Chroma_sample_loc_type_top_field = _bitReader.GetUEValue();
                    VUI.Chroma_sample_loc_type_bottom_field = _bitReader.GetUEValue();
                }

                VUI.Timing_info_present_flag = _bitReader.GetBool();
                if (VUI.Timing_info_present_flag)
                {
                    VUI.Num_units_in_tick = _bitReader.GetBits(32);
                    VUI.Time_scale = _bitReader.GetBits(32);
                    VUI.Fixed_frame_rate_flag = _bitReader.GetBool();
                }

                VUI.Nal_hrd_parameters_present_flag = _bitReader.GetBool();
                if (VUI.Nal_hrd_parameters_present_flag)
                {
                    VUI.Nal_hrd_parameters = ParseHrdParameters();
                    if (null == VUI.Nal_hrd_parameters)
                    {
                        goto Finished;
                    }
                }

                VUI.Vcl_hrd_parameters_present_flag = _bitReader.GetBool();
                if (VUI.Vcl_hrd_parameters_present_flag)
                {
                    VUI.Vcl_hrd_parameters = ParseHrdParameters();
                    if (null == VUI.Vcl_hrd_parameters)
                    {
                        goto Finished;
                    }
                }

                if (VUI.Nal_hrd_parameters_present_flag || VUI.Vcl_hrd_parameters_present_flag)
                {
                    VUI.Low_delay_hrd_flag = _bitReader.GetBool();
                }

                VUI.Pic_struct_present_flag = _bitReader.GetBool();

                VUI.Bitstream_restriction_flag = _bitReader.GetBool();
                if (VUI.Bitstream_restriction_flag)
                {
                    VUI.Motion_vectors_over_pic_boundaries_flag = _bitReader.GetBool();
                    VUI.Max_bytes_per_pic_denom = _bitReader.GetUEValue();
                    VUI.Max_bits_per_mb_denom = _bitReader.GetUEValue();
                    VUI.Log2_max_mv_length_horizontal = _bitReader.GetUEValue();
                    VUI.Log2_max_mv_length_vertical = _bitReader.GetUEValue();
                    VUI.Num_reorder_frames = _bitReader.GetUEValue();
                    VUI.Max_dec_frame_buffering = _bitReader.GetUEValue();
                }

                success = true;

            Finished:
                if (success)
                {
                    return VUI;
                }
                else
                {
                    return null;
                }
            }

            private HrdParameters ParseHrdParameters()
            {
                HrdParameters HRD = new HrdParameters();
                bool success = false;

                HRD.Cpb_cnt_minus1 = _bitReader.GetUEValue();
                HRD.Bit_rate_scale = _bitReader.GetBits(4);
                HRD.Cpb_size_scale = _bitReader.GetBits(4);

                HRD.Bit_rate_value_minus1 = new uint[HRD.Cpb_cnt_minus1 + 1];
                HRD.Cpb_size_value_minus1 = new uint[HRD.Cpb_cnt_minus1 + 1];
                HRD.Cbr_flag = new bool[HRD.Cpb_cnt_minus1 + 1];
                for (int schedSelIdx = 0; schedSelIdx <= HRD.Cpb_cnt_minus1; schedSelIdx++)
                {
                    HRD.Bit_rate_value_minus1[schedSelIdx] = _bitReader.GetUEValue();
                    HRD.Cpb_size_value_minus1[schedSelIdx] = _bitReader.GetUEValue();
                    HRD.Cbr_flag[schedSelIdx] = _bitReader.GetBool();
                }

                HRD.Initial_cpb_removal_delay_length_minus1 = _bitReader.GetBits(5);
                HRD.Cpb_removal_delay_length_minus1 = _bitReader.GetBits(5);
                HRD.Dpb_output_delay_length_minus1 = _bitReader.GetBits(5);
                HRD.Time_offset_length = _bitReader.GetBits(5);

                success = true;

                if (success)
                {
                    return HRD;
                }
                else
                {
                    return null;
                }
            }

            /// <summary>
            /// Parse the scalinglist field
            /// </summary>
            /// <returns></returns>
            private bool ParseScalingList(List<short> scalingList, out bool useDefaultScalingMatrix)
            {
                bool success = false;
                int j;
                int scanj;
                int deltaScale;
                int lastScale;
                int nextScale;

                lastScale = 8;
                nextScale = 8;
                useDefaultScalingMatrix = false;

                for (j = 0; j < scalingList.Count; ++j)
                {
                    scanj = (16 == scalingList.Count) ? ZZ_SCAN[j] : ZZ_SCAN8[j];

                    Debug.Assert(scanj < scalingList.Count);
                    if (scalingList.Count <= scanj)
                    {
                        break;
                    }

                    if (0 != nextScale)
                    {
                        deltaScale = _bitReader.GetSEValue();
                        // The value of delta_scale shall be in the range of  -128 to +127, inclusive.
                        if (deltaScale > 127 || deltaScale < -128)
                        {
                            goto Finished;
                        }

                        nextScale = (lastScale + deltaScale + 256) % 256;
                        useDefaultScalingMatrix = (0 == scanj && 0 == nextScale);
                    }

                    scalingList[scanj] = (short)((0 == nextScale) ? lastScale : nextScale);
                    lastScale = scalingList[scanj];
                }

                success = true;

            Finished:
                return success;
            }

            /// <summary>
            /// This function computes the MaxDPBSize of an H264 bitstream, given the Sequence Parameter Set (SPS).
            /// </summary>
            /// <param name="SPS">The parsed sequence parameter set.</param>
            /// <returns>The MaxDPBSize.</returns>
            private static uint ComputeMaxDPBSize(SequenceParameterSet SPS)
            {
                uint maxDPBSize = 16;
                double maxDPBInBytes = 0;

                // If an unspecified level is given (eg. 4.9), be conservative and use the next level's values
                if (SPS.Level_idc > 51)
                {
                    // Never heard of this level, just assume max value is 16
                    Debug.Assert(false); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBSize = 16;
                    goto Finished;
                }
                else if (SPS.Level_idc > 50)
                {
                    Debug.Assert(51 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 69120;  // Level 5.1
                }
                else if (SPS.Level_idc > 42)
                {
                    Debug.Assert(50 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 41400;  // Level 5
                }
                else if (SPS.Level_idc > 41)
                {
                    Debug.Assert(42 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 13056;  // Level 4.2
                }
                else if (SPS.Level_idc > 32)
                {
                    Debug.Assert(40 == SPS.Level_idc || 41 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 12288;  // Level 4, 4.1
                }
                else if (SPS.Level_idc > 31)
                {
                    Debug.Assert(32 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 7680;   // Level 3.2
                }
                else if (SPS.Level_idc > 30)
                {
                    Debug.Assert(31 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 6750;   // Level 3.1
                }
                else if (SPS.Level_idc > 21)
                {
                    Debug.Assert(22 == SPS.Level_idc || 30 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 3037.5; // Level 2.2, 3
                }
                else if (SPS.Level_idc > 20)
                {
                    Debug.Assert(21 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 1782;
                }
                else if (SPS.Level_idc > 11)
                {
                    Debug.Assert(12 == SPS.Level_idc || 13 == SPS.Level_idc || 20 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 891;    // Level 1.2, 1.3, 2
                }
                else if (SPS.Level_idc > 10)
                {
                    if (SPS.Constrained_set3_flag)
                    {
                        Debug.Assert(11 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                        maxDPBInBytes = 148.5; // Level 1b
                    }
                    else
                    {
                        Debug.Assert(11 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                        maxDPBInBytes = 337.5;  // Level 1.1
                    }
                }
                else
                {
                    Debug.Assert(10 == SPS.Level_idc); // We will handle gracefully but notify programmer anyway to update the code
                    maxDPBInBytes = 148.5;  // Level 1
                }
                maxDPBInBytes *= 1024;

                uint picWidthInMbs = SPS.Pic_width_in_mbs_minus1 + 1;
                uint picHeightInMapUnits = SPS.Pic_height_in_map_units_minus1 + 1;
                uint frameHeightInMbs;
                if (SPS.Frame_mbs_only_flag)
                {
                    frameHeightInMbs = 1;
                }
                else
                {
                    frameHeightInMbs = 2;
                }
                frameHeightInMbs *= picHeightInMapUnits;

                maxDPBSize = Math.Min(16, (uint)maxDPBInBytes / (picWidthInMbs * frameHeightInMbs * 384));

            Finished:
                Debug.Assert(maxDPBSize <= 16);
                return maxDPBSize;
            }

            /// <summary>
            /// This function returns max_dec_frame_buffering as specified in the spec, meaning that if it is
            /// present it is returned, if it is not present then its implicit value is MaxDPBSize.
            /// </summary>
            /// <param name="SPS">The parsed sequence parameter set.</param>
            /// <returns>The value of max_dec_frame_buffering (explicit or implicit).</returns>
            public static uint ComputeMaxDecFrameBuffering(SequenceParameterSet SPS)
            {
                const uint maxReturnValue = 16;
                uint maxDecFrameBuffering = maxReturnValue;
                bool foundMaxDecFrameBuffering = false;
                uint retVal = maxReturnValue;

                // Our default value is MaxDPBSize
                uint maxDPBSize = ComputeMaxDPBSize(SPS);
                retVal = maxDPBSize;

                // Check if encoder explicitly specified max_dec_frame_buffering
                if (SPS.Vui_parameters_present_flag && null != SPS.Vui_parameters)
                {
                    VuiParameters VUI = SPS.Vui_parameters;
                    if (VUI.Bitstream_restriction_flag)
                    {
                        maxDecFrameBuffering = VUI.Max_dec_frame_buffering;
                        foundMaxDecFrameBuffering = true;

                        Debug.Assert(maxDecFrameBuffering <= maxDPBSize); // Handle gracefully but notify programmer
                        if (maxDecFrameBuffering <= maxDPBSize)
                        {
                            retVal = maxDecFrameBuffering;
                        }
                    }
                }

                // Final validation
                if (retVal > maxReturnValue)
                {
                    // Somthing is wrong, bail
                    Debug.Assert(false); // Handle gracefully but notify programmer
                    retVal = maxReturnValue;
                    goto Finished;
                }

                if (retVal < SPS.Num_ref_frames)
                {
                    // Somthing is wrong, bail
                    Debug.Assert(false); // Handle gracefully but notify programmer
                    retVal = maxReturnValue;
                    goto Finished;
                }

            Finished:
                //Tracing.Trace(
                //    TraceArea.Manifest,
                //    TraceLevel.Informational,
                //    "MaxDPBSize = {0}, max_dec_frame_buffering = {1}, num_ref_frames = {2}, using {3}",
                //    maxDPBSize,
                //    foundMaxDecFrameBuffering ? maxDecFrameBuffering.ToString() : "NOT FOUND",
                //    SPS.Num_ref_frames,
                //    retVal);

                return retVal;
            }
        }
    }
}
