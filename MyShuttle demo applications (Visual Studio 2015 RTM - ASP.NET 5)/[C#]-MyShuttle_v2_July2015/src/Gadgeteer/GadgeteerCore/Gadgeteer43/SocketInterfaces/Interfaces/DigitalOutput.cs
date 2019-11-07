////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    /// <summary>
    /// Provides the digital output functionality.
    /// </summary>
    public abstract class DigitalOutput : System.IDisposable
    {
        /// <summary>
        /// Writes a value to the interface port output. 
        /// </summary>
        /// <param name="state">The value to be written to the port output.</param>
        public abstract void Write(bool state);
        /// <summary>
        /// Reads a Boolean value at the interface port input.
        /// </summary>
        /// <returns>The current value of the port (either 0 or 1).</returns>
        public abstract bool Read();

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}
