namespace MyCompany.Expenses.Data.Infraestructure
{
    using MyCompany.Expenses.Data.Infrastructure.Initializers;
    using MyCompany.Expenses.Data.Infrastructure.Interceptors;
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
            //Interceptor(new ConnectionBreakInterceptor());
        }
    }
}
