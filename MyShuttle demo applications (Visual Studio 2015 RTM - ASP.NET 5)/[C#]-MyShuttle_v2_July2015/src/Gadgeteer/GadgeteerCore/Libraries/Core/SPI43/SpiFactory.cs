////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Gadgeteer.Modules;
    using Microsoft.SPOT.Hardware;

    /// <summary>
    /// Provides access to the <see cref="Spi" /> interfaces on sockets.
    /// </summary>
    public static class SpiFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="Spi" /> for the given socket.
        /// </summary>
        /// <param name="socket">The <see cref="Socket"/> for the <see cref="Spi"/> interface.</param>
        /// <param name="spiConfiguration">The <see cref="SpiConfiguration"/> object for the <see cref="Spi"/> interface.</param>
        /// <param name="sharingMode">The <see cref="SpiSharing"/> mode of the <see cref="SPI"/> interface.</param>
        /// <param name="chipSelectSocket">The chip select <see cref="Socket"/> of the <see cref="Spi"/> interface. Can be null if not used.</param>
        /// <param name="chipSelectSocketPin">The chip select pin on <paramref name="chipSelectSocket"/>.</param>
        /// <param name="module">The <see cref="Module"/> that is connected to the <see cref="Spi"/> interface.</param>
        /// <returns></returns>
        public static Spi Create(Socket socket, SpiConfiguration spiConfiguration, SpiSharing sharingMode, Socket chipSelectSocket, Socket.Pin chipSelectSocketPin, Module module)
        {
            return Create(socket, spiConfiguration, sharingMode, chipSelectSocket, chipSelectSocketPin, null, Socket.Pin.None, module);
        }
        
        /// <summary>
        /// Creates an instance of <see cref="Spi" /> for the given socket.
        /// </summary>
        /// <param name="socket">The <see cref="Socket"/> for the <see cref="Spi"/> interface.</param>
        /// <param name="spiConfiguration">The <see cref="SpiConfiguration"/> object for the <see cref="Spi"/> interface.</param>
        /// <param name="sharingMode">The <see cref="SpiSharing"/> mode of the <see cref="SPI"/> interface.</param>
        /// <param name="chipSelectSocket">The chip select <see cref="Socket"/> of the <see cref="Spi"/> interface. Can be null if not used.</param>
        /// <param name="chipSelectSocketPin">The chip select pin on <paramref name="chipSelectSocket"/>.</param>
        /// <param name="busySocket">The busy <see cref="Socket"/> of the <see cref="Spi" /> interface. Can be null if not used.</param>
        /// <param name="busySocketPin">The busy pin to on the <paramref name="busySocket"/>.</param>
        /// <param name="module">The <see cref="Module"/> that is connected to the <see cref="Spi"/> interface.</param>
        /// <returns></returns>
        public static Spi Create(Socket socket, SpiConfiguration spiConfiguration, SpiSharing sharingMode, Socket chipSelectSocket, Socket.Pin chipSelectSocketPin, Socket busySocket, Socket.Pin busySocketPin, Module module)
        {
            socket.EnsureTypeIsSupported('S', module);

            Cpu.Pin reservedSelectPin = Socket.UnspecifiedPin;
            Cpu.Pin reservedBusyPin = Socket.UnspecifiedPin;

            if (chipSelectSocket != null)
                reservedSelectPin = chipSelectSocket.ReservePin(chipSelectSocketPin, module);

            if (busySocket != null)
                reservedBusyPin = busySocket.ReservePin(busySocketPin, module);

            // reserved pins will be: 
            //   UnspecifiedPin if no chip select/busy pin is used
            //   UnnumberedPin if a chip select/busy pin was used on an indirected (non-forwarded socket)
            //   a valid Cpu.Pin otherwise
            
            if (socket.SPIModule != Socket.SocketInterfaces.SPIMissing)
                return new NativeSpi(socket, spiConfiguration, sharingMode, reservedSelectPin, reservedBusyPin, module, socket.SPIModule);

            else
                return socket.SpiIndirector(socket, spiConfiguration, sharingMode, chipSelectSocket, chipSelectSocketPin, busySocket, busySocketPin, module);
        }
    }
}