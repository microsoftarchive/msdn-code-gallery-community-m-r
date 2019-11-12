////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Port = Microsoft.SPOT.Hardware.Port;

    /// <summary>
    /// Specifies the resistor modes for various ports.  N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
    /// </summary>
    /// <remarks>
    /// <para>
    ///  A value from this enumeration can be used when creating the following types of interfaces:
    /// </para>
    /// <list type="bullet">
    ///  <item><see cref="DigitalInput"/></item>
    ///  <item><see cref="DigitalIO"/></item>
    ///  <item><see cref="InterruptInput"/></item>
    /// </list>
    /// </remarks>
    public enum ResistorMode
    {
        /// <summary>
        /// A value that disables the resistor functionality. 
        /// </summary>
        Disabled = Port.ResistorMode.Disabled,
        /// <summary>
        /// A value that enables the resistor functionality in pull-up mode. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
        /// </summary>
        PullUp = Port.ResistorMode.PullUp,
        /// <summary>
        /// A value that enables the resistor functionality in pull-down mode. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
        /// </summary>
        PullDown = Port.ResistorMode.PullDown
    }

    /// <summary>
    /// Provides an enumeration of the values used to set the port interrupt mode. 
    /// </summary>
    /// <remarks>
    /// <para>
    ///  A value from this enumeration can be used when creating the following types of interfaces:
    /// </para>
    /// <list type="bullet">
    ///  <item><see cref="InterruptInput"/></item>
    /// </list>
    /// </remarks>
    public enum InterruptMode
    {
        /// <summary>
        /// A value that sets the port so that its interrupt is triggered on the rising edge.
        /// </summary>
        RisingEdge = Port.InterruptMode.InterruptEdgeHigh,
        /// <summary>
        /// A value that sets the port so that its interrupt is triggered when the input level is low.
        /// </summary>
        FallingEdge = Port.InterruptMode.InterruptEdgeLow,
        /// <summary>
        /// A value that sets the port so that its interrupt is triggered on both the rising and falling edges.
        /// </summary>
        RisingAndFallingEdge = Port.InterruptMode.InterruptEdgeBoth
    }

    /// <summary>
    /// Provides an enumeration of values used to set glitch filter mode on or off.
    /// </summary>
    /// <remarks>
    /// <para>
    ///  A value from this enumeration can be used when creating the following types of interfaces:
    /// </para>
    /// <list type="bullet">
    ///  <item><see cref="DigitalInput"/></item>
    ///  <item><see cref=" DigitalIO"/></item>
    ///  <item><see cref="InterruptInput"/></item>
    /// </list>
    /// </remarks>
    public enum GlitchFilterMode
    {
        /// <summary>
        /// Glitch filter mode is on.
        /// </summary>
        On,
        /// <summary>
        /// Glitch filter mode is off.
        /// </summary>
        Off
    }

    /// <summary>
    /// Represents the possible modes of a digital input/output interface.
    /// </summary>
    public enum IOMode
    {
        /// <summary>
        /// The interface is set for input operations.
        /// </summary>
        Input,
        /// <summary>
        /// The interface is set for output operations.
        /// </summary>
        Output
    }

    /// <summary>
    /// Provides scale factor values for duration and period.
    /// </summary>
    public enum PwmScaleFactor : uint
    {
        /// <summary>
        /// Milliseconds.
        /// </summary>
        Milliseconds = 1000,
        /// <summary>
        /// Microseconds.
        /// </summary>
        Microseconds = Milliseconds * 1000,
        /// <summary>
        /// Nanoseconds.
        /// </summary>
        Nanoseconds = Microseconds * 1000
    }

    /// <summary>
    /// Specifies the parity bit for a <see cref="Serial" /> interface. 
    /// </summary>
    /// <remarks>
    /// <para>
    ///  Use this enumeration when setting the <see cref="SerialParity" /> property for a serial port connection.
    /// </para>
    /// <para>
    ///  Parity is an error-checking procedure in which the number of 1s must always be the same—either even or odd—for each 
    ///  group of bits that is transmitted without error. In modem-to-modem communications, parity is often one of the parameters 
    ///  that must be agreed upon by sending parties and receiving parties before transmission can take place.
    /// </para>
    /// </remarks>
    public enum SerialParity
    {
        /// <summary>
        /// Sets the parity bit so that the count of bits set is an even number.
        /// </summary>
        Even = 2,
        /// <summary>
        /// Sets the parity bit so that the count of bits set is an odd number.
        /// </summary>
        Odd = 1,
        /// <summary>
        /// Leaves the parity bit set to 1.
        /// </summary>
        Mark = 3,
        /// <summary>
        /// Leaves the parity bit set to 0.
        /// </summary>
        Space = 4,
        /// <summary>
        /// No parity check occurs.
        /// </summary>
        None = 0
    }


    /// <summary>
    /// Specifies the number of stop bits used on the <see cref="Serial" /> object. 
    /// </summary>
    /// <remarks>
    /// <para>
    ///  This enumeration specifies the number of stop bits to use. Stop bits separate each unit of 
    ///  data on an asynchronous serial connection. 
    ///  They are also sent continuously when no data is available for transmission.
    /// </para>
    /// <para>
    /// The <b>None</b> option is not supported. Setting the <see cref="SerialStopBits" /> property 
    /// to <b>None</b> raises an ArgumentOutOfRangeException.
    /// </para>
    /// </remarks>
    public enum SerialStopBits
    {
        /// <summary>
        /// No stop bits are used. This value is not supported. Setting the <see cref="SerialStopBits" /> property 
        /// to <b>None</b> raises an ArgumentOutOfRangeException.
        /// </summary>
        None = 0,
        /// <summary>
        /// One stop bit is used.
        /// </summary>
        One = 1,
        /// <summary>
        /// 1.5 stop bits are used.
        /// </summary>
        OnePointFive = 3,
        /// <summary>
        /// Two stop bit are used.
        /// </summary>
        Two = 2
    }

    /// <summary>
    /// Specifies whether the <see cref="Serial" /> module requires hardware flow control. 
    /// </summary>
    public enum HardwareFlowControl
    {
        /// <summary>
        /// The module does not require hardware flow control.
        /// </summary>
        NotRequired,

        /// <summary>
        /// The module will use hardware flow control if available.
        /// </summary>
        UseIfAvailable,

        /// <summary>
        /// The module must have hardware flow control and will not function without it.
        /// </summary>
        Required
    }

    /// <summary>
    /// This specifies possible options for handling length errors, i.e. when the specified number of bytes cannot be read or written.
    /// Even if these are suppressed, exceptions may still be thrown for bus errors, e.g. if there is a timeout because the clock line is held low too long.
    /// </summary>
    public enum ErrorBehavior
    {
        /// <summary>
        /// Throw an exception if the right number of bytes is not written/read.
        /// </summary>
        ThrowException,

        /// <summary>
        /// Suppress exceptions if the right number of bytes is not written/read.
        /// </summary>
        SuppressException
    }

    /// <summary>
    /// Represents the configuration for an <see cref="Spi" /> interface.
    /// </summary>
    public class SpiConfiguration
    {
        /// <summary>
        /// The chip select active state (read-only).
        /// </summary>
        /// <value>
        ///  The active state for the chip-select port. If <b>true</b>, the chip-select port will be 
        ///  set to high when accessing the chip; 
        ///  if <b>false</b>, the chip-select port will be set to low when accessing the chip.
        /// </value>
        public readonly bool IsChipSelectActiveHigh;

        /// <summary>
        /// The chip-select setup time (read-only).
        /// </summary>
        /// <value>
        ///  The amount of time (in milliseconds) that will elapse between the time at which the device is selected and the time at which 
        ///  the clock and the clock data transmission will start.
        /// </value>
        public readonly uint ChipSelectSetupTime;

        /// <summary>
        /// The chip-select hold time (read-only).
        /// </summary>
        /// <value>
        ///  The amount of time (in milliseconds) that the chip-select port must remain in the active state before the device is unselected, 
        ///  or the amount of time (in milliseconds) that the chip-select will remain in the active state after the data read/write transaction 
        ///  has been completed.
        /// </value>
        public readonly uint ChipSelectHoldTime;

        /// <summary>
        /// The clock idle state (read-only).
        /// </summary>
        /// <value>
        ///  The idle state of the clock. If <b>true</b>, the SPI clock signal will be set to high while the device is idle; 
        ///  if <b>false</b>, the serial peripheral interface (SPI) clock signal will be set to low while the device is idle. 
        ///  The idle state occurs whenever the chip is not selected.
        /// </value>
        public readonly bool IsClockIdleHigh;

        /// <summary>
        /// Gets the sampling clock edge (read-only).
        /// </summary>
        /// <value>
        ///  The sampling clock edge. If <b>true</b>, data is sampled on the serial peripheral interface (SPI) clock rising edge; 
        ///  if <b>false</b>, the data is sampled on the SPI clock falling edge.
        /// </value>
        public readonly bool IsClockSamplingEdgeRising;

        /// <summary>
        /// Gets the clock rate, in kHz (read-only).
        /// </summary>
        public readonly uint ClockRateKHz;

        /// <summary>
        /// The busy pin active state (read-only).
        /// </summary>
        /// <value>
        ///  The active state for the busy port. If <b>true</b>, the SPI will wait while the busy signal is 
        ///  held high during transactions; if <b>false</b>, it will wait while the busy signal is held low.
        /// </value>
        public readonly bool IsBusyActiveHigh;


        // Note: A constructor summary is auto-generated by the doc builder.
        /// <summary></summary>
        /// <param name="chipSelectActiveHigh">
        ///  The active state for the chip-select port. If <b>true</b>, the chip-select port will be set to high when 
        ///  accessing the chip; if <b>false</b>, the chip select port will be set to low when accessing the chip.
        /// </param>
        /// <param name="chipSelectSetupTime">
        ///  The amount of time (in milliseconds) that will elapse between the time at which the device is selected and the time at which 
        ///  the clock and the clock data transmission will start.
        /// </param>
        /// <param name="chipSelectHoldTime">
        ///  The amount of time (in milliseconds) that the chip select port must remain in the active state before the device is unselected, 
        ///  or the amount of time (in milliseconds) that the chip select will remain in the active state after the data read/write transaction 
        ///  has been completed.
        /// </param>
        /// <param name="clockIdleHigh">
        ///  The idle state of the clock. If <b>true</b>, the serial peripheral interface (SPI) clock signal will be set to high while the device is idle; 
        ///  if <b>false</b>, the SPI clock signal will be set to low while the device is idle. The idle state occurs whenever 
        ///  the chip is not selected.
        /// </param>
        /// <param name="clockSamplingEdgeRising">
        ///  The sampling clock edge. If <b>true</b>, data is sampled on the SPI clock rising edge; 
        ///  if <b>false</b>, the data is sampled on the SPI clock falling edge.
        /// </param>
        /// <param name="clockRateKHz">The SPI clock rate in kHz.</param>
        /// <param name="busyActiveHigh">
        ///  The active state for the busy port. If <b>true</b>, the SPI will wait while the busy signal is 
        ///  held high during transactions; if <b>false</b>, it will wait while the busy signal is held low.
        /// </param>
        public SpiConfiguration(bool chipSelectActiveHigh, uint chipSelectSetupTime, uint chipSelectHoldTime, bool clockIdleHigh, bool clockSamplingEdgeRising, uint clockRateKHz, bool busyActiveHigh = false)
        {
            this.IsChipSelectActiveHigh = chipSelectActiveHigh;
            this.ChipSelectSetupTime = chipSelectSetupTime;
            this.ChipSelectHoldTime = chipSelectHoldTime;
            this.IsClockIdleHigh = clockIdleHigh;
            this.IsClockSamplingEdgeRising = clockSamplingEdgeRising;
            this.ClockRateKHz = clockRateKHz;
            this.IsBusyActiveHigh = busyActiveHigh;
        }
    }

    /// <summary>
    /// Provides values to specify the sharing mode for an SPI instance.
    /// </summary>
    public enum SpiSharing
    {
        /// <summary>
        /// Exclusive, no sharing allowed.
        /// </summary>
        Exclusive,
        /// <summary>
        /// Sharing is allowed.
        /// </summary>
        Shared,
        /// <summary>
        /// No more interfaces may share.
        /// </summary>
        NoMoreAllowed
    }
}