// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ListsSample.Model;

namespace ListsSample
{
    public partial class ModifyListChildWindow : ChildWindow
    {
        private ModelList list;

        public ModifyListChildWindow()
        {
            InitializeComponent();

            list = ContextModel.Instance.GetNewList();
            NewList = true;

            this.Loaded += new RoutedEventHandler(NewListChildWindow_Loaded);
        }

        public ModifyListChildWindow(DefaultScope.List scopeList)
        {
            InitializeComponent();

            list = ContextModel.Instance.GetList(scopeList);
            NewList = true;

            this.Loaded += new RoutedEventHandler(NewListChildWindow_Loaded);
        }

        public ModelList List
        {
            get
            {
                return list;
            }
        }

        public bool NewList
        {
            get;
            set;
        }

        void NewListChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = list;

            NameTextBox.IsReadOnly = !NewList;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

