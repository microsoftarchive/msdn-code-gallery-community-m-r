////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    /// <summary>
    /// Provides the analog output functionality.
    /// </summary>
    public abstract class AnalogOutput : System.IDisposable
    {
        /// <summary>
        /// Gets or sets the active state of the analog output.
        /// </summary>
        public abstract bool IsActive { get; set; }
        /// <summary>
        /// Sets the voltage of the analog output.
        /// </summary>
        /// <param name="voltage">A double value that represents the voltage to which the analogue output will be set.</param>
        public abstract void WriteVoltage(double voltage);
        /// <summary>
        /// Sets the voltage of the analog output as a proportion from 0.0 to 1.0 of the maximum value (3.3V).
        /// </summary>
        /// <param name="proportion">The voltage proportion from 0.0-1.0 to set.</param>
        public virtual void WriteProportion(double proportion)
        {
            WriteVoltage(proportion * 3.3);
        }

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}
