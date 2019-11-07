using System;

namespace SubscriberWinForm
{
    [Serializable]
    public class EventData
    {

        public String Name;
        
        /// <summary>
        /// The Details are initially null.
        /// </summary>
        public String Details = null;

        public EventData(String name)
        {
            Name = name;
            //Details = details;
        }

        /// <summary>
        /// In order to be serialized, an object must have a parameterless Constructor.
        /// </summary>
        public EventData()
        {
        }
    }
}
