
namespace MyCompany.Travel.Data.Infrastructure
{
    using MyCompany.Travel.Data.Infrastructure.Initializers;
    using MyCompany.Travel.Data.Infrastructure.Interceptors;
    using System.Data.Entity;
    using System.Data.Entity.SqlServer;

    class MyCompanyDbConfiguration
        : DbConfiguration
    {
        public MyCompanyDbConfiguration()
        {
            SetDatabaseInitializer<MyCompanyContext>(new MyCompanyContextInitializer());
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());

            //Un-comment next line to test execution strategy!
            //AddInterceptor(new ConnectionBreakInterceptor());
        }
    }
}
