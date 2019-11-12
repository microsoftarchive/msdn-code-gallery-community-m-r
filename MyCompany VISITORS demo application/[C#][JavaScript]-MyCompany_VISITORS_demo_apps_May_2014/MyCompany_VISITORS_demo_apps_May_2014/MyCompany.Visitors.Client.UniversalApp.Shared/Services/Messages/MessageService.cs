namespace MyCompany.Visitors.Client.UniversalApp.Services.Messages
{
    using System;
    using Windows.UI.Popups;

    /// <summary>
    /// Message Service Implementation.
    /// </summary>
    public class MessageService : IMessageService
    {
        /// <summary>
        /// ShowMessage.
        /// </summary>
        /// <param name="message">Dialog Message</param>
        /// <param name="title">Dialog Title</param>
        public async void ShowMessage(string message, string title)
        {
            var dialog = new MessageDialog(message, title);
            await dialog.ShowAsync();
        }
    }
}
