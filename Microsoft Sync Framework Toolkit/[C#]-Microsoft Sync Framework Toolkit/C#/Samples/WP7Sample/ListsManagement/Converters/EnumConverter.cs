// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System.Windows.Data;
using System;
using ListsManagement.ViewModels;
using System.Linq;
namespace ListsManagement.Converters
{
    public class EnumConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (parameter.ToString() == "Status")
            {
                return SyncContextInstance.Context.StatusCollection.Where(e => e.ID == (int)value).First().Name;
            }
            else if (parameter.ToString() == "Priority")
            {
                return SyncContextInstance.Context.PriorityCollection.Where(e => e.ID == (int)value).First().Name;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            try
            {
                if (parameter.ToString() == "Status")
                {
                    return SyncContextInstance.Context.StatusCollection.Where(e => e.ID == int.Parse(value.ToString())).First().ID;
                }
                else if (parameter.ToString() == "Priority")
                {
                    return SyncContextInstance.Context.PriorityCollection.Where(e => e.ID == int.Parse(value.ToString())).First().ID;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception)
            {
                return 1;
            }
            
        }
    }
}
