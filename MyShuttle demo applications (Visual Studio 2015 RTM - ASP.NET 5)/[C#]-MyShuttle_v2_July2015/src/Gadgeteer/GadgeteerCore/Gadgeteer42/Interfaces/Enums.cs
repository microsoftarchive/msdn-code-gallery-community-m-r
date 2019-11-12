////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.Interfaces
{
    using Microsoft.SPOT;
    using Microsoft.SPOT.Hardware;
    using System;

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
}