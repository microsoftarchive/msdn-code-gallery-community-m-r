////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    /// <summary>
    /// Provides the SPI functionality.
    /// </summary>
    public abstract class Spi : System.IDisposable
    {
        /// <summary>
        /// Gets or sets the configuration of this <see cref="Spi" /> interface.
        /// </summary>
        public abstract SpiConfiguration Configuration { get; set; }
        /// <summary>
        /// Writes an array of bytes to the interface, and reads an array of bytes from the interface into a specified location in the read buffer.
        /// </summary>
        /// <param name="writeBuffer">The buffer whose contents will be written to the interface.</param>
        /// <param name="writeOffset">The offset in <paramref name="writeBuffer" /> to start write data from.</param>
        /// <param name="writeLength">The number of elements in <paramref name="writeBuffer" /> to write.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        /// <param name="readOffset">The offset in <paramref name="readBuffer" /> to start read data from.</param>
        /// <param name="readLength">The number of elements in <paramref name="readBuffer" /> to fill.</param>
        /// <param name="startReadOffset">The offset in time, measured in transacted elements from <paramref name="writeBuffer" />, when to start reading back data into <paramref name="readBuffer" />.</param>
        public abstract void WriteRead(byte[] writeBuffer, int writeOffset, int writeLength, byte[] readBuffer, int readOffset, int readLength, int startReadOffset);
        /// <summary>
        /// Writes an array of unsigned values to the interface, and reads an array of unsigned values from the interface into a specified location in the read buffer.
        /// </summary>
        /// <param name="writeBuffer">The buffer whose contents will be written to the interface.</param>
        /// <param name="writeOffset">The offset in <paramref name="writeBuffer" /> to start write data from.</param>
        /// <param name="writeLength">The number of elements in <paramref name="writeBuffer" /> to write.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        /// <param name="readOffset">The offset in <paramref name="readBuffer" /> to start read data from.</param>
        /// <param name="readLength">The number of elements in <paramref name="readBuffer" /> to fill.</param>
        /// <param name="startReadOffset">The offset in time, measured in transacted elements from <paramref name="writeBuffer" />, when to start reading back data into <paramref name="readBuffer" />.</param>
        public abstract void WriteRead(ushort[] writeBuffer, int writeOffset, int writeLength, ushort[] readBuffer, int readOffset, int readLength, int startReadOffset);

        /// <summary>
        /// Writes an array of bytes to the SPI interface, and reads an array of bytes from the interface into a 
        /// specified location in the read buffer.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        /// <param name="readOffset">
        ///  The offset in time, measured in transacted elements from <paramref name="writeBuffer"/>, 
        ///  to start reading data into.<paramref name="readBuffer"/>.
        /// </param>
        public void WriteRead(byte[] writeBuffer, byte[] readBuffer, int readOffset)
        {
            WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, readOffset);            
        }

        /// <summary>
        /// Writes an array of bytes to the SPI interface, and reads an array of 
        /// bytes from the interface into a specified location in the read buffer. 
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        /// <param name="readOffset">
        ///  The offset in time, measured in transacted elements from <paramref name="writeBuffer"/>, 
        ///  to start reading data into <paramref name="readBuffer"/>.
        /// </param>
        public void WriteRead(ushort[] writeBuffer, ushort[] readBuffer, int readOffset)
        {
            WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, readOffset);
        }

        /// <summary>
        ///  Writes an array of bytes to the interface, and reads an array of bytes from the interface.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        public void WriteRead(byte[] writeBuffer, byte[] readBuffer)
        {
            WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, 0);
        }

        /// <summary>
        /// Writes an array of unsigned values to the SPI interface, and reads an array of unsigned values from the interface.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        public void WriteRead(ushort[] writeBuffer, ushort[] readBuffer)
        {
            WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, 0);
        }

        /// <summary>
        /// Writes an array of bytes to the SPI interface.  This is a synchronous call; it will not return until the bytes are written out.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        public void Write(params byte[] writeBuffer)
        {
            WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, null, 0, 0, 0);
        }

        /// <summary>
        /// Writes an array of unsigned values to the SPI interface. This is a synchronous call; it will not return until the bytes are written out.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        public void Write(params ushort[] writeBuffer)
        {
            WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, null, 0, 0, 0);
        }

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}