// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System.Windows.Data;
using System;
using System.Text;
using System.Reflection;
using ListsManagement.ViewModels;
using System.Linq;

namespace ListsManagement.Converters
{
    public class TagsCounterConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return (0);
            }

            int tagId = SyncContextInstance.Context.TagCollection.Where(e => e.Name == value as string).First().ID;
            return "(" + SyncContextInstance.Context.TagItemMappingCollection.Count(e => e.TagID == tagId) + ")";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
