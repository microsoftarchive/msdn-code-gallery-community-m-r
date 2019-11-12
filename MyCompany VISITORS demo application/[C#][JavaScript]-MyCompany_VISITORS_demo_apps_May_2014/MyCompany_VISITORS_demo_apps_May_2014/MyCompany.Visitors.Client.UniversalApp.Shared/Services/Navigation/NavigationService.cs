namespace MyCompany.Visitors.Client.UniversalApp.Services.Navigation
{
    using MyCompany.Visitors.Client.UniversalApp.Views;
#if WINDOWS_PHONE_APP    
    using MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Views;    
#endif
#if WINDOWS_APP    
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views;
#endif

    /// <summary>
    /// Navigation implementation.
    /// </summary>
    public class NavigationService : INavigationService
    {
#if WINDOWS_APP
        private int employeeId;
        private int visitorId;
#endif



        /// <summary>
        /// DataTransfer.
        /// </summary>
        public object DataTransfer { get; set; }

        /// <summary>
        /// Determine if the navigation service can go back.
        /// </summary>
        public bool CanGoBack
        {
            get { return App.RootFrame.CanGoBack; }
        }

        /// <summary>
        /// Navigate to the last page in the navigation stack.
        /// </summary>
        public void GoBack()
        {
            if (App.RootFrame.CanGoBack)
                App.RootFrame.GoBack();
        }

        /// <summary>
        /// This method navigate from the authentication page to main page.
        /// </summary>
        public void NavigateToMainPage()
        {
            App.RootFrame.Navigate(typeof(MainPage), null);
        }

        /// <summary>
        /// This method navigate from the settings page to auth page.
        /// </summary>
        public void NavigateToAuthentication()
        {
            App.RootFrame.Navigate(typeof(AuthPage), null);
        }

        /// <summary>
        /// This method navigate from the main page to the listing page.
        /// </summary>
        public void NavigateToVisitsListing(bool todayVisits = false)
        {
            App.RootFrame.Navigate(typeof(VisitsListingPage), todayVisits);
        }

        /// <summary>
        /// This method navigate from the listing page to the visit details page.
        /// </summary>
        public void NavigateToVisitDetailPage(int visitId)
        {
            App.RootFrame.Navigate(typeof(VisitDetailPage), visitId);
        }

#if WINDOWS_APP

        /// <summary>
        /// This method navigate from the visit listing page to the new visit page.
        /// </summary>
        public void NavigateToNewVisitPage()
        {
            App.RootFrame.Navigate(typeof (NewVisitPage));
        }

        /// <summary>
        /// This method navigate from the search employee page to the new visit page.
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        public void NavigateToNewVisitPageFromEmployeeSearch(int employeeId)
        {
            this.employeeId = employeeId;
            App.RootFrame.Navigated += NavigatingBackFromEmployeeSearch;
            App.RootFrame.GoBack();
        }

        /// <summary>
        /// This method navigate from the search visitor page to the new visit page.
        /// </summary>
        /// <param name="visitorId">Visitor Id</param>
        public void NavigateToNewVisitPageFromVisitorSearch(int visitorId)
        {
            this.visitorId = visitorId;
            App.RootFrame.Navigated += NavigatingBackFromVisitorSearch;
            App.RootFrame.GoBack();
        }

        /// <summary>
        /// This metod navigate from the new visitor page to the new visit page.
        /// </summary>
        /// <param name="visitorId"></param>
        public void NavigateToNewVisitPageFromNewVisitorPage(int visitorId)
        {
            App.RootFrame.Navigate(typeof(NewVisitPage), visitorId);
        }

        /// <summary>
        /// This method navigate from the new visit page to the search visitor page.
        /// </summary>
        public void NavigateToSearchVisitorPage()
        {
            App.RootFrame.Navigate(typeof(SearchVisitorPage), null);
        }

        /// <summary>
        /// This method navigate from the new visit page to the search visitor page.
        /// </summary>
        public void NavigateToSearchEmployeePage()
        {
            App.RootFrame.Navigate(typeof(SearchEmployeePage), null);
        }

        /// <summary>
        /// This method navigate from the visitors listing page to the new visitor page.
        /// </summary>
        public void NavigateToNewVisitorPage()
        {
            App.RootFrame.Navigate(typeof(NewVisitorPage));
        }

        private void NavigatingBackFromEmployeeSearch(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            App.RootFrame.Navigated -= NavigatingBackFromEmployeeSearch;
            var targetPage = (e.Content as NewVisitPage);
            targetPage.EmployeeId = this.employeeId;
            this.employeeId = 0;
        }

        private void NavigatingBackFromVisitorSearch(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            App.RootFrame.Navigated -= NavigatingBackFromVisitorSearch;
            var targetPage = (e.Content as NewVisitPage);
            targetPage.VisitorId = this.visitorId;
            this.visitorId = 0;
        }

#endif

    }
}
