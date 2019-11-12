//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
[assembly: System.CLSCompliant(true), System.Resources.NeutralResourcesLanguage("en")]

namespace SLBlobUploader.Control
{
    using System;
    using System.Windows;

    /// <summary>
    /// Blob upload application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Parameter identifier for SAS URL passed from aspx page.
        /// </summary>
        private const string SasUrlParameter = "sasUrl";

        /// <summary>
        /// Parameter identifier for SAS timeout interval passed from aspx page.
        /// </summary>
        private const string TimeoutIntervalParameter = "timeOutInterval";

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        /// <summary>
        /// Reports the error to DOM.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.ApplicationUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");
                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight 2 Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Handles the Startup event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string sasUrl = e.InitParams[SasUrlParameter];
            string timeOutInterval = e.InitParams[TimeoutIntervalParameter];

            if (!string.IsNullOrWhiteSpace(sasUrl) && !string.IsNullOrWhiteSpace(timeOutInterval))
            {
                this.RootVisual = new MainPage(new Uri(sasUrl, UriKind.Absolute), timeOutInterval);
            }
        }

        /// <summary>
        /// Handles the Exit event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Application_Exit(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the UnhandledException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.ApplicationUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
    }
}
