// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System.Windows.Data;
using System;
using ListsManagement.ViewModels;
using System.Linq;
using System.Text;
using DefaultScope;

namespace ListsManagement.Converters
{
    public class TagsConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            Guid itemId = new Guid(value.ToString());

            var tagNames = from tags in SyncContextInstance.Context.TagCollection
                           join t2i in SyncContextInstance.Context.TagItemMappingCollection.Where((e) => e.ItemID == itemId)
                           on tags.ID equals t2i.TagID
                           select tags.Name;

            return String.Join(",", tagNames.ToArray());
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
