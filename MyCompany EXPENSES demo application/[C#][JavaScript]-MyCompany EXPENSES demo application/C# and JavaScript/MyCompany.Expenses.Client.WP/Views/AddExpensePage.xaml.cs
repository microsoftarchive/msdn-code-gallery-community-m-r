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
    /// Add expense page
    /// </summary>
    public partial class AddExpensePage : PhoneApplicationPage
    {
        VMAddExpense vm;

        ApplicationBarIconButton saveExpenseButton;
        ApplicationBarIconButton takeImageButton;
        ApplicationBarIconButton trackRouteButton;
        /// <summary>
        /// Constructor
        /// </summary>
        public AddExpensePage()
        {
            InitializeComponent();
            this.vm = (VMAddExpense)this.DataContext;
        }

        /// <summary>
        /// Code executed when access to the page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                this.vm.InitializeData();
                GenerateAppBar();
            }
            else if (e.NavigationMode == NavigationMode.Back && this.vm.Type == ExpenseType.Travel)
            {
                this.vm.RefreshData();
            }
        }

        /// <summary>
        /// Regenerate appbar
        /// </summary>
        private void GenerateAppBar()
        {
            this.saveExpenseButton = new ApplicationBarIconButton();
            this.saveExpenseButton.IconUri = new Uri("/Assets/appbar.save.png", UriKind.Relative);
            this.saveExpenseButton.Text = AppResources.AppBarSaveText;
            this.saveExpenseButton.Click += saveExpensesButton_Click;

            this.takeImageButton = new ApplicationBarIconButton();
            this.takeImageButton.IconUri = new Uri("/Assets/appbar.camera.png", UriKind.Relative);
            this.takeImageButton.Text = AppResources.AddExpensesPageTakeImageText;
            this.takeImageButton.Click += takeImage_Click;

            this.ApplicationBar.Buttons.Clear();
            this.ApplicationBar.Buttons.Add(this.saveExpenseButton);
            this.ApplicationBar.Buttons.Add(this.takeImageButton);

            if (this.vm.Type == ExpenseType.Travel)
            {
                this.trackRouteButton = new ApplicationBarIconButton();
                this.trackRouteButton.IconUri = new Uri("/Assets/appbar.location.png", UriKind.Relative);
                this.trackRouteButton.Text = AppResources.AddExpensesPageTrackRouteText;
                this.trackRouteButton.Click += trackRoute_Click;
                this.ApplicationBar.Buttons.Add(this.trackRouteButton);
            }
        }


        void saveExpensesButton_Click(object sender, EventArgs e)
        {
            txtAmount.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtDescription.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtTitle.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            this.vm.SaveExpenseCommand.Execute(null);
        }

        void takeImage_Click(object sender, EventArgs e)
        {
            this.vm.TakePhotoCommand.Execute(null);
        }

        void trackRoute_Click(object sender, EventArgs e)
        {
            this.vm.TrackRouteCommand.Execute(null);
        }

    }
}