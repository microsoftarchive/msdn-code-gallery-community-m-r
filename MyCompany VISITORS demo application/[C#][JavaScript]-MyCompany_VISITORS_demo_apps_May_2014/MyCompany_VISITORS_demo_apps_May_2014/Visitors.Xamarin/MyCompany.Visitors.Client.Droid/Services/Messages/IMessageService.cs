namespace MyCompany.Visitors.Client.WindowsStore.Services.Messages
{
    /// <summary>
    /// Message Service Contract.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// ShowMessage.
        /// </summary>
        /// <param name="message">Dialog Message</param>
        /// <param name="title">Dialog Title</param>
        void ShowMessage(string message, string title);
    }
}
