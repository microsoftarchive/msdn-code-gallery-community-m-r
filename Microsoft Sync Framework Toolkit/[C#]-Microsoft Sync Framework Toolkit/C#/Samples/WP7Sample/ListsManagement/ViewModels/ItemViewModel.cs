// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ListsManagement
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _listItemTitle;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string ListItemTitle
        {
            get
            {
                return _listItemTitle;
            }
            set
            {
                _listItemTitle = value;
                NotifyPropertyChanged("ListItemTitle");
            }
        }

        private string _listItemDetail;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string ListItemDetail
        {
            get
            {
                return _listItemDetail;
            }
            set
            {
                _listItemDetail = value;
                NotifyPropertyChanged("ListItemDetail");
            }
        }

        private string _detailPage;

        public string DetailPage
        {
            get
            {
                return this._detailPage;
            }
            set
            {
                this._detailPage = value;
                NotifyPropertyChanged("DetailPage");
            }
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