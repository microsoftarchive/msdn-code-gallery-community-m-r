////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Gadgeteer.Modules;
    using Microsoft.SPOT.Hardware;

    /// <summary>
    /// Provides access to the <see cref="AnalogOutput" /> interfaces on sockets.
    /// </summary>
    public static class AnalogOutputFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="AnalogOutput" /> for the given socket and pin number.
        /// </summary>
        /// <remarks>This automatically checks that the socket supports Type O, and reserves the pin.
        /// An exception will be thrown if there is a problem with these checks.</remarks>
        /// <param name="socket">The analog output capable socket.</param>
        /// <param name="pin">The pin to assign to the analog output.</param>
        /// <param name="module">The module using this analog output interface, which can be null if unspecified.</param>
        /// <returns>An instance of <see cref="AnalogOutput" /> for the given socket and pin number.</returns>
        public static AnalogOutput Create(Socket socket, Socket.Pin pin, Module module)
        {
            socket.EnsureTypeIsSupported('O', module);
            socket.ReservePin(pin, module);

            Cpu.AnalogOutputChannel channel = socket.AnalogOutput5;

            // native implementation is preferred to an indirected one
            if (channel == Cpu.AnalogOutputChannel.ANALOG_OUTPUT_NONE && socket.AnalogOutputIndirector != null)
                return socket.AnalogOutputIndirector(socket, pin, module);

            else
                return new NativeAnalogOutput(socket, pin, module, channel);
        }
    }
}
