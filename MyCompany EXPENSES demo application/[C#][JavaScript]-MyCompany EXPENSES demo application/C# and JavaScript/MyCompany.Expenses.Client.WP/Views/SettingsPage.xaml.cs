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
    /// Settings page
    /// </summary>
    public partial class SettingsPage : PhoneApplicationPage
    {
        ApplicationBarIconButton saveSettingsButton;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
            GenerateAppBar();
        }

        /// <summary>
        /// Regenerate appbar
        /// </summary>
        private void GenerateAppBar()
        {
            this.saveSettingsButton = new ApplicationBarIconButton();
            this.saveSettingsButton.IconUri = new Uri("/Assets/appbar.save.png", UriKind.Relative);
            this.saveSettingsButton.Text = AppResources.AppBarSaveText;
            this.saveSettingsButton.Click += saveSettingsButton_Click;

            this.ApplicationBar.Buttons.Clear();
            this.ApplicationBar.Buttons.Add(this.saveSettingsButton);
        }

        void saveSettingsButton_Click(object sender, EventArgs e)
        {
            txtServerUrl.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            chkTestMode.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            var vm = (VMSettings)this.DataContext;
            vm.SaveSettingsCommand.Execute(null);
        }
    }
}