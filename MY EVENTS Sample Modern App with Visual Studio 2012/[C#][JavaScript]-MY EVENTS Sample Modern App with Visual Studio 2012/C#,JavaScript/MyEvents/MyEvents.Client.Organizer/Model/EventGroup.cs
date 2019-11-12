using System.Collections.ObjectModel;
using MyEvents.Api.Client;

namespace MyEvents.Client.Organizer.Model
{
    /// <summary>
    /// Event grouping based on date.
    /// </summary>
    public class EventGroup
    {
        /// <summary>
        /// constructor
        /// </summary>
        public EventGroup()
        {

        }
        public int GroupIndex { get; set; }

        public string GroupTitle { get; set; }

        public ObservableCollection<EventDefinition> Items { get; set; }
    }

    
}
