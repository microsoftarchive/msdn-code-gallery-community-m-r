using System.Collections.Generic;
namespace MyCompany.Common.Notification
{
    /// <summary>
    /// Notification service interface.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Sends an email using the specified template
        /// </summary>
        /// <param name="toName">receiver name</param>
        /// <param name="toEmail">receiver email</param>
        /// <param name="templateName">template name</param>
        /// <param name="subject">subject of the mail</param>
        /// <param name="images">images to include in mail</param>
        /// <param name="textSubstitutions">dictionary with test substitutions</param>
        void SendTemplate(string toName, string toEmail, string templateName, string subject, Dictionary<string, string> textSubstitutions, string[] images);
    }
}
