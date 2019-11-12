
namespace MyCompany.Travel.Data.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MyCompanyContextTest
    {
        [TestMethod]
        public void MyCompanyContext()
        {
            MyCompanyContext context = new MyCompanyContext();
            context.Database.Initialize(true);    
        }
    }
}
