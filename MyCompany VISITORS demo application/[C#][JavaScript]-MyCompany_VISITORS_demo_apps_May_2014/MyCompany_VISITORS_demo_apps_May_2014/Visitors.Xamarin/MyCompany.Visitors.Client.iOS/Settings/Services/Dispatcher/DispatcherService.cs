using MonoTouch.UIKit;

namespace MyCompany.Visitors.Client.WindowsStore.Services.Dispatcher
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
            UIApplication.SharedApplication.InvokeOnMainThread(()=> action());
        }
    }
}
