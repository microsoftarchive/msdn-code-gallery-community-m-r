
namespace MyCompany.Travel.Data.Infrastructure.Interceptors
{
    using System.Data.Entity.Infrastructure.Interception;

    class ConnectionBreakInterceptor
        :IDbCommandInterceptor
    {
        public void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            //add a error in the command ( 40501:The service is currently busy ) to 
            //test if the execution strategy works correctly! If command logger is setted to console
            //you can view the retries produced for configured execution strategy
            command.CommandText = string.Format("{0};{1}", command.CommandText, "RAISERROR(40501,18,1)");
        }

        public void NonQueryExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
        }

       
        public void ScalarExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }
    }
}
