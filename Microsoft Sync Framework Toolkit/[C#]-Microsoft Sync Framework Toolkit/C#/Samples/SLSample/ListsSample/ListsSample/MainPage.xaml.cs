// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using DefaultScope;
using ListsSample.Model;
using Microsoft.Synchronization.ClientServices.IsolatedStorage;

namespace ListsSample
{
    /// <summary>
    /// This is the main page of the application.  Most logic for
    /// maintaining the data is handled in the ContextModel class.
    /// </summary>
	public partial class MainPage : UserControl
	{
        public MainPage()
		{
			// Required to initialize UI
			InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
		}

        /// <summary>
        /// Responds to the loaded event so that operations that affect UI
        /// don't happen until the page is loaded
        /// </summary>
        /// <param name="sender">page that was loaded</param>
        /// <param name="e">event args</param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ContextModel.Instance.LoginCompleted += new Action<Exception>(LoginCompleted);
            ContextModel.Instance.LoadCompleted += new EventHandler<Microsoft.Synchronization.ClientServices.IsolatedStorage.LoadCompletedEventArgs>(ModelLoadCompleted);

            // Check if the user was already "logged in"
            // Note: This is a simple technique for this app.  There may be more
            // appropriate mechanisms for your application
            if (ContextModel.Instance.UserID == Guid.Empty)
            {
                // If there was no user id, show the login window
                ShowLoginWindow();
            }
            else
            {
                // Otherwise, load the model and bind it to the page
                Load();
            }
        }

        /// <summary>
        /// Called when the context is don laoding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ModelLoadCompleted(object sender, LoadCompletedEventArgs e)
        {
            // This callback is not guaranteed to occur on the UI thread, so dispatch on the 
            // UI thread.
            Dispatcher.BeginInvoke(() =>
                {
                    if (e.Exception == null)
                    {
                        Bind();
                    }
                    else
                    {
                        MessageBox.Show("Loading the context failed: " + e.Exception.ToString());
                    }
                });
        }

        /// <summary>
        /// Displays the login window
        /// </summary>
        private void ShowLoginWindow()
        {
            // Only display if the user id is not specified
            if (ContextModel.Instance.UserID == Guid.Empty)
            {
                LoginChildWindow window = new LoginChildWindow();
                window.Closed += new EventHandler(LoginWindowClosed);

                window.Show();
            }
        }

        /// <summary>
        /// Operates once the context model reports that login is completed
        /// </summary>
        /// <param name="obj">Error that occured</param>
        private void LoginCompleted(Exception obj)
        {
            if (obj == null)
            {
                // If there was no error, load the context and do databinding
                Load();
            }
            else
            {
                // If there was an error, display it and redisplay the login window
                // (App can't work without login completed successfully)
                MessageBox.Show("The following error occurred when attempting to log on: " + obj.ToString());
                ShowLoginWindow();
            }
        }

        /// <summary>
        /// Event handler for when the login window is closed
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event args</param>
        void LoginWindowClosed(object sender, EventArgs e)
        {
            LoginChildWindow cw = (LoginChildWindow)sender;

            if (cw.DialogResult == true)
            {
                ContextModel.Instance.LoginAsync(cw.UserName);
            }
            else
            {
                // Don't allow close to happen
                ShowLoginWindow();
            }
        }


        /// <summary>
        /// Loads the context and binds the data
        /// </summary>
        private void Load()
        {
            ContextModel.Instance.LoadContext();
        }

        /// <summary>
        /// Binds the context to the context and do an initial sync
        /// </summary>
        private void Bind()
        {
            this.DataContext = ContextModel.Instance;

            ContextModel.Instance.Sync();
        }

        
        /// <summary>
        /// This method initiates sync
        /// </summary>
        /// <param name="sender">Sender of the change</param>
        /// <param name="e">The event args</param>
        private void syncButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the instance sync method
            ContextModel.Instance.Sync();
        }

        /// <summary>
        /// Handles launching a UI for creating a new window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewItemButtonClick(object sender, RoutedEventArgs e)
        {
            Control control = sender as Control;

            // The parent list is bound to the "Tag" property
            ModifyItemChildWindow window = new ModifyItemChildWindow(
                ((ModelList)control.Tag));
            window.NewItem = true;

            window.Closed += new EventHandler(ModifyItemWindowClosed);

            window.Show();
        }

        /// <summary>
        /// Launches the UI to edit an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItemButtonClick(object sender, RoutedEventArgs e)
        {
            Control control = sender as Control;

            // The item to edit is bound to the "Tag" property
            ModifyItemChildWindow window = new ModifyItemChildWindow(
                ((Item)control.Tag));

            window.NewItem = false;

            window.Closed += new EventHandler(ModifyItemWindowClosed);

            window.Show();
        }

        /// <summary>
        /// Deletes the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItemButtonClick(object sender, RoutedEventArgs e)
        {
            Control control = sender as Control;

            // The item to delete is bound to the "Tag" property
            ContextModel.Instance.DeleteItem((DefaultScope.Item)control.Tag);

            // save the changes
            ContextModel.Instance.SaveChanges();
        }

        /// <summary>
        /// Event handler for the modify item window closed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ModifyItemWindowClosed(object sender, EventArgs e)
        {
            ModifyItemChildWindow window = (ModifyItemChildWindow)sender;

            // Check if the user clicked OK
            if (window.DialogResult == true)
            {
                ModelItem item = window.Item;

                if (window.NewItem)
                {
                    // Add the item
                    ContextModel.Instance.AddItem(item);
                }

                // Save the changes
                ContextModel.Instance.SaveChanges();
            }
            else
            {
                // If the user clicked cancel, revert
                ContextModel.Instance.CancelChanges();
            }
        }

        /// <summary>
        /// Event handler for when the user clicks a tag button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemTagButtonClick(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;

            // Get the tag from the control "Tag" property
            Tag tag = (Tag)control.Tag;

            // Find the model version of the tag so that we can
            // look it up in the tag list box
            ModelTag mt = (from t in ContextModel.Instance.UserTags
                           where t.TagID == tag.ID
                           select t).FirstOrDefault();

            // Switch to the appropriate tab
            MainPageTabControl.SelectedIndex = 1;

            // Select the item
            TagsListBox.SelectedItem = mt;
        }

        /// <summary>
        /// Called when the button to create a new list is clicked
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">event args</param>
        private void NewListButtonClick(object sender, RoutedEventArgs e)
        {
            ModifyListChildWindow newListWindow = new ModifyListChildWindow();
            newListWindow.NewList = true;

            newListWindow.Closed += new EventHandler(ModifyListWindowClosed);
            newListWindow.Show();
        }

        /// <summary>
        /// Displays UI to edit a list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditListButtonClick(object sender, RoutedEventArgs e)
        {
            Control control = sender as Control;
            ModifyListChildWindow newListWindow = new ModifyListChildWindow((DefaultScope.List)control.Tag);
            newListWindow.NewList = false;

            newListWindow.Closed += new EventHandler(ModifyListWindowClosed);
            newListWindow.Show();
        }

        /// <summary>
        /// Handles deleting a list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteListButtonClick(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;

            // Delete the list
            ContextModel.Instance.DeleteList((DefaultScope.List)control.Tag);

            // Commit the changes to the disk
            ContextModel.Instance.SaveChanges();
        }

        /// <summary>
        /// Called when the new list creation window is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ModifyListWindowClosed(object sender, EventArgs e)
        {
            ModifyListChildWindow newListWindow = sender as ModifyListChildWindow;

            bool? dialogResult = newListWindow.DialogResult;

            if (dialogResult == true)
            {
                // If the user clicked ok, add the new list
                ModelList list = newListWindow.List;

                if (newListWindow.NewList)
                {
                    try
                    {
                        ContextModel.Instance.AddList(list);
                    }
                    catch (ArgumentException)
                    {
                        // Warn the user that the list name is bad
                        MessageBox.Show("The Name for the new list must be unique");

                        // Redisplay the window
                        newListWindow.Show();
                    }
                }

                // Commit the change
                ContextModel.Instance.SaveChanges();
            }
            else
            {
                // Revert the change
                ContextModel.Instance.CancelChanges();
            }
        }

        /// <summary>
        /// Navigates to the item's list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemListButtonClick(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            DefaultScope.List list = (DefaultScope.List)control.Tag;

            ListsListBox.SelectedItem = list;
            MainPageTabControl.SelectedIndex = 0;
        }
	}
}
