using MyEvents.Api.Client;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyEvents.Client.Organizer.Selectors
{
    /// <summary>
    /// Item template selection based on parent group.
    /// </summary>
    public class EventItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ComingSoonTemplate { get; set; }
        public DataTemplate AllEventsTemplate { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventItemTemplateSelector()
        {
            ComingSoonTemplate = null;
            AllEventsTemplate = null;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if ((item as EventDefinition).GroupNumber == 1)
                return ComingSoonTemplate;
            else
                return AllEventsTemplate;
        }
    }
}
