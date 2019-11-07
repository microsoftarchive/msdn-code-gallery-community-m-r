// <copyright file="ChunkFragmenteParser.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ChunkFragmenteParser.cs                     
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
    using System.IO;

    /// <summary>
    /// This class knows how to parse our adaptive streaming media bits
    /// </summary>
    public class ChunkFragmenteParser
    {
        /// <summary>
        /// The base box size
        /// </summary>
        private const int BaseBoxSize = 8;

        private static Guid sampleEncryptionBox = new Guid("A2394F52-5A9B-4f14-A244-6C427C648DF4");

        /// <summary>
        /// The 'uuid' box header.  Used for the SampleEncryptionBox and the DrmSpecificHeaderBox
        /// </summary>
        private static byte[] uuidBoxId = { 0x75, 0x75, 0x69, 0x64 };

        /// <summary>
        /// The 'mdat' box header
        /// </summary>
        private static byte[] mdatBoxId = { 0x6d, 0x64, 0x61, 0x74 };

        /// <summary>
        /// The 'mfhd' box header
        /// </summary>
        private static byte[] mfhdBoxId = { 0x6d, 0x66, 0x68, 0x64 };

        /// <summary>
        /// The 'moof' box header
        /// </summary>
        private static byte[] moofBoxId = { 0x6d, 0x6f, 0x6f, 0x66 };

        /// <summary>
        /// The 'tfhf' box header
        /// </summary>
        private static byte[] tfhdBoxid = { 0x74, 0x66, 0x68, 0x64 };

        /// <summary>
        /// The 'traf' box header
        /// </summary>
        private static byte[] trafBoxId = { 0x74, 0x72, 0x61, 0x66 };

        /// <summary>
        /// The 'trun' box header
        /// </summary>
        private static byte[] trunBoxId = { 0x74, 0x72, 0x75, 0x6e };

        /// <summary>
        /// The current offset into the stream
        /// </summary>
        private long m_currentOffsetInBytes;

        /// <summary>
        /// The number of bytes left in the curren subbox
        /// </summary>
        private int m_currentSubboxLeft;

        /// <summary>
        /// The position of the current subbox
        /// </summary>
        private int m_currentSubboxPos;

        /// <summary>
        /// The amount of data left in the stream
        /// </summary>
        private long m_dataLeftInBytes;

        /// <summary>
        /// The size of the sample identifiers for this fragment
        /// </summary>
        private int m_SampleIdentifierSize = 8;

        /// <summary>
        /// The sample identifiers for the samples in this fragment
        /// </summary>
        private string[] m_SampleIdentifiers;

        /// <summary>
        /// The fixed duration, if used
        /// </summary>
        private uint m_fixedDuration;

        /// <summary>
        /// The fixed frame size, if we have one
        /// </summary>
        private uint m_fixedFrameSizeInBytes;

        /// <summary>
        /// The duration of each frame
        /// </summary>
        private uint[] m_frameDurations;

        /// <summary>
        /// The presitation offset of each frame
        /// </summary>
        private int[] m_frameOffsets;
        
        /// <summary>
        /// An array of frame sizes
        /// </summary>
        private uint[] m_frameSizesArray;

        /// <summary>
        /// Buffer which contains our header data
        /// </summary>
        private byte[] m_headerBuffer;

        /// <summary>
        /// The number of frame sizes in the m_rgFrameSizes array
        /// </summary>
        private uint m_numFrameSizes;

        /// <summary>
        /// The number of durations in the m_frameDurations array
        /// </summary>
        private uint m_numTimes;

        /// <summary>
        /// The number of offsets in the m_frameOffsets array
        /// </summary>
        private uint m_numOffsets;

        /// <summary>
        /// The number of bytes left in the subbox
        /// </summary>
        private int m_subboxLeft;

        /// <summary>
        /// The position of the subbox
        /// </summary>
        private int m_subboxPos;

        /// <summary>
        /// The saved frame rate for this chunk.
        /// </summary>
        private double m_cachedFrameRate = -1.0;

        /// <summary>
        /// Gets the frame rate of this chunk.
        /// </summary>
        /// <returns>The frame rate, in fps.</returns>
        public double FrameRate
        {
            get
            {
                // Gets the duration of the first frame as a double
                if (this.m_frameDurations != null)
                {
                    if (this.m_cachedFrameRate == -1.0)
                    {
                        double duration = 0.0;
                        for (uint i = 0; i < this.m_numTimes; ++i)
                        {
                            duration += this.m_frameDurations[i];
                        }

                        this.m_cachedFrameRate = 10000000.0 * (double)this.m_numTimes / duration;
                    }

                    return this.m_cachedFrameRate;
                }

                // Default to fixed duration
                return 10000000.0 / (double)this.m_fixedDuration;
            }
        }

        public bool ParseHeader(Stream stream)
        {
            bool isHeaderFound = false;
            bool isDataFound = false;

            // Reset the stream
            stream.Position = 0;

            // Do we have enough data to parse this stream?
            if ((stream.Length - stream.Position) < BaseBoxSize)
            {
                return false;
            }

            byte[] boxID;
            byte[] rawSize;
            uint sizeInBytes;

            // Pick off and ignore the 'moof' header
            rawSize = ReadBytes(4, stream);
            boxID = ReadBytes(4, stream);
            long moofSize = IntFromArray(4, rawSize);

            if (IntFromArray(4, moofBoxId) != IntFromArray(4, boxID))
            {
                throw new ChunkParserException("Unknown stream type");
            }

            // Check for large size
            if (moofSize == 1)
            {
                if ((moofSize = ReadLargeSize(stream)) < 0)
                {
                    return false;
                }
            }

            // Pick off and ignore 'mfhd'
            rawSize = ReadBytes(4, stream);
            boxID = ReadBytes(4, stream);
            long mfhdSize = IntFromArray(4, rawSize);

            // Make sure we are at the mfhd box
            if (IntFromArray(4, mfhdBoxId) != IntFromArray(4, boxID))
            {
                throw new ChunkParserException("Unknown stream type");
            }

            // Check for large size
            if (mfhdSize == 1)
            {
                if ((mfhdSize = ReadLargeSize(stream)) < 0)
                {
                    return false;
                }

                mfhdSize -= 8;
            }

            // Skip the rest of the mfhd field
            stream.Position += (mfhdSize - 8);

            do
            {
                // Great, now move onto the 'traf' box
                rawSize = ReadBytes(4, stream);
                boxID = ReadBytes(4, stream);
                sizeInBytes = (uint)IntFromArray(4, rawSize);
                if (sizeInBytes == 1)
                {
                    if ((stream.Length - stream.Position) < 8)
                    {
                        return false;
                    }

                    byte[] bigSize;
                    bigSize = ReadBytes(8, stream);
                    sizeInBytes = (uint)IntFromArray(8, bigSize);

                    if (sizeInBytes < 8)
                    {
                        throw new ChunkParserException();
                    }

                    sizeInBytes -= 8;
                }

                if (sizeInBytes < BaseBoxSize)
                {
                    throw new ChunkParserException();
                }

                sizeInBytes -= BaseBoxSize;

                if ((stream.Length - stream.Position) < sizeInBytes)
                {
                    // Don't have enough yet - can happen
                    return false;
                }

                if (Equal4cc(trafBoxId, boxID))
                {
                    uint trunSampleCount = 0;
                    bool haveTrun = false;
                    byte[] subboxType;

                    this.m_headerBuffer = new byte[sizeInBytes];
                    stream.Read(this.m_headerBuffer, 0, (int)sizeInBytes);
                    this.SubboxInit(0, (int)sizeInBytes);

                    // Indicate it's been read so we don't skip over it below
                    sizeInBytes = 0;
                    while (true == this.SubboxNext(out subboxType, out this.m_currentSubboxPos, out this.m_currentSubboxLeft))
                    {
                        uint flags;
                        byte version;

                        if (Equal4cc(tfhdBoxid, subboxType))
                        {
                            this.ParseVersionAndFlags(out version, out flags);

                            // Spec says unknown versions shall be ignored
                            if (0 == version)
                            {
                                // Track_ID
                                this.ReadIntFromSubbox(4);

                                if ((0x01 & flags) != 0)
                                {
                                    // Base data offset
                                    this.ReadIntFromSubbox(4);
                                }

                                if ((0x02 & flags) != 0)
                                {
                                    // Sample description index
                                    this.ReadIntFromSubbox(4);
                                }

                                if ((0x08 & flags) != 0)
                                {
                                    this.m_fixedDuration = (uint)this.ReadIntFromSubbox(4);
                                }

                                if ((0x10 & flags) != 0)
                                {
                                    this.m_fixedFrameSizeInBytes = (uint)this.ReadIntFromSubbox(4);
                                }

                                if ((0x20 & flags) != 0)
                                {
                                    // Sample flags
                                    this.ReadIntFromSubbox(4);
                                }
                            }
                        }
                        else if (Equal4cc(trunBoxId, subboxType))
                        {
                            this.ParseVersionAndFlags(out version, out flags);

                            // Spec says unknown versions shall be ignored
                            // Not dealing with multiple truns !
                            if (0 == version && !haveTrun)
                            {
                                this.ParseTrun(flags, out trunSampleCount);
                                haveTrun = true;
                            }
                        }
                        else if (Equal4cc(uuidBoxId, subboxType))
                        {
                            Guid userType = Guid.Empty;

                            this.ParseGuid(out userType);

                            if (userType == sampleEncryptionBox)
                            {
                                this.ParseVersionAndFlags(out version, out flags);

                                if ((flags & 0x1) == 0x1)
                                {
                                    // currently unsupported
                                    throw new ChunkParserException("Unsupported SampleEncryptionBox flags value");
                                }

                                // Spec says unknown versions shall be ignored
                                if (0 == version)
                                {
                                    this.m_SampleIdentifiers = this.ParseSampleIdentifiers(this.m_SampleIdentifierSize);
                                }
                                else
                                {
                                    // we only understand v0, throw an exception so we don't pass encrypted data to the decoder
                                    throw new ChunkParserException("Unsupported SampleEncryptionBox version value");
                                }
                            }
                        }
                    }

                    isHeaderFound = true;
                }
                else if (Equal4cc(mdatBoxId, boxID))
                {
                    this.m_dataLeftInBytes = sizeInBytes;
                    this.m_currentOffsetInBytes = stream.Position;
                    isDataFound = true;
                    break;
                }

                if (isHeaderFound && isDataFound)
                {
                    break;
                }

                // Skip the rest of this box
                stream.Position += sizeInBytes;
            }
            while (true);

            return isHeaderFound && isDataFound && (this.m_fixedFrameSizeInBytes != 0 || this.m_frameSizesArray != null);
        }
        
        /// <summary>
        /// Converts an array to an integer
        /// </summary>
        /// <param name="numBytes">the size of the array</param>
        /// <param name="rb">the array to convert</param>
        /// <returns>the new integer</returns>
        internal static long IntFromArray(int numBytes, byte[] rb)
        {
            long tmp = 0;
            int c;

            for (c = 0; c < numBytes; c++)
            {
                tmp = (tmp << 8) | rb[c];
            }

            return tmp;
        }

        /// <summary>
        /// Are two 4cc's equal
        /// </summary>
        /// <param name="a">first 4CC to check</param>
        /// <param name="b">second 4CC to check</param>
        /// <returns>true if they are equal</returns>
        private static bool Equal4cc(byte[] a, byte[] b)
        {
            return a[0] == b[0] && a[1] == b[1] && a[2] == b[2] && a[3] == b[3];
        }

        /// <summary>
        /// Reads bytes from our stream
        /// </summary>
        /// <param name="numBytes">number of bytes to read</param>
        /// <param name="stream">stream to read from</param>
        /// <returns>array of bytes read</returns>
        private static byte[] ReadBytes(int numBytes, Stream stream)
        {
            byte[] p = new byte[numBytes];
            stream.Read(p, 0, numBytes);
            return p;
        }

        /// <summary>
        /// Read a large size from the stream
        /// </summary>
        /// <param name="stream">stream to read from</param>
        /// <returns>the large size read</returns>
        private static long ReadLargeSize(Stream stream)
        {
            // Suck out the box size
            ReadBytes(4, stream);

            // Do we have enough data to read?
            if ((stream.Length - stream.Position) < 8)
            {
                return -1;
            }

            byte[] bigSize;
            bigSize = ReadBytes(8, stream);
            long size = IntFromArray(8, bigSize);
            if (size < 8)
            {
                throw new ChunkParserException();
            }

            return size;
        }
        
        /// <summary>
        /// Parses the trun box
        /// </summary>
        /// <param name="flags">flags from the box</param>
        private void ParseTrun(uint flags, out uint numSamples)
        {
            numSamples = (uint)this.ReadIntFromSubbox(4);

            if (numSamples > 0)
            {
                // Spec says:
                // "The number of optional fields is
                //  determined from the number of bits
                //  set in the lower byte of the flags,
                //  and the size of a record from the
                //  bits set in the second byte of the
                //  flags. This procedure shall be followed,
                //  to allow for new fields to be defined."
                // So that's what this code below does.
                //
                // Note: this logic pattern feels like
                // it should be abstracted into a subroutine
                // so that it can be used for more things,
                // but I only found one instance of the above
                // language in the spec.  So keeping the code
                // inlined for now for readability.

                // read global (not per-sample) fields
                for (uint f = 1; f < 0x100; f <<= 1)
                {
                    if ((f & flags) != 0)
                    {
                        this.ReadIntFromSubbox(4);
                    }
                }

                // allcate space for per-sample fields we want
                if ((0x100 & flags) != 0)
                {
                    this.m_numTimes = numSamples;
                    this.m_frameDurations = new uint[numSamples];
                }

                if ((0x200 & flags) != 0)
                {
                    this.m_numFrameSizes = numSamples;
                    this.m_frameSizesArray = new uint[numSamples];
                }

                if ((0x800 & flags) != 0)
                {
                    this.m_numOffsets = numSamples;
                    this.m_frameOffsets = new int[numSamples];
#if TRACER
                    Tracer.Trace(TraceChannel.BFrames, "Allocated space for BFrames m_numOffsets={0}", m_numOffsets.ToString());
#endif
                }

                // Read per-sample fields.
                for (uint sample = 0; sample < numSamples; sample++)
                {
                    // I'm sure some day somebody will special-case
                    // this in the name of performance.
                    for (uint f = 0x100; f < 0x10000; f <<= 1)
                    {
                        if ((f & flags) != 0)
                        {
                            int i = (int)this.ReadIntFromSubbox(4);

                            if (0x100 == f)
                            {
                                this.m_frameDurations[sample] = (uint)i;
                            }

                            if (0x200 == f)
                            {
                                this.m_frameSizesArray[sample] = (uint)i;
                            }

                            if (0x800 == f)
                            {
                                this.m_frameOffsets[sample] = i;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse the version and flags from a subbox
        /// </summary>
        /// <param name="version">the version we parsed</param>
        /// <param name="flags">the flags we parsed</param>
        private void ParseVersionAndFlags(out byte version, out uint flags)
        {
            if (this.m_currentSubboxLeft < 4)
            {
                throw new ChunkParserException();
            }

            version = this.m_headerBuffer[this.m_currentSubboxPos];

            byte[] p;
            p = new byte[3];
            Array.Copy(this.m_headerBuffer, this.m_currentSubboxPos + 1, p, 0, 3);
            flags = (uint)IntFromArray(3, p);

            this.m_currentSubboxPos += 4;
            this.m_currentSubboxLeft -= 4;
        }

        private void ParseGuid(out Guid value)
        {
            if (this.m_currentSubboxLeft < 16)
            {
                throw new ChunkParserException();
            }

            int a = (int)this.ReadIntFromSubbox(4);
            short b = (short)this.ReadIntFromSubbox(2);
            short c = (short)this.ReadIntFromSubbox(2);
            byte[] d = this.ReadBytes(8, ref this.m_currentSubboxPos, ref this.m_currentSubboxLeft);

            value = new Guid(a, b, c, d);
        }

        private void ParseEncryptionParameters(out int algorithmId, out int sampleIdentifierSize, out Guid keyId)
        {
            if (this.m_currentSubboxLeft < 20)
            {
                throw new ChunkParserException();
            }

            //
            //  Parse the Algorithm Identifier
            //
            algorithmId = Convert.ToInt32(this.ReadIntFromSubbox(3));

            //
            //  Parse the sample identifier size
            //
            sampleIdentifierSize = Convert.ToInt32(this.ReadIntFromSubbox(1));

            //
            //  Parse the key identifier
            //
            this.ParseGuid(out keyId);
        }

        private string[] ParseSampleIdentifiers(int sampleIdentifierSize)
        {
            //
            //  Parse out the number of samples
            //
            if (this.m_currentSubboxLeft < 4)
            {
                throw new ChunkParserException();
            }

            int sample_count = Convert.ToInt32(this.ReadIntFromSubbox(4));

            //
            //  Parse out the number of sample identifiers themselves
            //
            int bytesRequiredBySampleIdentifiers = sample_count * sampleIdentifierSize;

            if (this.m_currentSubboxLeft < bytesRequiredBySampleIdentifiers)
            {
                throw new ChunkParserException();
            }

            string[] arrayOfSampleIdentifiers = new string[sample_count];
            byte[] currentSampleIdentifier = null;

            for (long index = 0; index < sample_count; index++)
            {
                currentSampleIdentifier = this.ReadBytes(sampleIdentifierSize, ref this.m_currentSubboxPos, ref this.m_currentSubboxLeft);
                arrayOfSampleIdentifiers[index] = Convert.ToBase64String(currentSampleIdentifier);
            }

            return arrayOfSampleIdentifiers;
        }

        /// <summary>
        /// Reads some bytes from our data stream
        /// </summary>
        /// <param name="numBytes">number of bytes to read</param>
        /// <param name="newPosition">position to read</param>
        /// <param name="numBytesLeft">position left</param>
        /// <returns>the bytes we read</returns>
        private byte[] ReadBytes(int numBytes, ref int newPosition, ref int numBytesLeft)
        {
            byte[] p;

            if (numBytesLeft < numBytes)
            {
                throw new ChunkParserException();
            }

            p = new byte[numBytes];
            Array.Copy(this.m_headerBuffer, newPosition, p, 0, numBytes);
            newPosition += numBytes;
            numBytesLeft -= numBytes;
            return p;
        }

        /// <summary>
        /// Reads an integer from our data
        /// </summary>
        /// <param name="numBytes">number of bytes to read</param>
        /// <param name="newPosition">position to read</param>
        /// <param name="numBytesLeft">position left</param>
        /// <returns>the integer we read</returns>
        private long ReadInt(int numBytes, ref int newPosition, ref int numBytesLeft)
        {
            byte[] rb = this.ReadBytes(numBytes, ref newPosition, ref numBytesLeft);
            return IntFromArray(numBytes, rb);
        }

        /// <summary>
        /// Reads an integer from a subsize
        /// </summary>
        /// <param name="numBytes">size of the int to red</param>
        /// <returns>the int we read</returns>
        private long ReadIntFromSubbox(int numBytes)
        {
            byte[] rb = this.ReadBytes(numBytes, ref this.m_currentSubboxPos, ref this.m_currentSubboxLeft);
            return IntFromArray(numBytes, rb);
        }

        /// <summary>
        /// Inits a subbox for reading
        /// </summary>
        /// <param name="startPosition">the start of the subbox</param>
        /// <param name="numBytes">the size of the subbox</param>
        private void SubboxInit(int startPosition, int numBytes)
        {
            this.m_subboxPos = startPosition;
            this.m_subboxLeft = numBytes;
        }

        /// <summary>
        /// Go to the next subbox
        /// </summary>
        /// <param name="subboxType">the type of the subbox</param>
        /// <param name="subboxStartPosition">the start of the subbox</param>
        /// <param name="subboxSize">the size of the subbox</param>
        /// <returns>true if we found a subbox</returns>
        private bool SubboxNext(out byte[] subboxType, out int subboxStartPosition, out int subboxSize)
        {
            subboxType = null;
            subboxStartPosition = 0;
            subboxSize = 0;

            if (this.m_subboxLeft < 8)
            {
                return false;
            }

            subboxStartPosition = this.m_subboxPos;
            subboxSize = (int)this.ReadInt(4, ref this.m_subboxPos, ref this.m_subboxLeft);
            subboxType = this.ReadBytes(4, ref this.m_subboxPos, ref this.m_subboxLeft);

            if (subboxSize == 1)
            {
                if (this.m_subboxLeft < 8)
                {
                    return false;
                }

                subboxSize = (int)this.ReadInt(8, ref this.m_subboxPos, ref this.m_subboxLeft);

                if (subboxSize < 8)
                {
                    throw new ChunkParserException();
                }

                subboxSize -= 8;
            }

            if (subboxSize < 8)
            {
                throw new ChunkParserException();
            }

            subboxSize -= 8;

            if (subboxSize > this.m_subboxLeft)
            {
                // Containment violation
                throw new ChunkParserException();
            }

            subboxStartPosition = this.m_subboxPos;
            this.m_subboxPos += subboxSize;
            this.m_subboxLeft -= subboxSize;

            return true;
        }
    }

    /// <summary>
    /// An exception class that you can use to signal Parsing exceptions. These are
    /// non-fatal and the rest of the app will try to continue
    /// </summary>
    public class ChunkParserException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ChunkParserException class
        /// </summary>
        public ChunkParserException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ChunkParserException class
        /// </summary>
        /// <param name="message">exception message</param>
        public ChunkParserException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ChunkParserException class
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="innerException">exception to wrap</param>
        public ChunkParserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
