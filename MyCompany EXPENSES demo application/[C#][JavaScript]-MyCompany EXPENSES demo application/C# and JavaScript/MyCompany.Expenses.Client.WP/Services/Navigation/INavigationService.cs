namespace MyCompany.Expenses.Client.WP.Services.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Device.Location;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Navigation contract.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Perform back stack navigation.
        /// </summary>
        void NavigateBack();

        /// <summary>
        /// Go back from track route to add expense with the trackPoints and trackLength.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="trackLength"></param>
        void NavigateBackToAddExpense(string from, string to, int trackLength);

        /// <summary>
        /// Navigate to add expense page.
        /// </summary>
        /// <param name="type">expense type to add</param>
        void NavigateToAddExpense(ExpenseType type);

        /// <summary>
        /// Navigate to track route expense page.
        /// </summary>
        void NavigateToTrackRoute();

        /// <summary>
        /// Navigate to expense detail page.
        /// </summary>
        /// <param name="expense">Expense</param>
        void NavigateToExpenseDetail(Expense expense);

        /// <summary>
        /// Navigate to authentication page and restart app.
        /// </summary>
        void NavigateToAuthPage();
    }
}
