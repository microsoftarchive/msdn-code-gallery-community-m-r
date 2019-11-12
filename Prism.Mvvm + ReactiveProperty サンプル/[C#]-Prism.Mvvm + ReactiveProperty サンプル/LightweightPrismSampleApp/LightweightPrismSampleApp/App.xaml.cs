using LightweightPrismSampleApp.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using System.Windows;

namespace LightweightPrismSampleApp
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private UnityContainer container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.container = new UnityContainer();
            this.container.RegisterType<AppContext>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<EventAggregator>(new ContainerControlledLifetimeManager());

            ViewModelLocationProvider.SetDefaultViewModelFactory(t => this.container.Resolve(t));
        }
    }
}
