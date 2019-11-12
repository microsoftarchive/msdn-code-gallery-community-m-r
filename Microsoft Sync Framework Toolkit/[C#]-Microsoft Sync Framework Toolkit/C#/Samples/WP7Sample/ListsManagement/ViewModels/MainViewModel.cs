// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace ListsManagement
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            // Insert code required on object creation below this point
            Items = new ObservableCollection<ItemViewModel>() {
                new ItemViewModel() { ListItemTitle = "Lists", ListItemDetail = "User defined lists", DetailPage="ListsView.xaml"},
                new ItemViewModel() { ListItemTitle = "Tags", ListItemDetail = "List of available tags", DetailPage="TagsView.xaml"},
                new ItemViewModel() { ListItemTitle = "By Completion Date", ListItemDetail = "Tasks nearing completion or past due", DetailPage="NearingCompletionView.xaml"},
                new ItemViewModel() { ListItemTitle = "Completed Tasks", ListItemDetail = "Completed and archived tasks", DetailPage="ArchivedItemsView.xaml"},
            };
        }

        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                _sampleProperty = value;
                NotifyPropertyChanged("SampleProperty");
            }
        }

        /// <summary> Sample ViewModel method; this method is invoked by a Behavior that is associated with it in the View</summary>
        public void SampleMethod()
        {
            SampleProperty = "SampleMethod invoked.";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}