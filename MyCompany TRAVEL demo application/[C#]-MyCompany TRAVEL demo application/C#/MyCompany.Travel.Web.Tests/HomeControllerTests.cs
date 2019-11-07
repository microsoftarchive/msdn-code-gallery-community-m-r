namespace MyCompany.Travel.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Travel.Web.Controllers;
    using System.Web.Mvc;

    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void HomeController_Index_Test()
        {
            var target = new HomeController();
            var result = target.Index();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}