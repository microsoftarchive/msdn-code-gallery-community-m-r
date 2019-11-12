using Microsoft.Practices.Prism.MefExtensions;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Windows;
using System.Reflection;
using Repository;

namespace PrimsModulityWithWPF
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            var executingAssembly = Assembly.GetExecutingAssembly();
            // Use current assembly when looking for MEF exports
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(executingAssembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(EmployeeModule.EmployeeModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ProductModule.ProductModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(RepositoryModule).Assembly));
        }

        protected override CompositionContainer CreateContainer()
        {
            var container = base.CreateContainer();
            container.ComposeExportedValue(container);
            return container;
        }
    }
}
