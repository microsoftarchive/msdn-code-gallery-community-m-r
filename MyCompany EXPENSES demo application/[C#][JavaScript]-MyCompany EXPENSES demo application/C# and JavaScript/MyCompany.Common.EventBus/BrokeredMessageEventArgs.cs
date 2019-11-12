namespace MyCompany.Common.EventBus
{
    using System;
    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    /// Brokered Message Event Args
    /// </summary>
    public class BrokeredMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Brokered Message
        /// </summary>
        public BrokeredMessage BrokeredMessage { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public BrokeredMessageEventArgs(BrokeredMessage message)
        {
            BrokeredMessage = message;
        }
    }
}
