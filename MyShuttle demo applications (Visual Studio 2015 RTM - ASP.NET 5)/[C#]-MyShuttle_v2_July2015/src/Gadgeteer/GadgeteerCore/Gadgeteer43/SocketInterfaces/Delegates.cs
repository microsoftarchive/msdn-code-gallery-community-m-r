////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Gadgeteer.Modules;

    /// <summary>
    /// Represents the method that provides indirected <see cref="AnalogInput" /> interface for a socket.
    /// </summary>
    /// <param name="socket">The socket.</param>
    /// <param name="pin">The analog input pin to use.</param>
    /// <param name="module">The module using the socket, which can be null if unspecified.</param>
    /// <returns>An <see cref="AnalogInput" /> interface.</returns>
    public delegate AnalogInput AnalogInputIndirector(Socket socket, Socket.Pin pin, Module module);
    /// <summary>
    /// Represents the method that provides indirected <see cref="AnalogOutput" /> interface for a socket.
    /// </summary>
    /// <param name="socket">The analog output capable socket.</param>
    /// <param name="pin">The pin to assign to the analog output.</param>
    /// <param name="module">The module using this analog output interface, which can be null if unspecified.</param>
    /// <returns>An <see cref="AnalogOutput" /> interface.</returns>
    public delegate AnalogOutput AnalogOutputIndirector(Socket socket, Socket.Pin pin, Module module);
    /// <summary>
    /// Represents the method that provides indirected <see cref="DigitalInput" /> interface for a socket.
    /// </summary>
    /// <param name="socket">The socket for the digital input interface.</param>
    /// <param name="pin">The pin used by the digital input interface.</param>
    /// <param name="glitchFilterMode">
    ///  A value from the <see cref="GlitchFilterMode" /> enumeration that specifies 
    ///  whether to enable the glitch filter on this digital input interface.
    /// </param>
    /// <param name="resistorMode">
    ///  A value from the <see cref="ResistorMode" /> enumeration that establishes a default state for the digital input interface. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
    /// </param>
    /// <param name="module">The module using this interface, which can be null if unspecified.</param>
    /// <returns>An <see cref="DigitalInput" /> interface.</returns>
    public delegate DigitalInput DigitalInputIndirector(Socket socket, Socket.Pin pin, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, Module module);
    /// <summary>
    /// Represents the method that provides indirected <see cref="DigitalIO" /> interface for a socket.
    /// </summary>
    /// <param name="socket">The socket for the digital input/output interface.</param>
    /// <param name="pin">The pin used by the digital input/output interface.</param>
    /// <param name="initialState">
    ///  The initial state to set on the digital input/output interface port.  
    ///  This value becomes effective as soon as the port is enabled as an output port.
    /// </param>
    /// <param name="glitchFilterMode">
    ///  A value from the <see cref="GlitchFilterMode" /> enumeration that specifies 
    ///  whether to enable the glitch filter on this digital input/output interface.
    /// </param>
    /// <param name="resistorMode">
    ///  A value from the <see cref="ResistorMode" /> enumeration that establishes a default state for the digital input/output interface. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
    /// </param>
    /// <param name="module">The module using this interface, which can be null if unspecified.</param>
    /// <returns>An <see cref="DigitalIO" /> interface.</returns>
    public delegate DigitalIO DigitalOIndirector(Socket socket, Socket.Pin pin, bool initialState, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, Module module);
    /// <summary>
    /// Represents the method that provides indirected <see cref="DigitalOutput" /> interface for a socket.
    /// </summary>
    /// <param name="socket">The socket for the digital output interface.</param>
    /// <param name="pin">The pin used by the digital output interface.</param>
    /// <param name="initialState">The initial state to place on the digital output interface port.</param>
    /// <param name="module">The module using this interface (which can be null if unspecified).</param>
    /// <returns>An <see cref="DigitalOutput" /> interface.</returns>
    public delegate DigitalOutput DigitalOutputIndirector(Socket socket, Socket.Pin pin, bool initialState, Module module);
    /// <summary>
    /// Represents the method that provides indirected <see cref="I2CBus" /> interface for a socket.
    /// </summary>
    /// <param name="address">The address for the I2C device.</param>
    /// <param name="clockRateKHz">The clock rate, in kHz, used when communicating with the I2C device.</param>
    /// <param name="socket">The socket for the I2C device interface.</param>
    /// <param name="sdaPin">The SDA pin for the I2C interface.</param>
    /// <param name="sclPin">The SCL pin for the I2C interface.</param>
    /// <param name="module">The module using this I2C interface, which can be null if unspecified.</param>
    /// <returns>An <see cref="I2CBus" /> interface.</returns>
    public delegate I2CBus I2CBusIndirector(Socket socket, Socket.Pin sdaPin, Socket.Pin sclPin, ushort address, int clockRateKHz, Module module);
    /// <summary>
    /// Represents the method that provides indirected <see cref="InterruptInput" /> interface for a socket.
    /// </summary>
    /// <param name="socket">The socket for the interrupt input interface.</param>
    /// <param name="pin">The pin used by the interrupt input interface.</param>
    /// <param name="glitchFilterMode">
    ///  A value from the <see cref="GlitchFilterMode" /> enumeration that specifies 
    ///  whether to enable the glitch filter on this interrupt input interface.
    /// </param>
    /// <param name="resistorMode">
    ///  A value from the <see cref="ResistorMode" /> enumeration that establishes a default state for the interrupt input interface. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
    /// </param>
    /// <param name="interruptMode">
    ///  A value from the <see cref="InterruptMode" /> enumeration that establishes the requisite conditions 
    ///  for the interface port to generate an interrupt.
    /// </param>
    /// <param name="module">The module using this interrupt input interface, which can be null if unspecified.</param>
    /// <returns>An <see cref="InterruptInput" /> interface.</returns>
    public delegate InterruptInput InterruptInputIndirector(Socket socket, Socket.Pin pin, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, InterruptMode interruptMode, Module module);
    /// <summary>
    /// Represents the method that provides indirected <see cref="PwmOutput" /> interface for a socket.
    /// </summary>
    /// <param name="socket">The socket that supports pulse width modulation (PWM) output.</param>
    /// <param name="pin">The pin on the socket that supports PWM.</param>
    /// <param name="invert">Whether to invert the output voltage.</param>
    /// <param name="module">The module using this PWM output interface, which can be null if unspecified.</param>
    /// <returns>An <see cref="PwmOutput" /> interface.</returns>
    public delegate PwmOutput PwmOutputIndirector(Socket socket, Socket.Pin pin, bool invert, Module module);
    /// <summary>
    /// Represents the method that provides indirected <see cref="Serial" /> interface for a socket.
    /// </summary>
    /// <param name="baudRate">The baud rate for the serial port.</param>
    /// <param name="parity">A value from the <see cref="SerialParity" /> enumeration that specifies 
    /// the parity for the port.</param>
    /// <param name="stopBits">A value from the <see cref="SerialStopBits" /> enumeration that specifies 
    /// the stop bits for the port.</param>
    /// <param name="dataBits">The number of data bits.</param>
    /// <param name="socket">The socket for this serial interface.</param>
    /// <param name="hardwareFlowControlRequirement">Specifies whether the module must use hardware flow control, will use hardware flow control if available, or does not use hardware flow control.</param>
    /// <param name="module">The module using this interface (which can be null if unspecified).</param>
    /// <returns>An <see cref="Serial" /> interface.</returns>
    public delegate Serial SerialIndirector(Socket socket, int baudRate, SerialParity parity, SerialStopBits stopBits, int dataBits, HardwareFlowControl hardwareFlowControlRequirement, Module module);
    /// <summary>
    /// Represents the method that provides indirected <see cref="Spi" /> interface for a socket.
    /// </summary>
    /// <param name="socket">The socket for this SPI interface.</param>
    /// <param name="spiConfiguration">The SPI configuration.</param>
    /// <param name="sharingMode">The sharing mode.</param>
    /// <param name="chipSelectSocket">The chip select socket to use.</param>
    /// <param name="chipSelectSocketPin">The chip select pin to use on the chip select socket.</param>
    /// <param name="busySocket">The busy socket to use.</param>
    /// <param name="busySocketPin">The busy pin to use on the busy socket.</param>
    /// <param name="module">The module using this interface (which can be null if unspecified).</param>
    /// <returns>An <see cref="Spi" /> interface.</returns>
    public delegate Spi SpiIndirector(Socket socket, SpiConfiguration spiConfiguration, SpiSharing sharingMode, Socket chipSelectSocket, Socket.Pin chipSelectSocketPin, Socket busySocket, Socket.Pin busySocketPin, Module module);
}