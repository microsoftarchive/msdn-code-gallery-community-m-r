namespace MVVMCalc
{
    using System.Windows;
    using MVVMCalc.View;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var v = new MainView();
            v.Show();
        }
    }
}
