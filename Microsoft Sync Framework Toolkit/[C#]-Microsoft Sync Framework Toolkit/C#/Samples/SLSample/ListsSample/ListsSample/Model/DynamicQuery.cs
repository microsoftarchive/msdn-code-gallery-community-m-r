// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace ListsSample.Model
{
    /// <summary>
    /// This is a simple class which allows a LINQ query to be returned an notify for property changed events.
    /// 
    /// The way it works is by taking an IEnumerable which is a query and a collection of INotifyCollectionChanged
    /// objects.  It then registers for the CollectionChanged event on each of them.  Whenever the event is signalled,
    /// it signals its own CollectionChanged event.  Note that the collections could change without changing the query
    /// so this is not most efficient way to handle it.
    /// 
    /// The other key not is that this only works because LINQ allows deferred execution.  When the IEnumerator is used,
    /// a snapshot of the LINQ query is taken at that moment in time.  This means that accessing it at other times could
    /// result in a different set of results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicQuery<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        List<INotifyCollectionChanged> dynamicCollections;
        IEnumerable<T> query;

        /// <summary>
        /// Constructor which takes the query to return data from and the set of collections to watch for
        /// CollectionChanged events
        /// </summary>
        /// <param name="query">Query to filter the data</param>
        /// <param name="collections">Set of collections which make up the query</param>
        public DynamicQuery(IEnumerable<T> query, params INotifyCollectionChanged [] collections)
        {
            // Verify that at least one collection to watch is specified
            if (collections.Length == 0)
            {
                throw new ArgumentException("At least one dynamic collection must be specified");
            }

            this.dynamicCollections = new List<INotifyCollectionChanged>(collections);
            this.query = query;

            // Register for the collection changed event
            foreach (INotifyCollectionChanged collection in dynamicCollections)
            {
                collection.CollectionChanged += new NotifyCollectionChangedEventHandler(dynamicCollection_CollectionChanged);
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Handles when one of the watched collections changes and signals its own CollectionChanged event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dynamicCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collChanged = CollectionChanged;

            if (collChanged != null)
            {
                // Unfortunately, we don't really get to pick an action since we're selectively filtering.
                collChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        #region IEnumerable<T> implementation
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            // Snapshot the query so it won't change during enumeration (will cause an exception)
            var snapshot = query.ToList();

            foreach (T t in snapshot)
            {
                yield return t;
            }
        }
        #endregion
    }
}
