////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using Gadgeteer.Modules;
    using Hardware = Microsoft.SPOT.Hardware;

    internal class NativePwmOutput : PwmOutput
    {
        private Hardware.PWM _port;
        private Hardware.Cpu.PWMChannel _channel;
        private Socket _socket;

        private bool _invert;
        private bool _started;

        public NativePwmOutput(Socket socket, Socket.Pin pin, bool invert, Module module, Hardware.Cpu.PWMChannel channel)
        {
            if (channel == Hardware.Cpu.PWMChannel.PWM_NONE)
            {
                Socket.InvalidSocketException.ThrowIfOutOfRange(pin, Socket.Pin.Seven, Socket.Pin.Nine, "PWM", module);

                // this is a mainboard error that should not happen (we already check for it in SocketInterfaces.RegisterSocket) but just in case...
                throw Socket.InvalidSocketException.FunctionalityException(socket, "PWM");
            }

            _channel = channel;
            _socket = socket;
            _invert = invert;
        }

        public override bool IsActive
        {
            get
            {
                return _port != null;
            }
            set
            {
                if ((_port != null) == value)
                    return;

                if (value)
                {
                    _port = new Hardware.PWM(_channel, 1, 0.5, _invert);
                    _started = false;
                }
                else
                {
                    if (_started)
                        _port.Stop();

                    _port.Dispose();
                    _port = null;
                }
            }
        }

        public override void Set(double frequency, double dutyCycle)
        {
            if (frequency < 0)
                throw new ArgumentException("frequency");

            if (dutyCycle < 0 || dutyCycle > 1)
                throw new ArgumentException("dutyCycle");

            if (_port == null)
            {
                _port = new Hardware.PWM(_channel, frequency, dutyCycle, _invert);

                _port.Start();
                _started = true;
            }
            else
            {
                if (_started)
                    _port.Stop();

                _port.Frequency = frequency;
                _port.DutyCycle = dutyCycle;

                _port.Start();
                _started = true;
            }
        }

        public override void Set(uint period, uint highTime, SocketInterfaces.PwmScaleFactor factor)
        {
            if (_port == null)
            {
                _port = new Hardware.PWM(_channel, period, highTime, (Hardware.PWM.ScaleFactor)factor, _invert);

                _port.Start();
                _started = true;
            }
            else
            {
                if (_started)
                    _port.Stop();

                _port.Scale = (Hardware.PWM.ScaleFactor)factor;
                _port.Period = period;
                _port.Duration = highTime;

                _port.Start();
                _started = true;
            }
        }

        public override void Dispose()
        {
            _port.Dispose();
        }
    }
}