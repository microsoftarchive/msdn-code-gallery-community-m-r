////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using Gadgeteer.Modules;
    using Hardware = Microsoft.SPOT.Hardware;

    internal class NativeDigitalInput : DigitalInput
    {
        /// <summary>
        /// Represents the input port for the digital input.
        /// </summary>
        private Hardware.InputPort _port;

        public NativeDigitalInput(Socket socket, Socket.Pin pin, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, Module module, Hardware.Cpu.Pin cpuPin)
        {
            if (cpuPin == Hardware.Cpu.Pin.GPIO_NONE)
            {
                // this is a mainboard error but should not happen since we check for this, but it doesnt hurt to double-check
                throw Socket.InvalidSocketException.FunctionalityException(socket, "DigitalInput");
            }

            _port = new Hardware.InputPort(cpuPin, glitchFilterMode == GlitchFilterMode.On, (Hardware.Port.ResistorMode)resistorMode);
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
