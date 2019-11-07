////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Gadgeteer.Modules;
    using Microsoft.SPOT.Hardware;

    /// <summary>
    /// Provides access to the <see cref="PwmOutput" /> interfaces on sockets.
    /// </summary>
    public static class PwmOutputFactory
    {   
        /// <summary>
        /// Creates an instance of <see cref="PwmOutput" /> for the given socket and pin number.
        /// </summary>
        /// <remarks>This automatically checks that the socket supports Type P, and reserves the pin.
        /// An exception will be thrown if there is a problem with these checks.</remarks>
        /// <param name="socket">The socket that supports pulse width modulation (PWM) output.</param>
        /// <param name="pin">The pin on the socket that supports PWM.</param>
        /// <param name="invert">Whether to invert the output voltage.</param>
        /// <param name="module">The module using this PWM output interface, which can be null if unspecified.</param>
        /// <returns>An instance of <see cref="PwmOutput" /> for the given socket and pin number.</returns>
        public static PwmOutput Create(Socket socket, Socket.Pin pin, bool invert, Module module)
        {
            socket.EnsureTypeIsSupported('P', module);
            socket.ReservePin(pin, module);

            Cpu.PWMChannel channel = Cpu.PWMChannel.PWM_NONE;
            switch (pin)
            {
                case Socket.Pin.Seven:
                    channel = socket.PWM7;
                    break;

                case Socket.Pin.Eight:
                    channel = socket.PWM8;
                    break;

                case Socket.Pin.Nine:
                    channel = socket.PWM9;
                    break;
            }

            // native implementation is preferred to an indirected one
            if (channel == Cpu.PWMChannel.PWM_NONE && socket.PwmOutputIndirector != null)
                return socket.PwmOutputIndirector(socket, pin, invert, module);

            else
                return new NativePwmOutput(socket, pin, invert, module, channel);
        }
    }
}