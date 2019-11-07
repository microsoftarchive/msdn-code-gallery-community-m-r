namespace MyCompany.Visitors.Client.UniversalApp.Services.Dispatcher
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Dispatcher wrapper implementation
    /// </summary>
    public class DispatcherService : IDispatcherService
    {
        /// <summary>
        /// Execute the specified action in UI Thread.
        /// </summary>
        /// <param name="action">Code to be executed.</param>
        public async Task InvokeUI(Action action)
        {
            await App.RootFrame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, new Windows.UI.Core.DispatchedHandler(action));
        }
    }
}
