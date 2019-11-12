
namespace MyCompany.Visitors.Data.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MyCompanyContextTest
    {
        [TestMethod]
        public void MyCompanyContext_should_work()
        {
            var context = new MyCompanyContext();
            context.Database.Initialize(true);    
        }
    }
}
