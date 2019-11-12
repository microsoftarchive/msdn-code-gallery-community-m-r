// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;

namespace ListsSample
{
    /// <summary>
    /// This class converts certain service entities into Model entities for databinding.
    /// The entities that are converted are ListCollection and ItemCollection
    /// </summary>
    public class ItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            DefaultScope.List srcList = value as DefaultScope.List;

            if (srcList != null)
            {
                return Model.ContextModel.Instance.GetList(srcList);
            }

            DefaultScope.Item srcItem = value as DefaultScope.Item;

            if (srcItem != null)
            {
                return Model.ContextModel.Instance.GetItem(srcItem);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            Model.ModelItem srcItem = value as Model.ModelItem;

            if (srcItem != null)
            {
                return srcItem.Item;
            }

            Model.ModelList srcList = value as Model.ModelList;

            if (srcList != null)
            {
                return srcList.List;
            }

            return null;
        }
    }
}
