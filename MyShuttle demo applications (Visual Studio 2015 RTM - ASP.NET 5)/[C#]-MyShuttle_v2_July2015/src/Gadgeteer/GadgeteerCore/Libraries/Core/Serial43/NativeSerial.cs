//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using Gadgeteer.Modules;
    using Hardware = System.IO.Ports;

    internal class NativeSerial : Serial
    {
        private Hardware.SerialPort _port;
        private bool _hardwareFlowControl;

        public NativeSerial(Socket socket, int baudRate, SerialParity parity, SerialStopBits stopBits, int dataBits, HardwareFlowControl hwFlowRequirement, Module module, string portName, bool hwFlowSupported)
        {
            if (portName == null || portName == "")
            {
                // this is a mainboard error that should not happen (we already check for it in SocketInterfaces.RegisterSocket) but just in case...
                throw Socket.InvalidSocketException.FunctionalityException(socket, "Serial");
            }

            _port = new Hardware.SerialPort(portName, baudRate, (Hardware.Parity)parity, dataBits, (Hardware.StopBits)stopBits);

            if ((hwFlowRequirement != SocketInterfaces.HardwareFlowControl.NotRequired) && hwFlowSupported)
            {
                _port.Handshake = Hardware.Handshake.RequestToSend;
                _hardwareFlowControl = true;
            }

            try
            {
                this.ReadTimeout = System.Threading.Timeout.Infinite;
                this.WriteTimeout = System.Threading.Timeout.Infinite;
            }
            catch { }
        }

        public override string PortName
        {
            get { return _port.PortName; }
        }

        public override int BaudRate
        {
            get { return _port.BaudRate; }
            set { _port.BaudRate = value; }
        }

        public override SerialParity Parity
        {
            get { return (SerialParity)_port.Parity; }
            set { _port.Parity = (Hardware.Parity)value; }
        }

        public override SerialStopBits StopBits
        {
            get { return (SerialStopBits)_port.StopBits; }
            set { _port.StopBits = (Hardware.StopBits)value; }
        }

        public override int DataBits
        {
            get { return _port.DataBits; }
            set { _port.DataBits = value; }
        }

        public override bool IsUsingHardwareFlowControl
        {
            get { return _hardwareFlowControl; }
        }

        public override void Open()
        {
            if (!_port.IsOpen)
                _port.Open();
        }

        public override bool IsOpen
        {
            get { return _port.IsOpen; }
        }

        public override void Close()
        {
            _port.Close();
        }

        public override void Flush()
        {
            _port.Flush();
        }

        public override int ReadTimeout
        {
            get { return _port.ReadTimeout; }
            set { _port.ReadTimeout = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _port.Read(buffer, offset, count);
        }

        public override void DiscardInBuffer()
        {
            _port.DiscardInBuffer();
        }

        public override int BytesToRead
        {
            get { return _port.BytesToRead; }
        }

        public override int WriteTimeout
        {
            get { return _port.WriteTimeout; }
            set { _port.WriteTimeout = value; }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _port.Write(buffer, offset, count);
        }

        public override void DiscardOutBuffer()
        {
            _port.DiscardOutBuffer();
        }

        public override int BytesToWrite
        {
            get { return _port.BytesToWrite; }
        }

        protected override void OnDataReceivedFirstSubscribed()
        {
            _port.DataReceived += OnPortDataReceived;
        }

        protected override void OnDataReceivedLastUnsubscribed()
        {
            _port.DataReceived -= OnPortDataReceived;
        }

        private void OnPortDataReceived(object sender, Hardware.SerialDataReceivedEventArgs e)
        {
            if (e.EventType == Hardware.SerialData.Chars)
                RaiseDataReceived();
        }
    }
}