////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using Gadgeteer.Modules;
    using Hardware = Microsoft.SPOT.Hardware;

    internal class NativeInterruptInput : InterruptInput
    {
        private Hardware.InterruptPort _port;

        public NativeInterruptInput(Socket socket, Socket.Pin pin, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, InterruptMode interruptMode, Module module, Hardware.Cpu.Pin cpuPin)
        {
            if (cpuPin == Hardware.Cpu.Pin.GPIO_NONE)
            {
                // this is a mainboard error but should not happen since we check for this, but it doesnt hurt to double-check
                throw Socket.InvalidSocketException.FunctionalityException(socket, "InterruptInput");
            }

            _port = new Hardware.InterruptPort(cpuPin, glitchFilterMode == GlitchFilterMode.On, (Hardware.Port.ResistorMode)resistorMode, (Hardware.Port.InterruptMode)interruptMode);
        }

        public override bool Read()
        {
            return _port.Read();
        }

        protected override void OnInterruptFirstSubscribed()
        {
            _port.OnInterrupt += OnPortInterrupt;
        }

        protected override void OnInterruptLastUnsubscribed()
        {
            _port.OnInterrupt -= OnPortInterrupt;
        }

        private void OnPortInterrupt(uint data1, uint data2, DateTime time)
        {
            RaiseInterrupt(data2 > 0);
        }

        public override void Dispose()
        {
            _port.Dispose();
        }
    }
}
