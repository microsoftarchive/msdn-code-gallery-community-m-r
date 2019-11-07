namespace MyCompany.Common.Notification
{
    using MyCompany.Common.CrossCutting;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Notification service
    /// </summary>
    public class BaseNotificationService : INotificationService
    {
        private readonly IEmailer _emailer;
        private readonly IEmailTemplatesRepository _emailTemplatesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseNotificationService" /> class.
        /// </summary>
        /// <param name="emailer">The emailer.</param>
        /// <param name="emailTemplatesRepository">The email templates repository.</param>
        public BaseNotificationService(IEmailer emailer, IEmailTemplatesRepository emailTemplatesRepository)
        {
            if (emailer == null)
                throw new ArgumentNullException("emailer");

            if (emailTemplatesRepository == null)
                throw new ArgumentNullException("emailTemplatesRepository");

            _emailer = emailer;
            _emailTemplatesRepository = emailTemplatesRepository;
        }

        /// <summary>
        /// Sends an email using the specified template
        /// </summary>
        /// <param name="toName">receiver name</param>
        /// <param name="toEmail">receiver email</param>
        /// <param name="templateName">template name</param>
        /// <param name="subject">subject of the mail</param>
        /// <param name="images">images to include in mail</param>
        /// <param name="textSubstitutions">dictionary with test substitutions</param>
        public void SendTemplate(string toName, string toEmail, string templateName, string subject, Dictionary<string, string> textSubstitutions, string[] images)
        {
            try
            {
                string body = _emailTemplatesRepository.ProcessTemplate(templateName, textSubstitutions);

                var mail = new Email()
                {
                    To = new Recipient() { Name = toName, Email = toEmail },
                    Body = body,
                    Subject = subject,
                    Images = images
                };
                _emailer.Send(mail);
            }
            catch (Exception ex)
            {
                TraceManager.TraceError(ex);
            }
        }
    }

}
