// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;

using DefaultScope;
using Microsoft.Synchronization.ClientServices;
using Microsoft.Synchronization.ClientServices.IsolatedStorage;
using System.Windows.Threading;

namespace ListsSample.Model
{
    /// <summary>
    /// This class provides a simple model that wraps the entities retrieved from the
    /// service to provide richer databinding.  It is similar to a view model, but is not
    /// quite complete in the way that it is implemented.
    /// 
    /// This is a singleton class, so only one can be instantiated.
    /// </summary>
    public class ContextModel : INotifyPropertyChanged
    {
        private ListSampleOfflineContext context;
        private static ContextModel instance;
        private bool syncInactive = true;
        private bool loaded = false;
        private DynamicQuery<ModelTag> userTags;
        private string lastSyncStatus = null;
        private double lastSyncStatusDisplayInSeconds = 60.0;

        // NOTE: Replace this URI with your service Uri
        private Uri ServiceUri = new Uri("http://localhost/ListService/DefaultScopeSyncService.svc/");

        private ContextModel()
        {
            UserID = Guid.Empty;
            userTags = null;
            LoadUserID();
        }

        /// <summary>
        /// Public method to load the context
        /// </summary>
        public void LoadContext()
        {
            CreateContext("listsample", ServiceUri);
            context.LoadAsync();
        }

        /// <summary>
        /// Public event handler called when the load is completed
        /// </summary>
        public event EventHandler<LoadCompletedEventArgs> LoadCompleted;

        /// <summary>
        /// This method handles "logging in" the user with the specified user name.
        /// 
        /// NOTE: This is not secure. The service takes a user name and returns a
        /// user id.  There is no authentication.
        /// </summary>
        /// <param name="userName"></param>
        public void LoginAsync(string userName)
        {
            Exception e = null;
            userName = userName.Trim();

            // Validate the user name
            if (string.IsNullOrEmpty(userName))
            {
                e = new ArgumentNullException("userName");
            }
            else
            {
                var validator = new Regex("^([A-Za-z0-9 ])+$");

                if (!validator.IsMatch(userName))
                {
                    e = new ArgumentException("Username can consist of only alphanumeric characters and spaces");
                }
            }

            if (e != null)
            {
                // If there was an error, notify that login completed with a failure.  Have to
                // marshal back to the UI thread.
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        OnLoginCompleted(e);
                    });
                return;
            }


