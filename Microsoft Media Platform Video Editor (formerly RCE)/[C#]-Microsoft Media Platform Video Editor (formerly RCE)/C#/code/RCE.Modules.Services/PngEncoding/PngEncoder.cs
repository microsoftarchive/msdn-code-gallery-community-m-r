// <copyright file="PngEncoder.cs" company="Microsoft Corporation">
// ===============================================================================
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockAddPreviewEvent.cs                     
//
// A simple PngEncoder created by Joe Stegman (http://blogs.msdn.com/jstegman/archive/2008/10/30/updated-source-for-image-samples.aspx).
//
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// A simple PngEncoder created by Joe Stegman (http://blogs.msdn.com/jstegman/archive/2008/10/30/updated-source-for-image-samples.aspx).
    /// </summary>
    public sealed class PngEncoder
    {
        private const int Adler32Base = 65521;

        private const int MaxBlock = 0xFFFF;
        
        private static byte[] header = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        
        private static byte[] ihdr = { (byte)'I', (byte)'H', (byte)'D', (byte)'R' };
        
        private static byte[] gama = { (byte)'g', (byte)'A', (byte)'M', (byte)'A' };
        
        private static byte[] idat = { (byte)'I', (byte)'D', (byte)'A', (byte)'T' };
        
        private static byte[] iend = { (byte)'I', (byte)'E', (byte)'N', (byte)'D' };
        
        private static byte[] fourByteData = { 0, 0, 0, 0 };
        
        private static byte[] argb = { 0, 0, 0, 0, 0, 0, 0, 0, 8, 6, 0, 0, 0 };

        private static uint[] crcTable = new uint[256];

        private static bool crcTableComputed;

        /// <summary>
        /// Prevents a default instance of the <see cref="PngEncoder"/> class from being created.
        /// </summary>
        private PngEncoder()
        {
        }

        public static Stream Encode(WriteableBitmap wb)
        {
            int rowLength = (wb.PixelWidth * 4) + 1;
            int width = wb.PixelWidth;
            int height = wb.PixelHeight;
            byte[] buffer = new byte[rowLength * height];
            int idx;

            for (idx = 0; idx < height; idx++)
            {
                buffer[idx * rowLength] = 0;    // Filter bit
            }

            // Copy WB into the buffer
            for (idx = 0; idx < width; idx++)
            {
                for (int jdx = 0; jdx < height; jdx++)
                {
                    int pixel = wb.Pixels[(jdx * width) + idx];
                    byte[] color = BitConverter.GetBytes(pixel);

                    // Get position in buffer
                    int start = (jdx * rowLength) + (idx * 4) + 1;
                    buffer[start] = color[2]; // Red
                    buffer[start + 1] = color[1]; // Green
                    buffer[start + 2] = color[0]; // Blue
                    buffer[start + 3] = color[3];  // Alpha
                }
            }

            return Encode(buffer, width, height);
        }

        public static Stream Encode(byte[] data, int width, int height)
        {
            MemoryStream ms = new MemoryStream();

            // Write PNG header
            ms.Write(header, 0, header.Length);

            // Write ihdr
            //  Width:              4 bytes
            //  Height:             4 bytes
            //  Bit depth:          1 byte
            //  Color type:         1 byte
            //  Compression method: 1 byte
            //  Filter method:      1 byte
            //  Interlace method:   1 byte
            byte[] size = BitConverter.GetBytes(width);
            argb[0] = size[3]; 
            argb[1] = size[2]; 
            argb[2] = size[1]; 
            argb[3] = size[0];

            size = BitConverter.GetBytes(height);
            argb[4] = size[3]; 
            argb[5] = size[2]; 
            argb[6] = size[1]; 
            argb[7] = size[0];

            // Write ihdr chunk
            WriteChunk(ms, ihdr, argb);

            //// Set gamma = 1
            //int gammaValue = (int)(2.2f * 100000f);

            //size = BitConverter.GetBytes(gammaValue);
            //fourByteData[0] = size[3]; 
            //fourByteData[1] = size[2]; 
            //fourByteData[2] = size[1]; 
            //fourByteData[3] = size[0];

            //// Write gAMA chunk
            //WriteChunk(ms, gama, fourByteData);

            // Write IDAT chunk
            uint widthLength = (uint)(width * 4) + 1;
            uint idatSize = widthLength * (uint)height;

            // First part of ZLIB header is 78 1101 1010 (DA) 0000 00001 (01)
            // ZLIB info
            //
            // CMF Byte: 78
            //  CINFO = 7 (32K window size)
            //  CM = 8 = (deflate compression)
            // FLG Byte: DA
            //  FLEVEL = 3 (bits 6 and 7 - ignored but signifies max compression)
            //  FDICT = 0 (bit 5, 0 - no preset dictionary)
            //  FCHCK = 26 (bits 0-4 - ensure CMF*256+FLG / 31 has no remainder)
            // Compressed data
            //  FLAGS: 0 or 1
            //    00000 00 (no compression) X (X=1 for last block, 0=not the last block)
            //    LEN = length in bytes (equal to ((width*4)+1)*height
            //    NLEN = one's compliment of LEN
            //    Example: 1111 1011 1111 1111 (FB), 0000 0100 0000 0000 (40)
            //    Data for each line: 0 [RGBA] [RGBA] [RGBA] ...
            //    ADLER32
            uint adler = ComputeAdler32(data);
            MemoryStream comp = new MemoryStream();

            // Calculate number of 64K blocks
            uint rowsPerBlock = MaxBlock / widthLength;
            uint blockSize = rowsPerBlock * widthLength;
            uint blockCount;
            ushort length;
            uint remainder = idatSize;

            if ((idatSize % blockSize) == 0)
            {
                blockCount = idatSize / blockSize;
            }
            else
            {
                blockCount = (idatSize / blockSize) + 1;
            }

            // Write headers
            comp.WriteByte(0x78);
            comp.WriteByte(0xDA);

            for (uint blocks = 0; blocks < blockCount; blocks++)
            {
                // Write LEN
                length = (ushort)((remainder < blockSize) ? remainder : blockSize);

                if (length == remainder)
                {
                    comp.WriteByte(0x01);
                }
                else
                {
                    comp.WriteByte(0x00);
                }

                comp.Write(BitConverter.GetBytes(length), 0, 2);

                // Write one's compliment of LEN
                comp.Write(BitConverter.GetBytes((ushort)(~length)), 0, 2);

                // Write blocks
                comp.Write(data, (int)(blocks * blockSize), length);

                // Next block
                remainder -= blockSize;
            }

            WriteReversedBuffer(comp, BitConverter.GetBytes(adler));
            comp.Seek(0, SeekOrigin.Begin);

            byte[] dat = new byte[comp.Length];
            comp.Read(dat, 0, (int)comp.Length);

            WriteChunk(ms, idat, dat);

            // Write IEND chunk
            WriteChunk(ms, iend, new byte[0]);

            // Reset stream
            ms.Seek(0, SeekOrigin.Begin);

            return ms;

            // See http://www.libpng.org/pub/png//spec/1.2/PNG-Chunks.html
            // See http://www.libpng.org/pub/png/book/chapter08.html#png.ch08.div.4
            // See http://www.gzip.org/zlib/rfc-zlib.html (ZLIB format)
            // See ftp://ftp.uu.net/pub/archiving/zip/doc/rfc1951.txt (ZLIB compression format)
        }

        private static void WriteReversedBuffer(Stream stream, byte[] data)
        {
            int size = data.Length;
            byte[] reorder = new byte[size];

            for (int idx = 0; idx < size; idx++)
            {
                reorder[idx] = data[size - idx - 1];
            }

            stream.Write(reorder, 0, size);
        }

        private static void WriteChunk(Stream stream, byte[] type, byte[] data)
        {
            int idx;
            int size = type.Length;
            byte[] buffer = new byte[type.Length + data.Length];

            // Initialize buffer
            for (idx = 0; idx < type.Length; idx++)
            {
                buffer[idx] = type[idx];
            }

            for (idx = 0; idx < data.Length; idx++)
            {
                buffer[idx + size] = data[idx];
            }

            // Write length
            WriteReversedBuffer(stream, BitConverter.GetBytes(data.Length));

            // Write type and data
            stream.Write(buffer, 0, buffer.Length);   // Should always be 4 bytes

            // Compute and write the CRC
            WriteReversedBuffer(stream, BitConverter.GetBytes(GetCRC(buffer)));
        }

        private static void MakeCRCTable()
        {
            uint c;

            for (int n = 0; n < 256; n++)
            {
                c = (uint)n;
                for (int k = 0; k < 8; k++)
                {
                    if ((c & 0x00000001) > 0)
                    {
                        c = 0xEDB88320 ^ (c >> 1);
                    }
                    else
                    {
                        c = c >> 1;
                    }
                }

                crcTable[n] = c;
            }

            crcTableComputed = true;
        }

        private static uint UpdateCRC(uint crc, byte[] buf, int len)
        {
            uint c = crc;

            if (!crcTableComputed)
            {
                MakeCRCTable();
            }

            for (int n = 0; n < len; n++)
            {
                c = crcTable[(c ^ buf[n]) & 0xFF] ^ (c >> 8);
            }

            return c;
        }

        /* Return the CRC of the bytes buf[0..len-1]. */
        private static uint GetCRC(byte[] buf)
        {
            return UpdateCRC(0xFFFFFFFF, buf, buf.Length) ^ 0xFFFFFFFF;
        }

        private static uint ComputeAdler32(byte[] buf)
        {
            uint s1 = 1;
            uint s2 = 0;
            int length = buf.Length;

            for (int idx = 0; idx < length; idx++)
            {
                s1 = (s1 + (uint)buf[idx]) % Adler32Base;
                s2 = (s2 + s1) % Adler32Base;
            }

            return (s2 << 16) + s1;
        }
    }
}
