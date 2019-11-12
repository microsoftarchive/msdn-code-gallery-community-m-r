// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Linq;
using System.ComponentModel;

using DefaultScope;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Windows;


namespace ListsSample.Model
{
    /// <summary>
    /// This class is a wrapper over a context item.  It provides extra properties for databinding,
    /// such as Tags (used tags) and UnusedTags, and the list.
    /// </summary>
    public class ModelItem : INotifyPropertyChanged
    {
        Item item;
        ListSampleOfflineContext context;
        DynamicQuery<Tag> tags;
        DynamicQuery<Tag> unusedTags;
        Priority priority = null;
        Status status = null;
        Dictionary<int, int> usedTags;
        DefaultScope.List list;

        /// <summary>
        /// Constructor which takes the context Item that is being wrapped and its associated context.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="context"></param>
        public ModelItem(Item item, ListSampleOfflineContext context)
        {
            this.item = item;
            this.context = context;
            tags = null;
            unusedTags = null;
            usedTags = new Dictionary<int, int>();
            list = null;

            // Materialize the priority and status from the ids in the item
            UpdatePriority();
            UpdateStatus();

            // Register for any events that occur on the item
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);

            // Register for any changes to the tag item mappings
            ((INotifyCollectionChanged)context.TagItemMappingCollection).CollectionChanged += new NotifyCollectionChangedEventHandler(ModelItem_CollectionChanged);
        }

