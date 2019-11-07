using System.Configuration;
using System.Net.Mime;

namespace MyCompany.Common.Notification
{
    using System;
    using System.IO;
    using System.Net.Mail;

    /// <summary>
    /// Emailer
    /// </summary>
    public class Emailer : IEmailer
    {
        /// <summary>
        /// Sends the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        public void Send(Email email)
        {
            string emailTo = email.To.Email;

            if(!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("demo-email-to")))
            {
                emailTo = ConfigurationManager.AppSettings.Get("demo-email-to");
            }

            var to = new MailAddress(emailTo, email.To.Name);
            string subject = email.Subject;
            string body = email.Body;

            var htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            foreach (var imageName in email.Images)
            {
                string basePath = Path.Combine(GetBasePath(), "EmailTemplates");
                var image = new LinkedResource(Path.Combine(basePath, imageName))
                                {
                                    TransferEncoding = TransferEncoding.Base64,
                                    ContentType = new ContentType("image/png"),
                                    ContentId = Path.GetFileNameWithoutExtension(imageName)
                                };
                htmlView.LinkedResources.Add(image);
            }

            var smtp = new SmtpClient();
            var message = new MailMessage()
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            message.To.Add(to);

            message.AlternateViews.Add(htmlView);
            smtp.Send(message);
        }

        private static string GetBasePath()
        {
            if (System.Web.HttpContext.Current == null)
                return AppDomain.CurrentDomain.BaseDirectory;
            else
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
        } 
    }
}
