////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using Gadgeteer.Modules;
    using Hardware = Microsoft.SPOT.Hardware;

    internal class NativeAnalogOutput : AnalogOutput
    {
        private Hardware.AnalogOutput _port;
        private Hardware.Cpu.AnalogOutputChannel _channel;
        private Socket _socket;

        public NativeAnalogOutput(Socket socket, Socket.Pin pin, Module module, Hardware.Cpu.AnalogOutputChannel channel)
        {
            if (channel == Hardware.Cpu.AnalogOutputChannel.ANALOG_OUTPUT_NONE)
            {
                Socket.InvalidSocketException.ThrowIfOutOfRange(pin, Socket.Pin.Five, Socket.Pin.Five, "AnalogOutput", module);

                // this is a mainboard error but should not happen since we check for this, but it doesnt hurt to double-check
                throw Socket.InvalidSocketException.FunctionalityException(socket, "AnalogOutput");
            }

            _channel = channel;
            _socket = socket;
        }

        public override bool IsActive
        {
            get { return _port != null; }
            set
            {
                if ((_port != null) == value)
                    return;

                if (value)
                {
                    _port = new Hardware.AnalogOutput(_channel, _socket.AnalogOutputScale, _socket.AnalogOutputOffset, _socket.AnalogOutputPrecisionInBits);
                }
                else
                {
                    _port.Dispose();
                    _port = null;
                }
            }
        }

        public override void WriteVoltage(double voltage)
        {
            IsActive = true;

            _port.Write(voltage);
        }

        public override void Dispose()
        {
            _port.Dispose();
            _port = null;
        }
    }
}
