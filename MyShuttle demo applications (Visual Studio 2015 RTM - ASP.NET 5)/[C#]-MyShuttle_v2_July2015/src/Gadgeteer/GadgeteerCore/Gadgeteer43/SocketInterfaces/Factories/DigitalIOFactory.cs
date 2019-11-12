////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Gadgeteer.Modules;
    using Microsoft.SPOT.Hardware;

    /// <summary>
    /// Provides access to the <see cref="DigitalIO" /> interfaces on sockets.
    /// </summary>
    public static class DigitalIOFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="DigitalIO" /> for the given socket and pin number.
        /// </summary>
        /// <param name="socket">The socket for the digital input/output interface.</param>
        /// <param name="pin">The pin used by the digital input/output interface.</param>
        /// <param name="initialState">
        ///  The initial state to set on the digital input/output interface port.  
        ///  This value becomes effective as soon as the port is enabled as an output port.
        /// </param>
        /// <param name="glitchFilterMode">
        ///  A value from the <see cref="GlitchFilterMode"/> enumeration that specifies 
        ///  whether to enable the glitch filter on this digital input/output interface.
        /// </param>
        /// <param name="resistorMode">
        ///  A value from the <see cref="ResistorMode"/> enumeration that establishes a default state for the digital input/output interface. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
        /// </param>
        /// <param name="module">The module using this interface, which can be null if unspecified.</param>
        /// <returns>An instance of <see cref="DigitalIO" /> for the given socket and pin number.</returns>
        public static DigitalIO Create(Socket socket, Socket.Pin pin, bool initialState, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, Module module)
        {
            Cpu.Pin reservedPin = socket.ReservePin(pin, module);

            // native implementation is preferred to an indirected one
            if (reservedPin == Cpu.Pin.GPIO_NONE && socket.DigitalIOIndirector != null)
                return socket.DigitalIOIndirector(socket, pin, initialState, glitchFilterMode, resistorMode, module);

            else
                return new NativeDigitalIO(socket, pin, initialState, glitchFilterMode, resistorMode, module, reservedPin);        
        }
    }
}
