namespace MyCompany.Common.Notification
{
    /// <summary>
    /// Emailer interface
    /// </summary>
    public interface IEmailer
    {

        /// <summary>
        /// Sends the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        void Send(Email email);
    }
}
