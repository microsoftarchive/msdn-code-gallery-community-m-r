using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyCompany.Common.Notification.Tests
{
    [TestClass]
    public class EmailerTests
    {
        [TestMethod]
        public void Send_SendsAnEmail()
        {
            var mailer = new Emailer();
            var email = new Email()
            {
                Body = "test",
                Subject = "test",
                To = new Recipient() { Name = "Fake", Email = "dummy@dummy.com" }
            };
            mailer.Send(email);
        }
    }
}
