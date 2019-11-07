using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MyEvents.Client.Organizer.Desktop.Services.Navigation
{
    /// <summary>
    /// Navigation controller
    /// </summary>
    public class NavigationController
    {
        private static readonly NavigationController instance = new NavigationController();

        private NavigationController() 
        {
        }

        /// <summary>
        /// Gets the instance of Navigation Controller.
        /// </summary>
        /// <value>The current instance.</value>
        public static NavigationController Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Stores the previous View to Navigate Back
        /// </summary>
        private Stack<UIElement> _previousView = new Stack<UIElement>();

        /// <summary>
        /// Stores the current View
        /// </summary>
        private UIElement _currentView;

        /// <summary>
        /// Stores the MainWindow Instance
        /// </summary>
        private MainWindow _mainWindow;

        /// <summary>
        /// Gets or sets the previous view.
        /// </summary>
        /// <value>The previous view.</value>
        public Stack<UIElement> PreviousView
        {
            get { return _previousView; }
            set { _previousView = value; }
        }

        /// <summary>
        /// Gets or sets the current view.
        /// </summary>
        /// <value>The current view.</value>
        public UIElement CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; }
        }

        /// <summary>
        /// Gets the app main window.
        /// </summary>
        /// <value>The app main window.</value>
        public MainWindow AppMainWindow
        {
            get
            {
                if (_mainWindow != null)
                {
                    return _mainWindow;
                }
                return GetMainWindow();
            }
        }


        /// <summary>
        /// Gets the Main Window content host.
        /// </summary>
        /// <value>The content host part.</value>
        public Grid ContentHost
        {
            get
            {
                if (_mainWindow != null)
                {
                    return _mainWindow.PARTContentHost;
                }
                return GetMainWindow().PARTContentHost;
            }
        }

        /// <summary>
        /// Navigates the back.
        /// </summary>
        public void NavigateBack()
        {
            ContentHost.Children.Clear();
            if (PreviousView.Peek() != null)
                CurrentView = PreviousView.Pop();
            ContentHost.Children.Add(CurrentView);
        }

        /// <summary>
        /// Navigates to the target view.
        /// </summary>
        /// <param name="view">The view.</param>
        public void NavigateTo(UIElement view)
        {
            if (CurrentView == null)
                CurrentView = ContentHost.Children.Count > 0 ? ContentHost.Children[0] : null;
            PreviousView.Push(CurrentView);
            CurrentView = null;
            CurrentView = view;
            ContentHost.Children.Clear();
            ContentHost.Children.Add(CurrentView);
        }

        /// <summary>
        /// Navigates to the target view without saving previous page.
        /// </summary>
        /// <param name="view">The view.</param>
        public void NavigateWithoutBackTo(UIElement view)
        {
            CurrentView = null;
            CurrentView = view;
            ContentHost.Children.Clear();
            ContentHost.Children.Add(CurrentView);
            if (PreviousView.Count>0)
            {
                PreviousView.Pop();
            }
           
        }

        /// <summary>
        /// Gets the current application main window.
        /// </summary>
        /// <returns>The current mainwindow</returns>
        private MainWindow GetMainWindow()
        {
            foreach (Window item in App.Current.Windows)
            {
                MainWindow window = item as MainWindow;
                if (window != null)
                {
                    _mainWindow = window;
                    return window;
                }
            }
            return null;
        }
    }
}
