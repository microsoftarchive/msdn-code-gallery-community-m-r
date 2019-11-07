namespace MyCompany.Travel.Client.Desktop
{
    using MyCompany.Travel.Client.Desktop.Services.Security;
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                SecurityService.Login();
                // Start the application anyway to show error message
                Views.MainWindow main = new Views.MainWindow();
                main.Show();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                Views.ErrorWindow main = new Views.ErrorWindow();
                main.Show();
            }
        }
    }
}
