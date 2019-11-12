////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Gadgeteer.Modules;
    using Microsoft.SPOT.Hardware;

    /// <summary>
    /// Provides access to the <see cref="DigitalOutput" /> interfaces on sockets.
    /// </summary>
    public static class DigitalOutputFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="DigitalOutput" /> for the given socket and pin number.
        /// </summary>
        /// <param name="socket">The socket for the digital output interface.</param>
        /// <param name="pin">The pin used by the digital output interface.</param>
        /// <param name="initialState">The initial state to place on the digital output interface port.</param>
        /// <param name="module">The module using this interface (which can be null if unspecified).</param>
        /// <returns>An instance of <see cref="DigitalOutput" /> for the given socket and pin number.</returns>
        public static DigitalOutput Create(Socket socket, Socket.Pin pin, bool initialState, Module module)
        {
            Cpu.Pin reservedPin = socket.ReservePin(pin, module);

            // native implementation is preferred to an indirected one
            if (reservedPin == Cpu.Pin.GPIO_NONE && socket.DigitalOutputIndirector != null)
                return socket.DigitalOutputIndirector(socket, pin, initialState, module);

            else
                return new NativeDigitalOutput(socket, pin, initialState, module, reservedPin);
        }
    }
}
