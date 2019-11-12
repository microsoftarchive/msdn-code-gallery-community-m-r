namespace MyCompany.Visitors.Data.Infrastructure
{
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Infraestructure.Initializers;
    using MyCompany.Vacation.Data.Infrastructure.Interceptors;
    using System.Data.Entity;
    using System.Data.Entity.SqlServer;

    class MyCompanyDbConfiguration
        :DbConfiguration
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
