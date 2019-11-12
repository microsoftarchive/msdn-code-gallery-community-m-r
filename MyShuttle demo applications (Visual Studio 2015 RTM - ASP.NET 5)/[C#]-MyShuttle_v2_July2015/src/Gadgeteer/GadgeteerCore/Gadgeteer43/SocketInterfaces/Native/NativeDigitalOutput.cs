////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using Gadgeteer.Modules;
    using Hardware = Microsoft.SPOT.Hardware;

    internal class NativeDigitalOutput : DigitalOutput
    {
        private Hardware.OutputPort _port;

        public NativeDigitalOutput(Socket socket, Socket.Pin pin, bool initialState, Module module, Hardware.Cpu.Pin cpuPin)
        {
            if (cpuPin == Hardware.Cpu.Pin.GPIO_NONE)
            {
                // this is a mainboard error but should not happen since we check for this, but it doesnt hurt to double-check
                throw Socket.InvalidSocketException.FunctionalityException(socket, "DigitalOutput");
            }

            _port = new Hardware.OutputPort(cpuPin, initialState);
        }

        public override void Write(bool state)
        {
            _port.Write(state);
        }

        public override bool Read()
        {
            return _port.Read();
        }

        public override void Dispose()
        {
            _port.Dispose();
        }
    }
}
