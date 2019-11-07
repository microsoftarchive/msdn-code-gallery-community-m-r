////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using Gadgeteer.Modules;
    using Hardware = Microsoft.SPOT.Hardware;

    internal class NativeDigitalIO : DigitalIO
    {
        private Hardware.TristatePort _port;
        private IOMode _mode;

        public NativeDigitalIO(Socket socket, Socket.Pin pin, bool initialState, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, Module module, Hardware.Cpu.Pin cpuPin)
        {
            if (cpuPin == Hardware.Cpu.Pin.GPIO_NONE)
            {
                // this is a mainboard error but should not happen since we check for this, but it doesnt hurt to double-check
                throw Socket.InvalidSocketException.FunctionalityException(socket, "DigitalIO");
            }

            _port = new Hardware.TristatePort(cpuPin, initialState, glitchFilterMode == GlitchFilterMode.On, (Hardware.Port.ResistorMode)resistorMode);
        }

        public override IOMode Mode
        {
            get { return _mode; }
            set
            {
                switch (value)
                {
                    case IOMode.Input:
                        if (_port.Active)
                            _port.Active = false;

                        break;

                    case IOMode.Output:
                        if (!_port.Active)
                            _port.Active = true;

                        break;
                }

                _mode = value;
            }
        }

        public override void Write(bool state)
        {
            Mode = IOMode.Output;

            _port.Write(state);
        }

        public override bool Read()
        {
            Mode = IOMode.Input;

            return _port.Read();
        }

        public override void Dispose()
        {
            _port.Dispose();
        }
    }
}
