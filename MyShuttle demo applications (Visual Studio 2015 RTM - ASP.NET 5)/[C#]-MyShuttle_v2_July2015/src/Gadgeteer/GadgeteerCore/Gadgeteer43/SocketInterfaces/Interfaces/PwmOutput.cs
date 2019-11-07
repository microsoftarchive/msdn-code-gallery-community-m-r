////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    /// <summary>
    /// Provides the PWM output functionality.
    /// </summary>
    public abstract class PwmOutput : System.IDisposable
    {
        /// <summary>
        /// Gets or sets a Boolean value that indicates whether the PWM interface is active, <b>true</b> if active otherwise <b>false</b>.
        /// </summary>
        public abstract bool IsActive { get; set; }
        /// <summary>
        /// Sets the frequency and duty cycle of the <see cref="PwmOutput" /> interface and starts the PWM signal.
        /// </summary>
        /// <param name="frequency">Required frequency in Hertz.</param>
        /// <param name="dutyCycle">Duty cycle from 0-1.</param>
        public abstract void Set(double frequency, double dutyCycle);
        /// <summary>
        /// Sets the period and high time of the <see cref="PwmOutput" /> interface and starts the PWM signal.
        /// </summary>
        /// <param name="period">Period of the signal in units specified <paramref name="factor" />.</param>
        /// <param name="highTime">High time of the signal in units specified by <paramref name="factor" />.</param>
        /// <param name="factor">The units of <paramref name="period" /> and <paramref name="highTime" />.</param>
        public abstract void Set(uint period, uint highTime, PwmScaleFactor factor);

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}