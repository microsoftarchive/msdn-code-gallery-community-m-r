////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Gadgeteer.Modules;
    using Microsoft.SPOT.Hardware;

    /// <summary>
    /// Provides access to the <see cref="I2CBus" /> interfaces on sockets.
    /// </summary>
    public static class I2CBusFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="I2CBus" /> for the given socket.
        /// </summary>
        /// <remarks>This automatically checks that the socket supports Type I, and reserves the SDA and SCL pins.
        /// An exception will be thrown if there is a problem with these checks.</remarks>
        /// <param name="address">The address for the I2C device.</param>
        /// <param name="clockRateKhz">The clock rate, in kHz, used when communicating with the I2C device.</param>
        /// <param name="socket">The socket for this I2C device interface.</param>
        /// <param name="module">The module using this I2C interface, which can be null if unspecified.</param>
        /// <returns>An instance of <see cref="I2CBus" /> for the given socket.</returns>
        public static I2CBus Create(Socket socket, ushort address, int clockRateKhz, Module module)
        {
            return Create(socket, address, clockRateKhz, Socket.Pin.Eight, Socket.Pin.Nine, module);
        }
         
        /// <summary>
        /// Creates an instance of <see cref="I2CBus" /> for the given socket and pins.
        /// </summary>
        /// <remarks>This automatically checks that the socket supports Type I, and reserves the SDA and SCL pins.
        /// An exception will be thrown if there is a problem with these checks.</remarks>
        /// <param name="address">The address for the I2C device.</param>
        /// <param name="clockRateKhz">The clock rate, in kHz, used when communicating with the I2C device.</param>
        /// <param name="sdaPin">The SDA pin used by the I2C device.</param>
        /// <param name="sclPin">The SCL pin used by the I2C device.</param>
        /// <param name="socket">The socket for this I2C device interface.</param>
        /// <param name="module">The module using this I2C interface, which can be null if unspecified.</param>
        /// <returns>An instance of <see cref="I2CBus" /> for the given socket.</returns>
        public static I2CBus Create(Socket socket, ushort address, int clockRateKhz, Socket.Pin sdaPin, Socket.Pin sclPin, Module module)
        {
            // There is only one I²C module in .NET Micro Framework, so the NativeI2CBus would just go and use it
            // regardless of the requested pins, so we need to do the checks here instead.

            Cpu.Pin reservedSclPin = socket.ReservePin(sclPin, module);
            Cpu.Pin reservedSdaPin = socket.ReservePin(sdaPin, module);

            Cpu.Pin nativeSclPin, nativeSdaPin;
            HardwareProvider.HwProvider.GetI2CPins(out nativeSclPin, out nativeSdaPin);
                
            // native implementation is preferred to an indirected one
            if (reservedSdaPin == nativeSdaPin && reservedSclPin == nativeSclPin)
                return new NativeI2CBus(socket, address, clockRateKhz, module);

            else if (socket.I2CBusIndirector != null)
                return socket.I2CBusIndirector(socket, sdaPin, sclPin, address, clockRateKhz, module);

            else
                return new SoftwareI2CBus(socket, sdaPin, sclPin, address, clockRateKhz, module);
        }
    }
}