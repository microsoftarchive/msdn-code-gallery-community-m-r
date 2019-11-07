using Autofac;
using System;
using System.Windows;

namespace MyShuttle.Client.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Lazy<IContainer> container = new Lazy<IContainer>(IocContainer.BuildContainer);

        public IContainer Container
        {
            get
            {
                return container.Value;
            }
        }
    }
}
