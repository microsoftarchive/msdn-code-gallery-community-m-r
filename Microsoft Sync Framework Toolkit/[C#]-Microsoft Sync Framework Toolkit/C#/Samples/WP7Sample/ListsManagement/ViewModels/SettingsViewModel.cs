// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Runtime.Serialization.Json;
using System.IO.IsolatedStorage;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using Microsoft.Synchronization.ClientServices;
using System.Windows.Media;
namespace ListsManagement.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        static SettingsViewModel _instance;
        Timer timer = null;

        public static SettingsViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    ReadSettingsFile();
                }
                return _instance;
            }
        }

        public string SyncStatus
        {
            get;
            set;
        }

        public int AutoSyncInterval
        {
            get;
            set;
        }

        public bool AutoSyncEnabled
        {
            get;
            set;
        }

        public Guid UserId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public bool SyncLogEnabled
        {
            get;
            set;
        }

        public string CacheDataSize
        {
            get
            {
                long fileSize = 0;
                using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    string[] files = iso.GetFileNames();
                    fileSize = ComputeFileSize(iso, string.Empty, files);

                    // Compute Directories
                    fileSize += ComputeDirectorySizes(iso, string.Empty, iso.GetDirectoryNames());
                }

                if (fileSize < 1024)
                {
                    return fileSize + " B";
                }

                fileSize /= 1024;

                if (fileSize < 1024)
                {
                    return fileSize + " KB";
                }
                fileSize /= 1024;
                if (fileSize < 1024)
                {
                    return fileSize + " MB";
                }
                return fileSize / 1024 + " GB";
            }
        }

        private long ComputeDirectorySizes(IsolatedStorageFile iso, string prefix, string[] dirs)
        {
            long fileSize = 0;

            foreach (string dir in dirs)
            {
                string pattern = System.IO.Path.Combine(prefix, System.IO.Path.Combine(dir, "*"));
                fileSize += ComputeFileSize(iso, pattern, iso.GetFileNames(pattern));
                fileSize += ComputeDirectorySizes(iso, dir, iso.GetDirectoryNames(pattern)); 
            }
            return fileSize;
        }

        private static long ComputeFileSize(IsolatedStorageFile iso, string prefix, string[] files)
        {
            long fileSize = 0;
            foreach (string s in files)
            {
                if (s == "lock") continue;
                string fileName = string.IsNullOrEmpty(prefix) ? string.Empty : prefix.Substring(0, prefix.Length - 2);
                fileName = System.IO.Path.Combine(fileName, s);
                if (iso.FileExists(fileName))
                {
                    try
                    {
                        using (IsolatedStorageFileStream isofile = iso.OpenFile(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            fileSize += isofile.Length;
                        }
                    }
                    catch (IsolatedStorageException) 
                    {
                        // Continue as the size is only a best guess
                    }
                }
            }
            return fileSize;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void StartAutoSync()
        {
            if (timer == null)
            {
                timer = new Timer(OnTimerFired, null, 0, this.AutoSyncInterval * 60 * 1000);
            }
            else
            {
                timer.Change(0, this.AutoSyncInterval * 60 * 1000);
            }
        }

        public void OnTimerFired(object state)
        {
            if (!SyncContextInstance.Context.CacheController.IsBusy)
            {
                try
                {
                    SyncContextInstance.Context.CacheController.RefreshCompleted += new EventHandler<Microsoft.Synchronization.ClientServices.RefreshCompletedEventArgs>(CacheController_RefreshCompleted);
                    SyncContextInstance.Context.CacheController.RefreshAsync();
                }
                catch (CacheControllerException) { }
            }
        }

        void CacheController_RefreshCompleted(object sender, Microsoft.Synchronization.ClientServices.RefreshCompletedEventArgs e)
        {
            SyncContextInstance.Context.CacheController.RefreshCompleted -= new EventHandler<Microsoft.Synchronization.ClientServices.RefreshCompletedEventArgs>(CacheController_RefreshCompleted);
            SyncContextInstance.AddStats(e.Statistics, e.Error);
        }

        public void StopAutoSync()
        {
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        private static void ReadSettingsFile()
        {
            // Load the settings from IsolatedStorage
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SettingsViewModel));

            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (iso.FileExists("Settings.txt"))
                {
                    using (IsolatedStorageFileStream fs = iso.OpenFile("Settings.txt", System.IO.FileMode.Open))
                    {
                        _instance = (SettingsViewModel)serializer.ReadObject(fs);
                    }
                }
                else
                {
                    _instance = new SettingsViewModel()
                    {
                        AutoSyncEnabled = false,
                        AutoSyncInterval = 5,
                        SyncLogEnabled = true,
                        SyncStatus = "Not Synchronized yet."
                    };
                    SaveSettingsFile();
                }
            }
        }

        internal static void SaveSettingsFile()
        {
            // Load the settings from IsolatedStorage
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SettingsViewModel));

            try
            {
                using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream fs = iso.OpenFile("Settings.txt", System.IO.FileMode.Create))
                    {
                        serializer.WriteObject(fs, _instance);
                        fs.Flush();
                    }
                }
            }
            finally
            {
                if (_instance.AutoSyncEnabled)
                {
                    _instance.StartAutoSync();
                }
                else
                {
                    _instance.StopAutoSync();
                }
            }
        }
    }
}
