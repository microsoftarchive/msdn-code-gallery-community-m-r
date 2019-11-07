////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    /// <summary>
    /// Provides the digital input and output functionality.
    /// </summary>
    public abstract class DigitalIO : System.IDisposable
    {
        /// <summary>
        /// Gets or sets the mode for the digital input/output interface.
        /// </summary>
        /// <value>
        /// <list type="bullet">
        /// <item><see cref="IOMode.Input" /> if the interface is currently set for input operations.</item>
        /// <item><see cref="IOMode.Output" /> if the interface is currently set for ouput operations.</item>
        /// </list>
        /// </value>
        public abstract IOMode Mode { get; set; }
        /// <summary>
        /// Sets the interface to <see cref="IOMode.Output" /> and writes the specified value.
        /// </summary>
        /// <param name="state"></param>
        public abstract void Write(bool state);
        /// <summary>
        /// Sets the interface to <see cref="IOMode.Input" /> and reads a value.
        /// </summary>
        /// <returns>A Boolean value read from the interface.</returns>
        public abstract bool Read();

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}
