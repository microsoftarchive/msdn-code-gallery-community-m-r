// <copyright file="IsolatedStoragePersistenceService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IsolatedStoragePersistenceService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.IsolatedStorage;

    public class IsolatedStoragePersistenceService : IPersistenceService
    {   
        public event EventHandler StorageMaxSpaceReached;

        public event EventHandler StorageRemoved;

        public long UsedSize
        {
            get
            { 
                if (!IsEnabled)
                {
                    return 0;
                }

                using (IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return isolatedStorageFile.UsedSize;
                }
            }
        }

        public long AvailableFreeSpace
        {
            get
            {
                if (!IsEnabled)
                {
                    return 0;
                }

                using (IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return isolatedStorageFile.AvailableFreeSpace;
                }
            }
        }

        public long Quota
        {
            get
            {
                if (!IsEnabled)
                {
                    return 0;
                }

                using (IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return isolatedStorageFile.Quota;
                }
            }
        }

        private static bool IsEnabled
        {
            get { return IsolatedStorageFile.IsEnabled; }
        }

        public Dictionary<string, object> GetApplicationSettings()
        {
            var settings = new Dictionary<string, object>();

            if (IsEnabled)
            {
                foreach (KeyValuePair<string, object> pair in IsolatedStorageSettings.ApplicationSettings)
                {
                    if (!settings.ContainsValue(pair.Value))
                    {
                        settings.Add(pair.Key, pair.Value);
                    }
                }
            }

            return settings;
        }

        public void AddApplicationSettings(string key, object value)
        {
            if (IsEnabled)
            {
                IsolatedStorageSettings.ApplicationSettings[key] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public Stream Retrieve(string key)
        {
            if (!string.IsNullOrEmpty(key) && IsEnabled)
            {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists(key))
                    {
                        Stream stream = store.OpenFile(key, FileMode.Open, FileAccess.Read);

                        stream.Position = 0;
                        return stream;
                    }
                }
            }

            return null;
        }

        public bool Persist(string key, Stream stream)
        {
            bool result = false;
            if (stream != null && !string.IsNullOrEmpty(key) && IsEnabled)
            {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (stream.Length < (store.AvailableFreeSpace - (store.AvailableFreeSpace * 0.025)))
                    {
                        using (var isoStream = store.OpenFile(key, FileMode.Create))
                        {
                            byte[] bytes = new byte[stream.Length];
                            int numBytesToRead = (int)stream.Length;
                            int numBytesRead = 0;

                            while (numBytesToRead > 0)
                            {
                                int n = stream.Read(bytes, numBytesRead, numBytesToRead);

                                if (n == 0)
                                {
                                    break;
                                }

                                numBytesRead += n;
                                numBytesToRead -= n;
                            }

                            numBytesToRead = bytes.Length;

                            isoStream.Write(bytes, 0, numBytesToRead);

                            result = true;
                        }
                    }
                    else
                    {
                        this.OnStorageMaxSpaceReached();
                    }

                    stream.Close();
                }
            }

            return result;
        }

        public bool IncreaseQuota(long bytesToIncrease)
        {
            if (IsEnabled)
            {
                using (IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return isolatedStorageFile.IncreaseQuotaTo(isolatedStorageFile.Quota + bytesToIncrease);
                }
            }

            return false;
        }

        public void RemoveStorage()
        {
            if (IsEnabled)
            {
                using (IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    IsolatedStorageSettings.ApplicationSettings.Clear();
                    IsolatedStorageSettings.ApplicationSettings.Save();
                    isolatedStorageFile.Remove();
                    
                    this.OnStorageRemoved();
                }
            }
        }

        public void RemoveItem(string key)
        {
            if (!string.IsNullOrEmpty(key) && IsEnabled)
            {
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists(key))
                    {
                        try
                        {
                            store.DeleteFile(key);

                            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                            {
                                IsolatedStorageSettings.ApplicationSettings.Remove(key);
                                IsolatedStorageSettings.ApplicationSettings.Save();
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void OnStorageMaxSpaceReached()
        {
            EventHandler handler = this.StorageMaxSpaceReached;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnStorageRemoved()
        {
            EventHandler handler = this.StorageRemoved;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
