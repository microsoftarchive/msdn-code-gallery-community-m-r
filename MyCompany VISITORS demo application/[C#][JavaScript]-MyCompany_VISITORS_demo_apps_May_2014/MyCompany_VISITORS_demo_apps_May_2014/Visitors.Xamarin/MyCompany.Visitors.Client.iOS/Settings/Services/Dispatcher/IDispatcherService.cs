namespace MyCompany.Visitors.Client.WindowsStore.Services.Dispatcher
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Dispatcher wrapper contract.
    /// </summary>
    public interface IDispatcherService
    {
        /// <summary>
        /// Execute the specified action in UI Thread.
        /// </summary>
        /// <param name="action">Code to be executed.</param>
        Task InvokeUI(Action action);
    }
}
