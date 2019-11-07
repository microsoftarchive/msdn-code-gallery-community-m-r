namespace MyCompany.Expenses.Client.WP.Services.Navigation
{
    using MyCompany.Expenses.Client.WP.Settings;
    using MyCompany.Expenses.Client.WP.ViewModel;
    using MyCompany.Expenses.Client.WP.Views;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Device.Location;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Navigation;

    /// <summary>
    /// Navigation contract implementation
    /// </summary>
    public class NavigationService : INavigationService
    {
        private ExpenseType addExpenseExpenseType;
        private Expense expenseDetail;
        string from;
        string to;
        int trackLength;

        /// <summary>
        /// Perform back stack navigation.
        /// </summary>
        public void NavigateBack()
        {
            App.RootFrame.GoBack();
        }

        /// <summary>
        /// Go back from track route to add expense with the trackPoints and trackLength.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="trackLength"></param>
        public void NavigateBackToAddExpense(string from, string to, int trackLength)
        {
            this.from = from;
            this.to = to;
            this.trackLength = trackLength;
            App.RootFrame.Navigated += NavigateBackToAddExpenses_Navigated;
            App.RootFrame.GoBack();
        }

        /// <summary>
        /// Navigate to add expense page.
        /// </summary>
        /// <param name="type">expense type to add</param>
        public void NavigateToAddExpense(ExpenseType type)
        {
            this.addExpenseExpenseType = type;
            App.RootFrame.Navigated += NavigateToAddExpenses_Navigated;
            App.RootFrame.Navigate(new Uri("/Views/AddExpensePage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Navigate to track route expense page.
        /// </summary>
        public void NavigateToTrackRoute()
        {
            App.RootFrame.Navigate(new Uri("/Views/TrackRoutePage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Navigate to expense detail page.
        /// </summary>
        /// <param name="expense"></param>
        public void NavigateToExpenseDetail(Expense expense)
        {
            this.expenseDetail = expense;
            App.RootFrame.Navigated += NavigateToExpenseDetail_Navigated;
            App.RootFrame.Navigate(new Uri("/Views/ExpenseDetailPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Navigate to authentication page and restart app.
        /// </summary>
        public void NavigateToAuthPage()
        {
            App.RootFrame.RemoveBackEntry();
            App.RootFrame.Navigate(new Uri("/Views/AuthenticationPage.xaml", UriKind.Relative));
        }

        private void NavigateToAddExpenses_Navigated(object sender, NavigationEventArgs e)
        {
            App.RootFrame.Navigated -= NavigateToAddExpenses_Navigated;
            var page = (AddExpensePage)e.Content;
            var viewmodel = (VMAddExpense)page.DataContext;
            viewmodel.Type = this.addExpenseExpenseType;
        }

        private void NavigateBackToAddExpenses_Navigated(object sender, NavigationEventArgs e)
        {
            App.RootFrame.Navigated -= NavigateBackToAddExpenses_Navigated;
            var page = (AddExpensePage)e.Content;
            var viewmodel = (VMAddExpense)page.DataContext;
            if (viewmodel.Expense != null)
            {
                viewmodel.Expense.ExpenseTravel = new ExpenseTravel();
                viewmodel.Expense.ExpenseTravel.Distance = this.trackLength;
                viewmodel.Expense.ExpenseTravel.From = this.from;
                viewmodel.Expense.ExpenseTravel.To = this.to;
                viewmodel.Expense.Amount = this.trackLength * AppSettings.AmountPerKilometer;
            }
        }

        private void NavigateToExpenseDetail_Navigated(object sender, NavigationEventArgs e)
        {
            App.RootFrame.Navigated -= NavigateToExpenseDetail_Navigated;
            var page = (ExpenseDetailPage)e.Content;
            var viewmodel = (VMExpenseDetail)page.DataContext;
            viewmodel.Expense = this.expenseDetail;
        }
    }
}
