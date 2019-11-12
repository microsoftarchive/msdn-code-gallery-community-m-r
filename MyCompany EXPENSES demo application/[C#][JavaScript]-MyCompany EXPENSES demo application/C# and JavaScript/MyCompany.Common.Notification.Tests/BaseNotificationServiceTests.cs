namespace MyCompany.Common.Notification.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class BaseNotificationServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TravelNotificationService_Constructor_NullEmailer_Test()
        {
            IEmailer emailer = null;
            IEmailTemplatesRepository templatesRepository = new EmailTemplatesInFileRepository(new TextMerger());

            var service = new BaseNotificationService(emailer, templatesRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TravelNotificationService_Constructor_NullEmailTemplatesRepository_Test()
        {
            IEmailer emailer = new Emailer();
            IEmailTemplatesRepository templatesRepository = null;

            var service = new BaseNotificationService(emailer, templatesRepository);
        }

        [TestMethod]
        public void Send_SendsAnEmail()
        {
            var mailer = new Emailer();
            var merger = new TextMerger();
            var repository = new EmailTemplatesInFileRepository(merger);
            var service = new BaseNotificationService(mailer, repository);
            var substitutions = new Dictionary<string, string>();
            substitutions.Add("APPLICATIONURL", "http://localhost:31332");
            substitutions.Add("TRAVELDETAILURL", "http://localhost:31332/#/user/travelDetail/133");

            service.SendTemplate("José Fernández", "jfernandez@plainconcepts.com", "TravelApproved", "Your travel request has been approved", substitutions, new string[] { "logo.png" });
            service.SendTemplate("José Fernández", "jfernandez@plainconcepts.com", "TravelDenied", "Your travel request has been denied", substitutions, new string[] { "logo.png" });
        }
    }
}
