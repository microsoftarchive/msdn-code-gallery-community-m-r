// <copyright file="Bootstrapper.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Bootstrapper.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;

    using Dialogs;
    using Infrastructure.Translators;
    using LAgger;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.UnityExtensions;
    using Microsoft.Practices.Unity;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;
    using RCE.Services;

    /// <summary>
    /// This is the Bootstrapper for the RCE application. It initializes the module catalog and injects services into the <seealso cref="IUnityContainer"/> container.
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// The configuration settings.
        /// </summary>
        private readonly IDictionary<string, string> settings;

        /// <summary>
        /// The <see cref="ILogger"/> used to log application events.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class. 
        /// </summary>
        /// <param name="settings">The settings added in the configuration.</param>
        /// <param name="logger">The logger.</param>
        public Bootstrapper(IDictionary<string, string> settings, ILogger logger)
            : base()
        {
            this.settings = settings;
            this.logger = logger;
        }

        /// <summary>
        /// Returns the module catalog that will be used to initialize the modules.
        /// </summary>
        /// <returns>An instance of <seealso cref="IModuleCatalog" /> that will be used to initialize the modules.</returns>
        protected override IModuleCatalog GetModuleCatalog()
        {
            IConfigurationService configurationService = this.Container.Resolve<IConfigurationService>();

            string moduleCatalog = configurationService.GetParameterValue("ModulesCatalog");

            Uri moduleCatelogUri = new Uri(moduleCatalog, UriKind.Relative);
            IModuleCatalog catalog = ModuleCatalog.CreateFromXaml(moduleCatelogUri);

            return catalog;
        }

        /// <summary>
        /// Configures the <seealso cref="IUnityContainer" /> container.
        /// </summary>
        protected override void ConfigureContainer()
        {
            IConfigurationService configurationService = new ConfigurationService(this.settings);

            this.Container.RegisterInstance(configurationService, new ContainerControlledLifetimeManager());

            base.ConfigureContainer();

            DataServiceTranslator.ContentNetworkPrefix = configurationService.GetContentDistributionNetworkPrefix();

            this.Container.RegisterInstance<ILogger>(this.logger, new ContainerControlledLifetimeManager());
            
            this.Container.RegisterInstance<Func<IErrorView>>(() => new ErrorView());

            this.Container.RegisterType<IShell, Shell>();

            this.Container.RegisterType<ISequenceModel, SequenceModel>();

            this.Container.RegisterType<IMenuButtonView, MenuButtonView>();
            this.Container.RegisterType<IMenuButtonViewModel, MenuButtonViewModel>();

            this.Container.RegisterType<IPersistenceService, IsolatedStoragePersistenceService>();
            this.Container.RegisterType<IWindowManager, FloatableWindowManager>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns> The shell of the application.</returns>
        protected override DependencyObject CreateShell()
        {
            ShellPresenter presenter = this.Container.Resolve<ShellPresenter>();

            UIElement shell = (UIElement)presenter.Shell;
            
            // Application.Current.RootVisual = shell;
            ((ContentControl)Application.Current.RootVisual).Content = shell;

            return shell;
        }
    }
}
