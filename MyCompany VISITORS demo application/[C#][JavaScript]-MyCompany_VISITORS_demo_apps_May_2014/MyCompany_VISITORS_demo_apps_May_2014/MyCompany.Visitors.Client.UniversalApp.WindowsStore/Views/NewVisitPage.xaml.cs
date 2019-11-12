namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views
{
    using System.Linq;
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel;
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views.Base;
    using Windows.UI.Xaml.Navigation;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewVisitPage : BasePage
    {
        /// <summary>
        /// Employee Id.
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Visitor Id.
        /// </summary>
        public int? VisitorId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public NewVisitPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            AppBar.IsOpen = false;
            base.OnNavigatedTo(e);

            var vm = (VMNewVisitPage)this.DataContext;
            vm.AreEnabledButtoms = true;

            // If it comes from seach visitor/employee.
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (EmployeeId.HasValue)
                {
                    await vm.InitializeEmployee((int) EmployeeId);
                }
                if(VisitorId.HasValue)
                {
                    await vm.InitializeVisitor((int) VisitorId);
                }
            }
            // If it comes from new visitor.
            else if (e.Parameter != null)
            {
                // If it was creating a Visit.
                if (!vm.IsCreatingVisit)
                {
                    vm.InitializeData();
                }

                RemoveLastFrames();

                var visitorId = 0;
                int.TryParse(e.Parameter.ToString(), out visitorId);
                if(visitorId != 0)
                {
                    await vm.InitializeVisitor(visitorId);
                }
            }

            // If it comes from main page or listing visits
            else if (e.NavigationMode == NavigationMode.New)
            {
                vm.InitializeData();
            }
        }

        /// <summary>
        /// Invoked when user leaves this page.
        /// </summary>
        /// <param name="e">Navigatoin event arguments</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if(e.NavigationMode == NavigationMode.Back)
            {
                var vm = (VMNewVisitPage)this.DataContext;
                vm.IsCreatingVisit = false;
            }
        }

        private void RemoveLastFrames()
        {
            try
            {
                string navigationHistory = Frame.GetNavigationState();
                var history = new List<string>(navigationHistory.Split(','));

                // GetByVisitor the index of the first NewVisit page.
                int indexFirstNewVisitPage = SearchNewVisitFrames(history, false);

                // GetByVisitor the index of the last NewVisit page.
                int indexLastNewVisitPage = SearchNewVisitFrames(history, true);
                
                if((indexFirstNewVisitPage != -1) && (indexLastNewVisitPage != -1) &&
                   (indexFirstNewVisitPage != indexLastNewVisitPage))
                {
                    history.RemoveRange(indexFirstNewVisitPage, indexLastNewVisitPage - indexFirstNewVisitPage);

                    // Update the number of pages and the current page.
                    var framesNumber = GetFramesNumber(history);
                    history[1] = framesNumber.ToString();
                    history[2] = (framesNumber - 1).ToString();

                    // Turn this into the required format.
                    string newHistory = String.Join<string>(",", history.AsEnumerable());

                    // Update the navigation.
                    Frame.SetNavigationState(newHistory);
                }
            }
            catch(Exception)
            {
            }
        }

        private int GetFramesNumber(List<string> history)
        {
            return history.Count(t => t.Contains("Views"));
        }

        private int SearchNewVisitFrames(List<string> history, bool searchTheLast)
        {
            bool found = false;
            int i;
            int indexResult = -1;

            if(!searchTheLast)
            {
                i = 0;
                while ((i < history.Count) && (!found))
                {
                    if (history[i].Contains("NewVisitPage"))
                    {
                        indexResult = i;
                        found = true;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            else
            {
                i = history.Count - 1;
                while ((i >= 0) && (!found))
                {
                    if(history[i].Contains("NewVisitPage"))
                    {
                        indexResult = i;
                        found = true;
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            return indexResult;
        }
    }
}
