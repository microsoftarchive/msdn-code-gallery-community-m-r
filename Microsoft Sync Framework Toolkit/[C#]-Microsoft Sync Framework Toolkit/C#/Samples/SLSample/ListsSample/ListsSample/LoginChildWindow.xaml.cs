// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;

namespace ListsSample
{
    /// <summary>
    /// This is a simple child window which displays a window for the user to login
    /// </summary>
    public partial class LoginChildWindow : ChildWindow
    {
        // Stores the user name.  Used for databinding
        UserInfo userInfo = new UserInfo();

        public LoginChildWindow()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(LoginChildWindow_Loaded);
        }

        void LoginChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = userInfo;
        }

        public string UserName
        {
            get
            {
                return userInfo.Name;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Increase the isolated storage quota so that there is enough space.  Currently, the context
            // doesn't handle the case where it runs out of room, so applications must request enough space
            // up front.
            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                long quota = 5 * 1024 * 1024;
                if (isoFile.Quota < quota)
                {
                    if (isoFile.IncreaseQuotaTo(quota))
                    {
                        this.DialogResult = true;
                    }
                    else
                    {
                        this.MessageTextBlock.Text = "You must increase the quota in order to allow the app to run";
                    }
                }
                else
                {
                    this.DialogResult = true;
                }
            }
        }

        /// <summary>
        /// Helper class used to databind the user name
        /// </summary>
        public class UserInfo : INotifyPropertyChanged
        {
            string name = null;

            public event PropertyChangedEventHandler PropertyChanged;

            public string Name 
            {
                get
                {
                    return name;
                }

                set
                {
                    if (value != name)
                    {
                        name = value;

                        var propChanged = PropertyChanged;

                        if (propChanged != null)
                        {
                            propChanged(this, new PropertyChangedEventArgs("Name"));
                        }
                    }
                }

            }
        }
    }
}

