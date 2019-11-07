////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using Gadgeteer.Modules;
    using Hardware = Microsoft.SPOT.Hardware;

    internal class NativeAnalogInput : AnalogInput
    {
        private Hardware.AnalogInput _port;
        private Hardware.Cpu.AnalogChannel _channel;
        private Socket _socket;

        public NativeAnalogInput(Socket socket, Socket.Pin pin, Module module, Hardware.Cpu.AnalogChannel channel)
        {
            if (channel == Hardware.Cpu.AnalogChannel.ANALOG_NONE)
            {
                Socket.InvalidSocketException.ThrowIfOutOfRange(pin, Socket.Pin.Three, Socket.Pin.Five, "AnalogInput", module);

                // this is a mainboard error that should not happen (we already check for it in SocketInterfaces.RegisterSocket) but just in case...
                throw Socket.InvalidSocketException.FunctionalityException(socket, "AnalogInput");
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
                    _port = new Hardware.AnalogInput(_channel, _socket.AnalogInputScale, _socket.AnalogInputOffset, _socket.AnalogInputPrecisionInBits);
                }
                else
                {
                    _port.Dispose();
                    _port = null;
                }
            }
        }

        public override double ReadVoltage()
        {
            IsActive = true;

            return _port.Read();
        }

        public override void Dispose()
        {
            _port.Dispose();
            _port = null;
        }
    }
}
