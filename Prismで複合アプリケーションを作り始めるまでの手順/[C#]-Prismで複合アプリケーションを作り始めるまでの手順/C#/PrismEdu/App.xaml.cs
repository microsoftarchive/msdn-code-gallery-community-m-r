using System.Windows;

namespace PrismEdu
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // 開始処理
            var b = new Bootstrapper();
            b.Run();
        }
    }
}