            // Use the webclient to get the user id
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(LoginDownloadStringCompleted);
            webClient.DownloadStringAsync(
                new Uri(ServiceUri, String.Format("../login.ashx?username={0}", userName)));

        }

        /// <summary>
        /// Event called when login is completed
        /// </summary>
        public event Action<Exception> LoginCompleted;

        /// <summary>
        /// The user id
        /// </summary>
        public Guid UserID
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the singleton instance.  Creates the instance if necessary
        /// </summary>
        public static ContextModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContextModel();
                }

                return instance;
            }
        }

        public string LastSyncStatus
        {
            get
            {
                return lastSyncStatus;
            }

            set
            {
                if (value != lastSyncStatus)
                {
                    if (value != null)
                    {
                        DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(lastSyncStatusDisplayInSeconds) };

                        dt.Tick += (sender, args) =>
                        {
                            LastSyncStatus = null;
                            ((DispatcherTimer)sender).Stop();
                        };

                        dt.Start();
                    }

                    lastSyncStatus = value;
                    OnPropertyChanged("LastSyncStatus");
                }
            }
        }

        /// <summary>
        /// Allows for databinding on buttons being available depending on if
        /// the sync is active.
        /// </summary>
        public bool SyncInactive
        {
            get
            {
                return syncInactive;
            }

            internal set
            {
                if (value != syncInactive)
                {
                    syncInactive = value;
                    OnPropertyChanged("SyncInactive");
                }
            }
        }

        /// <summary>
        /// Saves changes to disk.  Once they are saved they cannot be cancelled.
        /// </summary>
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Reverts all unsaved changes
        /// </summary>
        public void CancelChanges()
        {
            context.CancelChanges();
        }

        /// <summary>
        /// Event notified when a property of the context changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method to initiate sync with the service
        /// </summary>
        public void Sync()
        {
            SyncInactive = false;
            LastSyncStatus = null;
            context.CacheController.RefreshAsync();
        }

        /// <summary>
        /// Adds a new list.  It is important to note that because there is no enforcement of
        /// data consistency in the context, the app must understand the data and enforce it.
        /// </summary>
        /// <param name="modelList">The model list to add</param>
        public void AddList(ModelList modelList)
        {
            // Extract the service entity from the model entity
            DefaultScope.List newList = modelList.List;

            // Simple argument checking
            if (null == newList)
            {
                throw new ArgumentNullException("newList");
            }

            if (newList.ID == null)
            {
                throw new ArgumentNullException("List id is null");
            }

            // Do a LINQ query to verify that the list can be added.  Current constraints
            // are unique id and unique name.
            DefaultScope.List existingList = (from s in context.ListCollection 
                                              where s.ID == newList.ID || s.Name == newList.Name
                                              select s).FirstOrDefault();

            if (existingList != default(List))
            {
                throw new ArgumentException("List with the same id already exists");
            }

            // Add the list to the context.
            context.AddList(newList);
        }

        /// <summary>
        /// Instantiates a new model list.  Used when creating a new list and displaying ui
        /// </summary>
        /// <returns></returns>
        public ModelList GetNewList()
        {
            return new ModelList(
                new List() { ID = Guid.NewGuid(), UserID = UserID, CreatedDate = DateTime.Now },
                context);
        }

        /// <summary>
        /// Gets the corresponding model list for a context list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ModelList GetList(DefaultScope.List list)
        {
            return new ModelList(list, context);
        }

        /// <summary>
        /// Returns the collection of tags
        /// </summary>
        public IEnumerable<Tag> Tags
        {
            get
            {
                if (loaded)
                {
                    return context.TagCollection;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the collection of statuses
        /// </summary>
        public IEnumerable<Status> Statuses
        {
            get
            {
                if (loaded)
                {
                    return context.StatusCollection;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the collection of tag item mappings
        /// </summary>
        public IEnumerable<TagItemMapping> TagItemMappings
        {
            get
            {
                if (loaded)
                {
                    return context.TagItemMappingCollection;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the collection of list items
        /// </summary>
        public IEnumerable<Item> Items
        {
            get
            {
                if (loaded)
                {
                    return context.ItemCollection;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the collection of priorities
        /// </summary>
        public IEnumerable<Priority> Priorities
        {
            get
            {
                if (loaded)
                {
                    return context.PriorityCollection;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the current user
        /// </summary>
        public User User
        {
            get
            {
                if (loaded)
                {
                    // Since the filter is on User Id, there should only be one
                    return context.UserCollection.FirstOrDefault();
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the collection of lists
        /// </summary>
        public IEnumerable<DefaultScope.List> Lists
        {
            get
            {
                if (loaded)
                {
                    return context.ListCollection;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the set of tags in use by user items
        /// </summary>
        public IEnumerable<ModelTag> UserTags
        {
            get
            {
                if (loaded)
                {
                    if (userTags == null)
                    {
                        // Using the context, you can do complex LINQ queries by using Linq to objects.
                        // This query is calculating instance counts for each tag, using only those that
                        // are actually used (the join with TagItemMappingCollection ensures this), and
                        // sorting by the tag name. 
                        //
                        // This query is used in the tags tab of the tab control
                        userTags = new DynamicQuery<ModelTag>(
                            (from m in context.TagItemMappingCollection
                             join t in context.TagCollection on m.TagID equals t.ID
                             group m by m.TagID into tagGroup
                             select new ModelTag(tagGroup.Key, tagGroup.Count())).OrderBy(t => t.Tag),
                            (INotifyCollectionChanged)context.TagItemMappingCollection,
                            (INotifyCollectionChanged)context.TagCollection);
                    }

                    return userTags;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the corresponding model item for a context item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ModelItem GetItem(Item item)
        {
            return new ModelItem(item, context);
        }

        /// <summary>
        /// Gets a new model item for creating an item.  Takes the
        /// parent list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ModelItem GetNewItem(ModelList list)
        {
            // This method requires the parent list and the user id
            // to ensure that referential integrity is maintained
            Item item = new Item()
            {
                ID = Guid.NewGuid(),
                UserID = UserID,
                ListID = list.List.ID
            };

            return new ModelItem(item, context);
        }

        /// <summary>
        /// Deletes the specified list.
        /// 
        /// NOTE: Because of the fact that the context doesn't handle referential
        /// integrity, the application must handle the cascading delete
        /// </summary>
        /// <param name="list"></param>
        public void DeleteList(DefaultScope.List list)
        {
            // First find all items that are in the list and delete them
            List<Item> listItems = (from i in context.ItemCollection
                                    where i.ListID == list.ID
                                    select i).ToList();

            foreach (Item i in listItems)
            {
                DeleteItem(i);
            }

            // once all items are deleted, delete the list
            context.DeleteList(list);
        }

        /// <summary>
        /// Deletes the specified item.
        /// 
        /// NOTE: Because of the fact that the context doesn't handle referential
        /// integrity, the application must handle the cascading delete of
        /// tag <-> item mappings
        /// </summary>
        /// <param name="list"></param>
        public void DeleteItem(Item item)
        {
            // Get the list of TagItemMappings for the requested item
            List<TagItemMapping> tags = (from mapping in context.TagItemMappingCollection
                                         where mapping.ItemID == item.ID
                                         select mapping).ToList();

            // Delete the mappings
            foreach (TagItemMapping mapping in tags)
            {
                context.DeleteTagItemMapping(mapping);
            }

            // Now it should be safe to delete the item
            context.DeleteItem(item);
        }

        /// <summary>
        /// Adds a new item based on the model item passed in.
        /// </summary>
        /// <param name="modelItem"></param>
        public void AddItem(ModelItem modelItem)
        {
            // Retrieve the context item
            DefaultScope.Item newItem = modelItem.Item;

            // Parameter validation
            if (null == newItem)
            {
                throw new ArgumentNullException("newItem");
            }

            if (newItem.ID == null)
            {
                throw new ArgumentException("Item id is null");
            }

            // Ensure that the item id is unique
            DefaultScope.Item existingItem = (from s in context.ItemCollection
                                              where s.ID == newItem.ID
                                              select s).FirstOrDefault();

            if (existingItem != default(Item))
            {
                throw new ArgumentException("Item with the same id already exists");
            }

            // Add the new item to the context
            context.AddItem(newItem);
        }

        /// <summary>
        /// Adds a new tag to item mapping.
        /// </summary>
        /// <param name="item">Item to which a tag is being mapped</param>
        /// <param name="tag">Tag to which an item is being mapped</param>
        public void AddTagMapping(Item item, Tag tag)
        {
            // Here we ensure that the referential integrity is correct.
            // In the UI, there is no way to add a duplicate mapping, so
            // uniqueness is insured.  Other applications may have to do
            // an explicit check here.
            TagItemMapping tim = new TagItemMapping()
            {
                ItemID = item.ID,
                TagID = tag.ID,
                UserID = UserID
            };

            context.AddTagItemMapping(tim);
        }

        /// <summary>
        /// Removes the requested tag item mapping for the specified tag and item
        /// </summary>
        /// <param name="item">Item for which to remove the tag mapping</param>
        /// <param name="tag">Tag for which to remove the mapping</param>
        public void RemoveTagMapping(Item item, Tag tag)
        {
            // find the mapping
            TagItemMapping tim = (from t in context.TagItemMappingCollection
                                  where t.ItemID == item.ID && t.TagID == tag.ID
                                  select t).FirstOrDefault();

            if (tim != default(TagItemMapping))
            {
                // delete it from the context.
                context.DeleteTagItemMapping(tim);
            }
        }

        /// <summary>
        /// Returns the tag object for the specified tag id
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public Tag GetTag(int tagId)
        {
            return (from t in context.TagCollection where t.ID == tagId select t).FirstOrDefault();
            
        }

        /// <summary>
        /// Creates and initializes the context
        /// </summary>
        /// <param name="cachePath"></param>
        /// <param name="uri"></param>
        private void CreateContext(string cachePath, Uri uri)
        {
            context = new ListSampleOfflineContext(cachePath, uri);

            // Need to specify the filter paramters.
            context.CacheController.ControllerBehavior.AddScopeParameters("userid", UserID.ToString("D"));

            context.LoadCompleted += new EventHandler<LoadCompletedEventArgs>(ContextLoadCompleted);
            context.CacheController.RefreshCompleted += new EventHandler<RefreshCompletedEventArgs>(CacheController_RefreshCompleted);
        }

        /// <summary>
        /// Attempts to read a cached user id
        /// </summary>
        private void LoadUserID()
        {
            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string filePath = "userid";
                if (isoFile.FileExists(filePath))
                {
                    using (TextReader textReader = new StreamReader(isoFile.OpenFile(filePath, FileMode.Open)))
                    {
                        try
                        {
                            UserID = new Guid(textReader.ReadToEnd().Trim());
                        }
                        catch (FormatException)
                        {
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Stores the user id
        /// </summary>
        /// <param name="userID"></param>
        private void StoreUserID(Guid userID)
        {
            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string filePath = "userid";

                using (Stream stream = isoFile.OpenFile(filePath, FileMode.Create))
                {
                    using (TextWriter textWriter = new StreamWriter(stream))
                    {
                        textWriter.Write(userID.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for when the web request to get the user id is completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                UserID = new Guid(e.Result);

                // Write the user id to disk
                StoreUserID(UserID);
            }

            OnLoginCompleted(e.Error);
        }

        /// <summary>
        /// Handles signaling the LoginCompleted event
        /// </summary>
        /// <param name="error"></param>
        private void OnLoginCompleted(Exception error)
        {
            Action<Exception> action = LoginCompleted;

            if (action != null)
            {
                action(error);
            }
        }

        /// <summary>
        /// Handles the cache controller refresh completed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CacheController_RefreshCompleted(object sender, RefreshCompletedEventArgs e)
        {
            SyncInactive = true;

            if (e.Error == null)
            {
                LastSyncStatus = "Synchronization with service completed successfully";
            }
            else
            {
                LastSyncStatus = "Synchronization failed: " + e.Error.ToString();
            }
        }

        /// <summary>
        /// Handles the context LoadCompleted event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextLoadCompleted(object sender, LoadCompletedEventArgs e)
        {
            if (e.Exception == null)
            {
                loaded = true;
                // Notify that all of the collections changed
                OnPropertyChanged("Tags");
                OnPropertyChanged("Statuses");
                OnPropertyChanged("Priorities");
                OnPropertyChanged("User");
                OnPropertyChanged("UserTags");
                OnPropertyChanged("Lists");

                var loadCompleted = LoadCompleted;

                if (loadCompleted != null)
                {
                    loadCompleted(this, e);
                }
            }
            else if (e.Exception is ArgumentException)
            {
                context.ClearCache();
                context.LoadAsync();
            }
        }

        /// <summary>
        /// Signals the property changed event.
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            var evt = PropertyChanged;

            if (evt != null)
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    evt(this, new PropertyChangedEventArgs(propertyName));
                });
            }
        }
    }
}
