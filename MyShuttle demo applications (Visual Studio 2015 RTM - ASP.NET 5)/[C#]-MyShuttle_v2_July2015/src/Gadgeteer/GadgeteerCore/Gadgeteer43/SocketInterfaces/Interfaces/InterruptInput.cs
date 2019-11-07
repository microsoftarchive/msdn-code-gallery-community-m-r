////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    /// <summary>
    /// Represents the delegate used for the <see cref="InterruptInput.Interrupt" /> event.
    /// </summary>
    /// <param name="sender">The <see cref="InterruptInput" /> object that raised the event.</param>
    /// <param name="value"><b>true</b> if the the value received from the interrupt is greater than zero; otherwise, <b>false</b>.</param>
    public delegate void InterruptEventHandler(InterruptInput sender, bool value);

    /// <summary>
    /// Provides the interrupt input functionality.
    /// </summary>
    public abstract class InterruptInput : System.IDisposable
    {
        /// <summary>
        /// Reads a Boolean value at the InterruptInput interface port input.
        /// </summary>
        /// <returns>A Boolean value that indicates the current value of the port as either 0 or 1).</returns>
        public abstract bool Read();

        /// <summary>
        /// Called when the first handler is subscribed to the <see cref="Interrupt" /> event.
        /// </summary>
        protected abstract void OnInterruptFirstSubscribed();
        /// <summary>
        /// Called when the last handler is unsubsrcibed from the <see cref="Interrupt" /> event.
        /// </summary>
        protected abstract void OnInterruptLastUnsubscribed();
        /// <summary>
        /// Raises the <see cref="Interrupt" /> event.
        /// </summary>
        /// <param name="value"><b>true</b> if the the value received from the interrupt is greater than zero; otherwise, <b>false</b>.</param>
        protected void RaiseInterrupt(bool value)
        {
            if (this.interrupt != null)
            {
                this.interrupt(this, value);
            }
        }

        private InterruptEventHandler interrupt;
        /// <summary>
        /// Raised when the <see cref="InterruptInput" /> interface detects an interrupt.
        /// </summary>
        public event InterruptEventHandler Interrupt
        {
            add
            {
                if (this.interrupt == null)
                {
                    OnInterruptFirstSubscribed();
                }

                this.interrupt += value;
            }
            remove
            {
                this.interrupt -= value;

                if (this.interrupt == null)
                {
                    this.OnInterruptLastUnsubscribed();
                }
            }
        }
        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}
