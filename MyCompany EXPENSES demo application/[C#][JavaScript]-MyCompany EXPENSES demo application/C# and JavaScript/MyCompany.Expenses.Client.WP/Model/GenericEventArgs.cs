namespace MyCompany.Expenses.Client.WP.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic argument class for events
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventArgs<T> : EventArgs
    {
        private T eventData;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventData">Object data</param>
        public EventArgs(T eventData)
        {
            this.eventData = eventData;
        }

        /// <summary>
        /// Object data property
        /// </summary>
        public T EventData
        {
            get { return this.eventData; }
        }
    }
}
