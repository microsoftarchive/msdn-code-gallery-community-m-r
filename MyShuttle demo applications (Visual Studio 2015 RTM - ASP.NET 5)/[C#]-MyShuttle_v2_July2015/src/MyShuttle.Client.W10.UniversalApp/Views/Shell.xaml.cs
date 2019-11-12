using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.W10.UniversalApp.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MyShuttle.Client.W10.UniversalApp.Views
{
    public sealed partial class Shell : Page
    {
        public ShellViewModel ViewModel { get; set; }

        public Shell(Frame frame)
        {

            this.InitializeComponent();
            this.ShellSplitView.Content = frame;
            frame.Navigated += Frame_Navigated;
            this.DataContext = this;
            ViewModel = new ShellViewModel();

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
            else
            {
                backButton.Click += BackButton_Click;
            }

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                Windows.UI.ViewManagement.StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                statusBar.HideAsync();
            }
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            GoBack();
            e.Handled = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            var frame = this.ShellSplitView.Content as Frame;

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private void HamburgerButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.ShellSplitView.IsPaneOpen = !this.ShellSplitView.IsPaneOpen;
        }

        public bool BackButtonVisible
        {
            get
            {
                return (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"));
            }
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var type = e.SourcePageType.ToString();
            foreach (var radioButton in RadioButtonContainer.Children.OfType<RadioButton>())
            {
                var command = radioButton.CommandParameter as String;
                if (string.IsNullOrEmpty(command))
                {
                    radioButton.IsChecked = false;
                }
                else
                {
                    radioButton.IsChecked = type
                        .EndsWith(command, StringComparison.CurrentCultureIgnoreCase);
                }
            }

            var frame = this.ShellSplitView.Content as Frame;
            backButton.Visibility = frame.CanGoBack && BackButtonVisible ? Visibility.Visible : Visibility.Collapsed;
            PageTitle.Text = ((Base.WindowsBasePage)frame.Content).Title;
        }

        ICommand _navCommand;

        public ICommand NavCommand
        {
            get
            {
                if (_navCommand == null)
                {
                    _navCommand = new MvxCommand<string>((ns) =>
                    {
                        var root = App.Current.GetType().Namespace;
                        if (!ns.StartsWith(root))
                        { ns = string.Format("{0}.ViewModels.{1}ViewModel", root, ns); }

                        // attempt to find type
                        Type type = default(Type);
                        try { type = Type.GetType(ns); }
                        catch { throw new InvalidCastException(ns ?? "Not set"); }

                        this.ShellSplitView.IsPaneOpen = false;
                        ViewModel.NavCommand.Execute(type);
                    });
                }
                return _navCommand;
            }
        }

    }
}
