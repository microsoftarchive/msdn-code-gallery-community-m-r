// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Specialized;

using DefaultScope;
using System.Collections.Generic;
using System.Windows;

namespace ListsSample.Model
{
    /// <summary>
    /// This is a conceptualized view of a list from the service, used for databinding.
    /// As such, it provides extra properties that the underlying list does not have.
    /// </summary>
    public class ModelList : INotifyPropertyChanged
    {
        DefaultScope.List list;
        ListSampleOfflineContext context;
        DynamicQuery<Item> items;

        /// <summary>
        /// Construct which takes the underlying list and its associated context.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="context"></param>
        public ModelList(DefaultScope.List list, ListSampleOfflineContext context)
        {
            this.list = list;
            this.context = context;
            items = null;

            // Register for the property changed event
            list.PropertyChanged += new PropertyChangedEventHandler(list_PropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Returns the underlying list
        /// </summary>
        public DefaultScope.List List
        {
            get
            {
                return list;
            }
        }

        /// <summary>
        /// Returns the list name
        /// </summary>
        public string Name
        {
            get { return list.Name; }
            set
            {
                list.Name = value;
            }
        }

        /// <summary>
        /// Returns the list description
        /// </summary>
        public string Description
        {
            get
            {
                return list.Description;
            }

            set
            {
                list.Description = value;
            }
        }

        /// <summary>
        /// Returns the set of items that are part of the list
        /// </summary>
        public IEnumerable<Item> Items
        {
            get
            {
                if (items == null)
                {
                    items = new DynamicQuery<Item>(
                        from i in context.ItemCollection where i.ListID == list.ID select i,
                        (INotifyCollectionChanged)context.ItemCollection);
                }

                return items;
            }
        }

        /// <summary>
        /// Handles when a property changes on the underlying list.  When this happens this instance
        /// should also notify.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void list_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        /// <summary>
        /// Signals the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            var propChanged = PropertyChanged;

            if (propChanged != null)
            {
                propChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
