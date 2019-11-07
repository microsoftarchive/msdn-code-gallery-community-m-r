////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    /// <summary>
    /// Provides the analog input functionality.
    /// </summary>
    public abstract class AnalogInput : System.IDisposable
    {
        /// <summary>
        /// Gets or sets the active state of the analog input.
        /// </summary>
        public abstract bool IsActive { get; set; }
        /// <summary>
        /// Reads the current analog input value as a voltage between 0 and 3.3V.
        /// </summary>
        /// <returns>The current analog value in volts.</returns>
        public abstract double ReadVoltage();
        /// <summary>
        /// Reads the current analog input value as a proportion from 0.0 to 1.0 of the maximum value (3.3V).
        /// </summary>
        /// <returns>The analog input value from 0.0-1.0</returns>
        public virtual double ReadProportion()
        {
            return ReadVoltage() / 3.3;
        }

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}