        /// <summary>
        /// Handles the tag item mapping colleciton changed events.  Updates the used tags dictionary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ModelItem_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateUsedTags(from t in context.TagItemMappingCollection 
                           where t.ItemID == item.ID
                           select t);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Returns the item name
        /// </summary>
        public string Name
        {
            get
            {
                return item.Name;
            }

            set
            {
                item.Name = value;
            }
        }

        /// <summary>
        /// Returns the item description
        /// </summary>
        public string Description
        {
            get
            {
                return item.Description;
            }

            set
            {
                item.Description = value;
            }
        }

        /// <summary>
        /// Returns the priority text
        /// </summary>
        public string PriorityText
        {
            get
            {
                if (priority != null)
                {
                    return priority.Name;
                }
                return null;
            }
        }

        /// <summary>
        /// Returns the status text
        /// </summary>
        public string StatusText
        {
            get
            {
                if (status != null)
                {
                    return status.Name;
                }
                return null;
            }
        }

        /// <summary>
        /// Returns the start date converted to text
        /// </summary>
        public string StartDateText
        {
            get
            {
                if (item.StartDate.HasValue)
                {
                    return item.StartDate.Value.ToString("d");
                }
                return null;
            }
        }

        /// <summary>
        /// Returns the start date
        /// </summary>
        public DateTime? StartDate
        {
            get
            {
                return item.StartDate;
            }

            set
            {
                item.StartDate = value;
            }
        }

        /// <summary>
        /// Returns the end date converted to text
        /// </summary>
        public string EndDateText
        {
            get
            {
                if (item.EndDate.HasValue)
                {
                    return item.EndDate.Value.ToString("d");
                }
                return null;
            }
        }

        /// <summary>
        /// Returns the end date
        /// </summary>
        public DateTime? EndDate
        {
            get
            {
                return item.EndDate;
            }

            set
            {
                item.EndDate = value;
            }
        }

        /// <summary>
        /// Returns the status
        /// </summary>
        public Status Status
        {
            get
            {
                return status;
            }

            set
            {
                if (value != status)
                {
                    status = value;
                    item.Status = (status != null && status.ID != -1) ? status.ID : default(int?);
                    OnPropertyChanged("Status");
                    OnPropertyChanged("StatusText");
                }
            }
        }

        /// <summary>
        /// Returns the priority
        /// </summary>
        public Priority Priority
        {
            get
            {
                return priority;
            }

            set
            {
                if (value != priority)
                {
                    priority = value;
                    item.Priority = (priority != null) ? priority.ID : default(int?);
                    OnPropertyChanged("Priority");
                    OnPropertyChanged("PriorityText");
                }
            }
        }

        /// <summary>
        /// Handles the visibility of the hyphen between the start and end date
        /// </summary>
        public Visibility StartAndEnd
        {
            get
            {
                if (item.EndDate.HasValue && item.StartDate.HasValue)
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }
        }
        
        /// <summary>
        /// Returns the tags current used by the item
        /// </summary>
        public IEnumerable<Tag> Tags
        {
            get
            {
                if (tags == null)
                {
                    GenerateTagsCollection();
                }

                return tags;
            }
        }

        /// <summary>
        /// Returns the underlying context item
        /// </summary>
        public DefaultScope.Item Item
        {
            get
            {
                return item;
            }
        }

        /// <summary>
        /// Returns the collection of statuses from the context.  This
        /// is used for databinding a combobox
        /// </summary>
        public IEnumerable<Status> Statuses
        {
            get
            {
                return context.StatusCollection;
            }
        }

        /// <summary>
        /// Returns the collection of priorities from the context.  This
        /// is used for databinding a combobox
        /// </summary>
        public IEnumerable<Priority> Priorities
        {
            get
            {
                return context.PriorityCollection;
            }
        }

        /// <summary>
        /// Returns the set of unused tags
        /// </summary>
        public IEnumerable<Tag> UnusedTags
        {
            get
            {
                if (unusedTags == null)
                {
                    unusedTags = new DynamicQuery<Tag>(
                        from t in context.TagCollection
                        where !usedTags.ContainsKey(t.ID)
                        orderby t.Name
                        select t,
                        (INotifyCollectionChanged)context.TagCollection,
                        (INotifyCollectionChanged)context.TagItemMappingCollection);
                }

                return unusedTags;
            }
        }

        /// <summary>
        /// Returns the parent list
        /// </summary>
        public DefaultScope.List List
        {
            get
            {
                if (list == null)
                {
                    UpdateList();
                    
                }

                return list;
            }
        }

        /// <summary>
        /// Returns the parent list name
        /// </summary>
        public string ListName
        {
            get
            {
                return List.Name;
            }
        }

        /// <summary>
        /// Generate the collection of tags specified for the item
        /// </summary>
        private void GenerateTagsCollection()
        {
            // The query joins between the tag item mapping and tags based on
            // the user id
            var query = from m in context.TagItemMappingCollection
                        where m.ItemID == item.ID
                        join t in context.TagCollection on m.TagID equals t.ID
                        orderby t.Name
                        select t;

            // Create the dynamic query to handle changes in either collection
            tags = new DynamicQuery<Tag>(
                query,
                (INotifyCollectionChanged)context.TagItemMappingCollection,
                (INotifyCollectionChanged)context.TagCollection);

            // Update the dictionary of used tags
            UpdateUsedTags(from m in context.TagItemMappingCollection
                               where m.ItemID == item.ID
                               select m);
        }

        /// <summary>
        /// Handles when a property changes on the underlying item.  Certain properties
        /// have to be handled in different ways
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                case "Description":
                case "StartDate":
                case "EndDate":
                    OnPropertyChanged(e.PropertyName);
                    break;

                case "List":
                    if (list != null)
                    {
                        UpdateList();
                    }
                    OnPropertyChanged(e.PropertyName);
                    OnPropertyChanged("ListName");
                    break;

                case "StatusText":
                    UpdateStatus();
                    break;

                case "PriorityText":
                    UpdatePriority();
                    break;
            }
        }

        /// <summary>
        /// This method keeps track of the tags used by the item so that the unused tags can also be calculated
        /// </summary>
        /// <param name="collection"></param>
        private void UpdateUsedTags(IEnumerable<TagItemMapping> collection)
        {
            // Clear out the existing used tags
            usedTags.Clear();

            // Find the currently used ones
            IEnumerable<TagItemMapping> ienum = (IEnumerable<TagItemMapping>)collection;
            foreach (TagItemMapping tim in ienum)
            {
                usedTags.Add(tim.TagID, tim.TagID);
            }
        }

        /// <summary>
        /// Materializes the priority based on the priority id stored in the underlying item
        /// </summary>
        private void UpdatePriority()
        {
            if (item.Priority.HasValue)
            {
                Priority newPriority = (from p in context.PriorityCollection
                                        where p.ID == item.Priority
                                        select p).FirstOrDefault();

                if (newPriority == null && priority != null)
                {
                    SetPriority(newPriority);
                }
                else if (newPriority != null && priority == null)
                {
                    SetPriority(newPriority);
                }
                else
                {
                    if (newPriority.ID != priority.ID)
                    {
                        SetPriority(newPriority);
                    }
                }
            }
            else
            {
                SetPriority(null);
            }
        }

        /// <summary>
        /// Materializes the status based on the status id stored in the underlying item
        /// </summary>
        private void UpdateStatus()
        {
            if (item.Status.HasValue)
            {
                Status newStatus = (from p in context.StatusCollection
                                    where p.ID == item.Status
                                    select p).FirstOrDefault();

                if (newStatus == null && StatusText != null)
                {
                    SetStatus(newStatus);
                }
                else if (newStatus != null && StatusText == null)
                {
                    SetStatus(newStatus);
                }
                else
                {
                    if (newStatus.ID != status.ID)
                    {
                        SetStatus(newStatus);
                    }
                }
            }
            else
            {
                SetStatus(null);
            }
        }

        /// <summary>
        /// Gets a new materialized list
        /// </summary>
        private void UpdateList()
        {
            list = (from l in context.ListCollection
                    where l.ID == item.ListID
                    select l).FirstOrDefault();
        }

        /// <summary>
        /// Notifies that a property changed
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

        /// <summary>
        /// Sets the priority and notifies
        /// </summary>
        /// <param name="newPriority"></param>
        private void SetPriority(Priority newPriority)
        {
            priority = newPriority;

            OnPropertyChanged("PriorityText");
        }

        /// <summary>
        /// Sets the status and notifies
        /// </summary>
        /// <param name="newPriority"></param>
        private void SetStatus(Status newStatus)
        {
            status = newStatus;

            OnPropertyChanged("StatusText");
        }

        /// <summary>
        /// Adds a new tag for the item
        /// </summary>
        /// <param name="tag"></param>
        public void AddTag(Tag tag)
        {
            ContextModel.Instance.AddTagMapping(item, tag);
        }

        /// <summary>
        /// Removes a tag for the item
        /// </summary>
        /// <param name="tag"></param>
        public void RemoveTag(Tag tag)
        {
            ContextModel.Instance.RemoveTagMapping(item, tag);
        }
    }
}
