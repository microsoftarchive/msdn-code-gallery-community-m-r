namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Settings
{
    using MyCompany.Visitors.Client.UniversalApp.Settings;
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.Settings;
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.Controls;    
    using System.Net.Http;
    using Windows.ApplicationModel.Resources;
    using Windows.Foundation;
    using Windows.Storage;
    using Windows.UI.ApplicationSettings;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Media.Animation;
    using MyCompany.Visitors.Client.UniversalApp.ViewModel;

    /// <summary>
    /// Settings panel helper.
    /// </summary>
    public class SettingsPanelHelper
    {
        private SettingsPane settingsPane;
        private static SettingsPanelHelper instance;

        /// <summary>
        /// Instance the class.
        /// </summary>
        public static SettingsPanelHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new SettingsPanelHelper();
                return instance;
            }
        }

        /// <summary>
        /// This method creates the settings options.
        /// </summary>
        public void CreateSettingsOptions()
        {
            settingsPane = Windows.UI.ApplicationSettings.SettingsPane.GetForCurrentView();

            settingsPane.CommandsRequested += settingsPane_CommandsRequested;
        }

        private void settingsPane_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            UICommandInvokedHandler ConfigHandler = new UICommandInvokedHandler(onConfigCommand);
            UICommandInvokedHandler SignOutHandler = new UICommandInvokedHandler(onSignOutCommand);


            ResourceLoader loader = new ResourceLoader();
            SettingsCommand configurationCommand = new SettingsCommand(loader.GetString("Settings_Configuration"), loader.GetString("Settings_Configuration"), ConfigHandler);
            SettingsCommand signOutCommand = new SettingsCommand(loader.GetString("Settings_SignOut"), loader.GetString("Settings_SignOut"), SignOutHandler);

            args.Request.ApplicationCommands.Add(configurationCommand);
            args.Request.ApplicationCommands.Add(signOutCommand);
        }

        private SettingsPanelHelper()
        {
        }

        private void onConfigCommand(IUICommand command)
        {
            ConfigurationFlyout configurationPanel = new ConfigurationFlyout();
            configurationPanel.Show();
        }

        private void onSignOutCommand(IUICommand command)
        {
            AppSettings.RemoveKeyValue(AppSettings.STORAGEKEY_SECURITYTOKEN);
            AppSettings.EmployeeInformation = null;
            var locator = (App.Current.Resources["Locator"] as ViewModelLocator);
            locator.Register();
            locator.NavService.NavigateToAuthentication();
        }
    }
}
