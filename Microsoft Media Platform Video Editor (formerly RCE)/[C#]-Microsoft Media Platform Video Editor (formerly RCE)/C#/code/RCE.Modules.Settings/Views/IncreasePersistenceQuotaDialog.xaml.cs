// <copyright file="IncreasePersistenceQuotaDialog.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IncreasePersistenceQuotaDialog.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Views
{
    using System.Windows.Controls;

    public partial class IncreasePersistenceQuotaDialog : ChildWindow, IIncreasePersistenceQuotaDialog
    {
        public IncreasePersistenceQuotaDialog()
        {
            InitializeComponent();
        }

        void IIncreasePersistenceQuotaDialog.ShowDialog()
        {
            this.Show();
        }

        void IIncreasePersistenceQuotaDialog.SetViewModel(object increasePersistenceQuotaViewModel)
        {
            this.DataContext = increasePersistenceQuotaViewModel;
        }

        void IIncreasePersistenceQuotaDialog.CloseDialog()
        {
            this.DialogResult = true;
        }
    }
}