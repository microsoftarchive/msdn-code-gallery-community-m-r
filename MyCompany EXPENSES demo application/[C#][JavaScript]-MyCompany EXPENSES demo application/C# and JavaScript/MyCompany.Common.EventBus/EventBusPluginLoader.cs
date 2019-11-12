
namespace MyCompany.Common.EventBus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using MyCompany.Common.CrossCutting;

    /// <summary>
    /// AzureEventBus Plugin Loader
    /// </summary>
    public class EventBusPluginLoader : IDisposable
    {
        [ImportMany(typeof(IEventBus))]
        IEnumerable<IEventBus> eventBusCollection = null;

        CompositionContainer _container;

        /// <summary>
        /// Initialize Plugins that the method get as param
        /// </summary>
        public void Initialize(IEnumerable<IEventBus> eventBusCollection)
        {
            TraceManager.TraceInfo("Initialize Event Bus Plugins");

            if (eventBusCollection != null)
            {
                TraceManager.TraceInfo("Initialize Plugins");

                foreach (var eventBus in eventBusCollection)
                {
                    Task.Factory.StartNew(() =>
                    {
                        eventBus.RegisterHandler();
                    });
                }
            }

        }

        /// <summary>
        /// Initialize Plugins. Load and prepare them to handle events!
        /// </summary>
        public void DynamicInitialize()
        {
            TraceManager.TraceInfo("Initialize Event Bus Plugins");

            if (LoadPlugins())
            {
                Initialize(eventBusCollection);
            }
        }

        bool LoadPlugins()
        {
            bool result = false;

            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the EventBusPlugin class
            catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            try
            {
                //Fill the imports of this object
                _container.ComposeParts(this);
                result = true;
            }
            catch (CompositionException compositionException)
            {
                TraceManager.TraceError(compositionException);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && _container != null)
                _container.Dispose();
        }
    }
}
