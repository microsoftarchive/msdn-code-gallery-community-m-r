using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.ViewModel;

namespace MyEvents.Client.Organizer.Desktop.Converters
{
    /// <summary>
    /// Get events by room number
    /// </summary>
    public class EventsByRoomConverter : IValueConverter
    {
        /// <summary>
        /// Convert room to event
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ViewModelLocator loc = (ViewModelLocator)App.Current.Resources["Locator"];

            if (value != null && loc != null)
            {
                string roomNumber = value.ToString().ToLowerInvariant().Replace("room ", "");
                int number = 0;
                int.TryParse(roomNumber, out number);

                if (number != 0)
                {
                    ObservableCollection<Session> Sessions = new ObservableCollection<Session>();
                    for (DateTime i = loc.EventSchedule.Event.StartTime; i < loc.EventSchedule.Event.EndTime; i = i.AddHours(1))
                    {
                        Session session = loc.EventSchedule.Event.Sessions.Where(s => s.RoomNumber == number && s.StartTime >= i && s.StartTime < i.AddHours(1)).FirstOrDefault();
                        if (session == null)
                        {
                            session = new Session() { Duration = 0, RoomNumber = number, StartTime = loc.EventSchedule.Event.StartTime };
                        }
                        Sessions.Add(session);
                    }

                    return Sessions;
                }
            }
            return new ObservableCollection<Session>();
        }

        /// <summary>
        /// return room number by events.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
