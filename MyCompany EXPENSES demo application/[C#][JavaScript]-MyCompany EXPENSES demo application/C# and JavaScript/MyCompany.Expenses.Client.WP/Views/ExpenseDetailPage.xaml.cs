namespace MyCompany.Expenses.Client.WP.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using MyCompany.Expenses.Client.WP.Resources;
    using MyCompany.Expenses.Client.WP.ViewModel;

    /// <summary>
    /// Expense Detail Page
    /// </summary>
    public partial class ExpenseDetailPage : PhoneApplicationPage
    {
        VMExpenseDetail vm;

        ApplicationBarIconButton pinMenuItem;
        ApplicationBarIconButton unpinMenuItem;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExpenseDetailPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OnNavigatedTo
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string value;
            vm = ((VMExpenseDetail)this.DataContext);
            if (NavigationContext.QueryString.TryGetValue("expenseId", out value))
            {
                int expenseId;
                if (int.TryParse(value, out expenseId))
                {
                    App.RootFrame.RemoveBackEntry();
                    vm.PropertyChanged += vm_PropertyChanged;
                    vm.LoadExpense(expenseId);
                }
            }
            else
                GenerateAppBar();
        }

        /// <summary>
        /// OnNavigatedFrom
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            vm.PropertyChanged += vm_PropertyChanged;
        }

        void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Expense")
            {
                GenerateAppBar();
            }
        }
        
        private void GenerateAppBar()
        {
            this.ApplicationBar.Buttons.Clear();

            if (vm.IsPinned)
            {
                this.unpinMenuItem = new ApplicationBarIconButton();
                this.unpinMenuItem.IconUri = new Uri("/Assets/appbar.pin.remove.png", UriKind.Relative);
                this.unpinMenuItem.Text = AppResources.AppBarUnpinText;
                this.unpinMenuItem.Click += Unpin_Click;

                this.ApplicationBar.Buttons.Add(this.unpinMenuItem);
            }
            else
            {
                this.pinMenuItem = new ApplicationBarIconButton();
                this.pinMenuItem.IconUri = new Uri("/Assets/appbar.pin.png", UriKind.Relative);
                this.pinMenuItem.Text = AppResources.AppBarPinText;
                this.pinMenuItem.Click += Pin_Click;

                this.ApplicationBar.Buttons.Add(this.pinMenuItem);
            }
        }

        private void Pin_Click(object sender, EventArgs e)
        {
            vm.PinExpense();
        }

        private void Unpin_Click(object sender, EventArgs e)
        {
            vm.UnpinExpense();
            GenerateAppBar();
        }
    }
}