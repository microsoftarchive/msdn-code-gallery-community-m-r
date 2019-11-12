using System;
using System.Collections.ObjectModel;
using System.Linq;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.ViewModel;
using Windows.UI.Xaml.Data;
using GalaSoft.MvvmLight;

namespace MyEvents.Client.Organizer.Converters
{
    /// <summary>
    /// Return the events for the specified Room
    /// </summary>
    public class EventsByRoomConverter : IValueConverter
    {
        /// <summary>
        /// Return the specified room events
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ViewModelLocator locator = (ViewModelLocator)App.Current.Resources["Locator"];

            if (value != null && locator != null)
            {
                string roomNumber = value.ToString().ToLowerInvariant().Replace("room ", "");
                int number = 0;
                int.TryParse(roomNumber, out number);

                if (number != 0)
                {
                    //TODO: extract logic from converter to the viewModel?
                    if (!ViewModelBase.IsInDesignModeStatic) {
                        return Convert(locator, number, parameter);
                    }
                    else
                    {
                        return ConvertFake(locator, number, parameter);
                    }

                }
            }
            return new ObservableCollection<Session>();
        }

        /// <summary>
        /// not implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private object Convert(ViewModelLocator locator, int number, object parameter)
        {
            var eventDetailViewModel = locator.EventDetail as EventDetailViewModel;
            
            if (parameter != null && parameter.ToString() == "NoNulls")
            {
                return eventDetailViewModel.Event.Sessions.Where(s => s.RoomNumber == number);
            }
            ObservableCollection<Session> Sessions = new ObservableCollection<Session>();
            for (DateTime i = eventDetailViewModel.Event.StartTime; i < eventDetailViewModel.Event.EndTime; i = i.AddHours(1))
            {
                Session session = eventDetailViewModel.Event.Sessions.Where(s => s.RoomNumber == number && s.StartTime >= i && s.StartTime < i.AddHours(1)).FirstOrDefault();
                if (session == null)
                {
                    session = new Session() { Duration = 0, RoomNumber = number, StartTime = eventDetailViewModel.Event.StartTime };
                }
                Sessions.Add(session);
            }

            return Sessions;
        }

        private object ConvertFake(ViewModelLocator locator, int number, object parameter)
        {
            var eventDetailViewModel = locator.EventDetail as EventDetailViewModelFake;

            if (parameter != null && parameter.ToString() == "NoNulls")
            {
                return eventDetailViewModel.Event.Sessions.Where(s => s.RoomNumber == number);
            }
            ObservableCollection<Session> Sessions = new ObservableCollection<Session>();
            for (DateTime i = eventDetailViewModel.Event.StartTime; i < eventDetailViewModel.Event.EndTime; i = i.AddHours(1))
            {
                Session session = eventDetailViewModel.Event.Sessions.Where(s => s.RoomNumber == number && s.StartTime >= i && s.StartTime < i.AddHours(1)).FirstOrDefault();
                if (session == null)
                {
                    session = new Session() { Duration = 0, RoomNumber = number, StartTime = eventDetailViewModel.Event.StartTime };
                }
                Sessions.Add(session);
            }

            return Sessions;
        }
    }
}
