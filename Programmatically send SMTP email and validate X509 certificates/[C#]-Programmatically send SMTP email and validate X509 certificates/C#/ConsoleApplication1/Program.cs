using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SmtpClientEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("frommailaddress@domain.com");
            mail.To.Add("Tomailaddress@domain.com");
            mail.Subject = "Test Mail - SmtpClientEmail";
            mail.Body = "This is for testing SMTP mail from SmtpClientEmail";

            SmtpClient smtpServer = new SmtpClient("smtpserveraddress.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("frommailaddress@domain.com", "password");
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtpServer.Send(mail);
        }
    }
}
