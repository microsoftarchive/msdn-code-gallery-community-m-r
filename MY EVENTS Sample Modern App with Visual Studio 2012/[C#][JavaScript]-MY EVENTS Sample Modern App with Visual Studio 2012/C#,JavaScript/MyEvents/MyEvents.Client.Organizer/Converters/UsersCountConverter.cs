using System;
using System.Collections.Generic;
using System.Linq;
using MyEvents.Api.Client;
using Windows.UI.Xaml.Data;

namespace MyEvents.Client.Organizer.Converters
{
    public class UsersCountConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                List<RegisteredUser> userList = value as List<RegisteredUser>;
                return userList.Count();
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
