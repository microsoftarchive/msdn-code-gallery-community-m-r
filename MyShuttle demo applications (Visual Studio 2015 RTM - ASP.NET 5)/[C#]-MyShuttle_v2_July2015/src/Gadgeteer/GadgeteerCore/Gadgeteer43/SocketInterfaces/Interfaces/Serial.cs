////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using System.Text;
    using System.Threading;
    using Microsoft.SPOT;
    using Math = System.Math;

    /// <summary>
    /// Represents the method to handle the <see cref="Serial.DataReceived" /> event.
    /// </summary>
    /// <param name="sender">The <see cref="Serial" /> object that raised the event.</param>
    public delegate void SerialDataReceivedEventHandler(Serial sender);

    /// <summary>
    /// Represents the method to handle the <see cref="Serial.LineReceived" /> event.
    /// </summary>
    /// <param name="sender">The <see cref="Serial" /> object that raised the event.</param>
    /// <param name="line">The received line of data as string.</param>
    public delegate void SerialLineReceivedEventHandler(Serial sender, string line);

    /// <summary>
    /// Provides the serial port functionality.
    /// </summary>
    public abstract class Serial : System.IDisposable
    {
        private string newLine = "\r\n";
        private Encoding encoding = Encoding.UTF8;
        private Decoder decoder;

        /// <summary>
        /// Gets the port name associated with this serial interface.
        /// </summary>
        public abstract string PortName { get; }
        /// <summary>
        /// Gets or sets the baud rate of this serial interface.
        /// </summary>
        public abstract int BaudRate { get; set; }
        /// <summary>
        /// Gets or sets the parity of this serial interface.
        /// </summary>
        public abstract SerialParity Parity { get; set; }
        /// <summary>
        /// Gets or sets the stop bits of this serial interface.
        /// </summary>
        public abstract SerialStopBits StopBits { get; set; }
        /// <summary>
        /// Gets or sets the number of data bits of this serial interface. 
        /// </summary>
        public abstract int DataBits { get; set; }
        /// <summary>
        /// Gets a Boolean value that indicates whether the serial interface is using hardware flow control.
        /// </summary>
        public abstract bool IsUsingHardwareFlowControl { get; }

        /// <summary>
        /// Gets or sets the new line string for this serial interface.
        /// </summary>
        public string NewLine
        {
            get
            {
                return this.newLine;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                else if (value.Length < 1)
                {
                    throw new ArgumentException();
                }
                else
                {
                    this.newLine = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the <see cref="Encoding" /> for this serial interface.
        /// </summary>
        public Encoding Encoding
        {
            get { return this.encoding; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.encoding = value;

                if (this.decoder != null)
                {
                    this.decoder = value.GetDecoder();
                }
            }
        }

        /// <summary>
        /// Opens a new serial port connection. 
        /// </summary>
        public abstract void Open();
        /// <summary>
        /// Gets a Boolean value indicating the open or closed status of the <see cref="Serial" /> object. 
        /// </summary>
        public abstract bool IsOpen { get; }
        /// <summary>
        /// Closes the port connection, and sets the <see cref="IsOpen" /> property to <b>false</b>.
        /// </summary>
        public abstract void Close();
        /// <summary>
        /// Sends any data waiting in the 'send' buffer and clears the buffer.
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish. 
        /// </summary>
        public abstract int ReadTimeout { get; set; }
        /// <summary>
        /// Reads a byte from the serial port.
        /// </summary>
        /// <returns>The byte read from the port as an integer value.</returns>
        public virtual int ReadByte()
        {
            byte[] data = new byte[1];

            if (this.Read(data, 0, 1) > 0)
            {
                return data[0];
            }

            return -1;
        }
        /// <summary>
        /// Reads a number of bytes from the serial port input buffer and writes those bytes 
        /// to a byte array at the specified offset. 
        /// </summary>
        /// <param name="buffer">The byte[] array to write the input to.</param>
        /// <param name="offset">The offset in the <paramref name="buffer"/> array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        public abstract int Read(byte[] buffer, int offset, int count);
        /// <summary>
        /// Discards data from the serial driver's 'receive' buffer. 
        /// </summary>
        public abstract void DiscardInBuffer();
        /// <summary>
        /// Gets the number of bytes of data in the 'receive' buffer. 
        /// </summary>
        public abstract int BytesToRead { get; }

        /// <summary>
        /// Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish. 
        /// </summary>
        public abstract int WriteTimeout { get; set; }
        /// <summary>
        /// Writes a variable number of bytes to the serial port using data from a buffer. 
        /// </summary>
        /// <param name="data">The data to write as a byte[] array.</param>
        public virtual void Write(params byte[] data)
        {
            if (data != null)
            {
                this.Write(data, 0, data.Length);
            }
        }
        /// <summary>
        /// Writes the specified text to the serial port.
        /// </summary>
        /// <param name="text">The text to write.</param>
        public virtual void Write(string text)
        {
            if (text != null)
            {
                byte[] data = this.Encoding.GetBytes(text);
                Write(data, 0, data.Length);
            }
        }
        /// <summary>
        /// Writes the specified text and the value of <see cref="NewLine" /> to the serial port.
        /// </summary>
        /// <param name="text">The text to write.</param>
        public virtual void WriteLine(string text)
        {
            this.Write(text + this.NewLine);
        }
        /// <summary>
        /// Writes a specified number of bytes to the serial port using data from a buffer. 
        /// </summary>
        /// <param name="buffer">The byte[] array that contains the data to write to the port.</param>
        /// <param name="offset">The zero-based byte offset of the <paramref name="buffer"/> parameter 
        /// at which to begin copying bytes to the port.</param>
        /// <param name="count">The number of bytes to write.</param>
        public abstract void Write(byte[] buffer, int offset, int count);
        /// <summary>
        /// Discards data from the serial driver's 'send' buffer. 
        /// </summary>
        public abstract void DiscardOutBuffer();
        /// <summary>
        /// Gets the number of bytes of data in the 'send' buffer. 
        /// </summary>
        public abstract int BytesToWrite { get; }

        /// <summary>
        /// Called when the first handler subscribes to the <see cref="DataReceived" /> event.
        /// </summary>
        protected abstract void OnDataReceivedFirstSubscribed();
        /// <summary>
        /// Called when the last handler unsubsrcibes from the <see cref="DataReceived" /> event.
        /// </summary>
        protected abstract void OnDataReceivedLastUnsubscribed();
        /// <summary>
        /// Raises the <see cref="DataReceived" /> event.
        /// </summary>
        protected void RaiseDataReceived()
        {
            SerialDataReceivedEventHandler handler = this.dataReceived;

            if (handler != null)
                handler(this);
        }
        private SerialDataReceivedEventHandler dataReceived;
        /// <summary>
        /// Raised when the serial port signals that data has been received. 
        /// </summary>
        public event SerialDataReceivedEventHandler DataReceived
        {
            add
            {
                if (this.dataReceived == null)
                {
                    OnDataReceivedFirstSubscribed();
                }

                this.dataReceived = (SerialDataReceivedEventHandler)Delegate.Combine(this.dataReceived, value);
            }
            remove
            {
                this.dataReceived = (SerialDataReceivedEventHandler)Delegate.Remove(this.dataReceived, value);

                if (this.dataReceived == null)
                {
                    this.OnDataReceivedLastUnsubscribed();
                }
            }
        }

        private Thread readLineThread;
        private ManualResetEvent readLineContinueEvent;
        private void ReadLineThread()
        {
            this.decoder = this.encoding.GetDecoder();
            byte[] byteBuffer = new byte[4]; // this.encoding.GetMaxByteCount(1); - not available on .NET Micro Framework yet, RFC 3629 restricts this to 4 for UTF-8.
            char[] charBuffer = new char[2];

            int readIndex = 0;
            int bytesUsed, charsUsed;
            bool completed = false;

            StringBuilder outcome = new StringBuilder();
            int totalRead = 0;

            while (true)
            {
                if (!this.readLineContinueEvent.WaitOne(0, false))
                {
                    // Bring the thread into clean state.
                    // This may result in loss of data if the line reading is stopped and restarted, but it is consistent in all cases.
                    // (currently the buffers are local only and we abort the thread completely if the read operation is pending during a stop request)

                    totalRead = 0;
                    readIndex = 0;
                    completed = false;
                }

                this.readLineContinueEvent.WaitOne();

                if (!this.IsOpen)
                {
                    Thread.Sleep(100);
                    continue;
                }

                int read = this.Read(byteBuffer, readIndex, 1);
                if (read > 0)
                {
                    totalRead += read;
                    int convertIndex = 0;

                    while (!completed)
                    {
                        this.decoder.Convert(
                            byteBuffer, convertIndex, totalRead - convertIndex,
                            charBuffer, 0, charBuffer.Length, false, out bytesUsed, out charsUsed, out completed);

                        if (charsUsed > 0) // great, write them down
                        {
                            outcome.Append(charBuffer, 0, charsUsed);

                            if (outcome.Length >= this.newLine.Length)
                            {
                                // Chance for the new line marker being in, however, some characters after it could sneak in as well,
                                // so we need to check for newLine ending on at least charsUsed last positions.
                                // The extra characters could also happen to be line markers, so we can't check from the end of the string.

                                int lastStart = 0;
                                for (int i = Math.Max(0, outcome.Length - Math.Max(this.newLine.Length, charsUsed)); i <= outcome.Length - this.newLine.Length; i++)
                                {
                                    if (outcome[i] == this.newLine[0])
                                    {
                                        bool newLineFound = true;

                                        for (int nl = 1; nl < this.newLine.Length; nl++)
                                        {
                                            if (outcome[i + nl] != this.newLine[nl])
                                            {
                                                newLineFound = false;
                                                break;
                                            }
                                        }

                                        if (newLineFound)
                                        {
                                            // the below is a workaround for a bug in NETMF stringbuilder class - used to be string line = outcome.ToString(lastStart, i - lastStart);
                                            string line = outcome.ToString().Substring(lastStart, i - lastStart);
                                            RaiseLineReceived(line);

                                            lastStart = i + this.newLine.Length;
                                            i = lastStart - 1;
                                        }
                                    }
                                }
                                if (lastStart > 0)
                                {
                                    outcome.Remove(0, lastStart);
                                }
                            }
                        } // charsUsed


                        if (bytesUsed > 0) // either completed or an invalid character
                        {
                            convertIndex += bytesUsed;
                        }
                        else
                        {
                            // in this case the decoder does not have any state, and we need more bytes to decode the character
                            if (convertIndex > 0)
                            {
                                // start from the beginning buffer, so that we can get all the required bytes
                                Array.Copy(byteBuffer, convertIndex, byteBuffer, 0, totalRead - convertIndex);
                                readIndex = 0;
                            }

                            if (++readIndex >= byteBuffer.Length)
                            {
                                // this not RFC 3629 UTF-8 decoder and we need a larger buffer
                                byte[] largerBuffer = new byte[byteBuffer.Length * 3 / 2];
                                byteBuffer.CopyTo(largerBuffer, 0);
                                byteBuffer = largerBuffer;
                            }

                            break;
                        }
                    } // completed

                    if (completed)
                    {
                        totalRead = 0;
                        readIndex = 0;
                        completed = false;
                    }
                } // read
            } // main loop
        }
        /// <summary>
        /// Called when the first handler subscribes to the <see cref="LineReceived" /> event.
        /// </summary>
        protected virtual void OnLineReceivedFirstSubscribed()
        {
            if (this.readLineThread == null)
            {
                if (this.readLineContinueEvent == null)
                    this.readLineContinueEvent = new ManualResetEvent(true);

                this.readLineThread = new Thread(ReadLineThread);
                this.readLineThread.Start();
            }

            this.readLineContinueEvent.Set();
        }
        /// <summary>
        /// Called when the last handled unsubscribes forom the <see cref="LineReceived" /> event.
        /// </summary>
        protected virtual void OnLineReceivedLastUnsubscribed()
        {
            if (this.readLineThread != null)
            {
                this.readLineContinueEvent.Reset();

                if ((this.readLineThread.ThreadState & ThreadState.WaitSleepJoin) == ThreadState.WaitSleepJoin)
                {
                    // Prevent the thread from stealing incoming data.
                    // Note that aborting the thread leaks it in .NET Micro Framework 4.3.

                    Thread thread = this.readLineThread;
                    this.readLineThread = null;
                    thread.Abort();
                }
            }
        }
        /// <summary>
        /// Raises the <see cref="LineReceived" /> event.
        /// </summary>
        /// <param name="line">The received line of data.</param>
        protected void RaiseLineReceived(string line)
        {
            SerialLineReceivedEventHandler handler = this.lineReceived;

            if (handler != null)
                handler(this, line);
        }
        private SerialLineReceivedEventHandler lineReceived;
        /// <summary>
        /// Raised when a complete line of data has been received.
        /// </summary>
        /// <remarks>
        /// Any incomplete lines read will be discarded.
        /// </remarks>
        public event SerialLineReceivedEventHandler LineReceived
        {
            add
            {
                if (lineReceived == null)
                {
                    OnLineReceivedFirstSubscribed();
                }

                this.lineReceived = (SerialLineReceivedEventHandler)Delegate.Combine(this.lineReceived, value);
            }
            remove
            {
                this.lineReceived = (SerialLineReceivedEventHandler)Delegate.Remove(this.lineReceived, value);

                if (this.lineReceived == null)
                {
                    OnLineReceivedLastUnsubscribed();
                }
            }
        }

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}