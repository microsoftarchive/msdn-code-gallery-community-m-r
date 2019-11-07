// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System.Windows.Data;
using ListsManagement.ViewModels;

namespace ListsManagement.Converters
{
    public class SettingsValueRetrieverConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((string)parameter == "SyncStatus")
            {
                return SettingsViewModel.Instance.SyncStatus;
            }
            else
            {
                return null;// SettingsViewModel.Instance.SyncStatusColor;
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
