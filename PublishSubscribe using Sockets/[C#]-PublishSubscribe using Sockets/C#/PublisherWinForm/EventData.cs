using System;

namespace PublisherWinForm
{
    [Serializable]
    public class EventData
    {

        public String Name;
        public String Details;

        public EventData(String name, String details)
        {
            Name = name;
            Details = details;
        }

        /// <summary>
        /// In order to be serialized, an object must have a parameterless Constructor.
        /// </summary>
        public EventData()
        {
        }
    }
}
