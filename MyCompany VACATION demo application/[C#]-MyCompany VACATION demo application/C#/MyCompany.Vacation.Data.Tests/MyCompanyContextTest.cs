namespace MyCompany.Vacation.Data.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MyCompanyContextTest
    {
        [TestMethod]
        public void MyCompanyContext()
        {
            var context = new MyCompanyContext();
            context.Database.Initialize(true);  
        }
    }
}
