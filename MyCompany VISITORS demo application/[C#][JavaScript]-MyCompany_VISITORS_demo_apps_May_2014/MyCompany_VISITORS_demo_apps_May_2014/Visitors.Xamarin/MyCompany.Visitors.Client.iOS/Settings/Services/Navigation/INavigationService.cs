namespace MyCompany.Visitors.Client.WindowsStore.Services.Navigation
{
    /// <summary>
    /// Navigation contract.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Determine if the navigation service can go back.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Navigate to the last page in the navigation stack.
        /// </summary>
        void GoBack();

        /// <summary>
        /// This method navigate from the authentication page to main page.
        /// </summary>
        void NavigateToMainPage();

        /// <summary>
        /// This method navigate from the settings page to auth page.
        /// </summary>
        void NavigateToAuthentication();

        /// <summary>
        /// This method navigate from the main page to the listing page.
        /// </summary>
        /// <param name="todayVisits">If true, only get today visits.</param>
        void NavigateToVisitsListing(bool todayVisits = false);

        /// <summary>
        /// This method navigate from the listing page to the visit details page.
        /// </summary>
        void NavigateToVisitDetailPage(int visitId);

        /// <summary>
        /// This method navigate from the visit listing page to the new visit page.
        /// </summary>
        void NavigateToNewVisitPage();

        /// <summary>
        /// This method navigate from the search employee page to the new visit page.
        /// </summary>
        /// <param name="employeeId"></param>
        void NavigateToNewVisitPageFromEmployeeSearch(int employeeId);

        /// <summary>
        /// This method navigate from the search visitor page to the new visit page.
        /// </summary>
        /// <param name="visitorId">Visitor Id</param>
        void NavigateToNewVisitPageFromVisitorSearch(int visitorId);

        /// <summary>
        /// This method navigate from the new visit page to the search visitor page.
        /// </summary>
        void NavigateToSearchVisitorPage();

        /// <summary>
        /// This method navigate from the new visit page to the search employee page.
        /// </summary>
        void NavigateToSearchEmployeePage();

        /// <summary>
        /// This method navigate from the visitors listing page to the new visitor page.
        /// </summary>
        void NavigateToNewVisitorPage();

        /// <summary>
        /// This method navigate from the new visitor page to the new visit page.
        /// </summary>
        /// <param name="visitorId"></param>
        void NavigateToNewVisitPageFromNewVisitorPage(int visitorId);
    }
}
