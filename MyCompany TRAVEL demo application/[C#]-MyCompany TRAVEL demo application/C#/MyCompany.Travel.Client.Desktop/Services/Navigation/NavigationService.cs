namespace MyCompany.Travel.Client.Desktop.Services.Navigation
{
    using MyCompany.Travel.Client.Desktop.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// implementation of the contract for navigation service.
    /// </summary>
    public class NavigationService : INavigationService
    {
        private Stack<UIElement> backStack = new Stack<UIElement>();
        private UIElement currentElement;
        private MainWindow mainWindow;
        private Grid contentHost;

        /// <summary>
        /// Navigate to the previous content in the stack.
        /// </summary>
        public void NavigateBack()
        {
            this.NavigateBackStack();
        }

        /// <summary>
        /// Open Travel Request form
        /// <param name="travelRequest">travelRequest</param>
        /// </summary>
        public void NavigateToTravelRequest(TravelRequest travelRequest)
        {
            NavigateTo(new TravelRequestForm(travelRequest));
        }

        /// <summary>
        /// Open Travel Request list
        /// </summary>
        public void NavigateToTravelRequestList()
        {
            NavigateTo(new TravelList());
        }       

        /// <summary>
        /// Private Navigate method.
        /// </summary>
        /// <param name="view"></param>
        private void NavigateTo(UIElement view)
        {
            GetContentHost();

            if (this.currentElement == null)
                this.currentElement = this.contentHost.Children.Count > 0 ? this.contentHost.Children[0] : null;

            this.backStack.Push(this.currentElement);
            this.currentElement = null;
            this.currentElement = view;
            this.contentHost.Children.Clear();
            this.contentHost.Children.Add(this.currentElement);
        }


        /// <summary>
        /// Private navigate back method.
        /// </summary>
        private void NavigateBackStack()
        {
            this.contentHost.Children.Clear();
            if (this.backStack.Peek() != null)
                this.currentElement = this.backStack.Pop();
            this.contentHost.Children.Add(this.currentElement);
        }

        /// <summary>
        /// Auxiliar method to obtain the main window.
        /// </summary>
        /// <returns></returns>
        private MainWindow GetMainWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if ((window as MainWindow) != null)
                {
                    return (window as MainWindow);
                }
            }

            return null;
        }

        private void GetContentHost()
        {
            if (this.mainWindow == null)
                this.mainWindow = GetMainWindow();

            if (this.mainWindow != null && this.contentHost == null)
                this.contentHost = this.mainWindow.PARTContentHost;
        }
    }
}
