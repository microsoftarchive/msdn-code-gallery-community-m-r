////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Gadgeteer.Modules;
    using Microsoft.SPOT.Hardware;

    // The factory classes must be separate so that they can be in separate assembly and using them will ensure adding a reference to that assembly.

    /// <summary>
    /// Provides access to the <see cref="AnalogInput" /> interfaces on sockets.
    /// </summary>
    public static class AnalogInputFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="AnalogInput" /> for the given socket and pin number.
        /// </summary>
        /// <remarks>This automatically checks that the socket supports Type A, and reserves the pin used.
        /// An exception will be thrown if there is a problem with these checks.</remarks>
        /// <param name="socket">The socket.</param>
        /// <param name="pin">The analog input pin to use.</param>
        /// <param name="module">The module using the socket, which can be null if unspecified.</param>
        /// <returns>An instance of <see cref="AnalogInput" /> for the given socket and pin number.</returns>
        public static AnalogInput Create(Socket socket, Socket.Pin pin, Module module)
        {
            socket.EnsureTypeIsSupported('A', module);
            socket.ReservePin(pin, module);

            Cpu.AnalogChannel channel = Cpu.AnalogChannel.ANALOG_NONE;
            switch (pin)
            {
                case Socket.Pin.Three:
                    channel = socket.AnalogInput3;
                    break;

                case Socket.Pin.Four:
                    channel = socket.AnalogInput4;
                    break;

                case Socket.Pin.Five:
                    channel = socket.AnalogInput5;
                    break;
            }

            // native implementation is preferred to an indirected one
            if (channel == Cpu.AnalogChannel.ANALOG_NONE && socket.AnalogInputIndirector != null)
                return socket.AnalogInputIndirector(socket, pin, module);

            else
                return new NativeAnalogInput(socket, pin, module, channel);
        }
    }
}
