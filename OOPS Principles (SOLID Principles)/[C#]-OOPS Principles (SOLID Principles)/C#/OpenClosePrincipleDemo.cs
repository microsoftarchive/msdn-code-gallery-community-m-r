using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLIDPrinciplesDemo
{
    //2. Open Close Principle
    // Here DataProvder is open for extension (extends to Sql, Oracle, Oledb Providers and so on..) and closed for manipulation
    abstract class DataProvider
    {
        public abstract  int OpenConnection();
        public abstract  int CloseConnection();
        public abstract int ExecuteCommand();
    }
    class SqlDataProvider : DataProvider
    {
        public override int OpenConnection()
        {
            Console.WriteLine("\nSql Connection opened successfully");
            return 1;
        }
        public override int CloseConnection()
        {
            Console.WriteLine("Sql Connection closed successfully");
            return 1;
        }
        public override int ExecuteCommand()
        {
            Console.WriteLine("Sql Command Executed successfully");
            return 1;
        }
    }
    class OracleDataProvider : DataProvider
    {
        public override int OpenConnection()
        {
            Console.WriteLine("Oracle Connection opened successfully");
            return 1;
        }
        public override int CloseConnection()
        {
            Console.WriteLine("Oracle Connection closed successfully");
            return 1;
        }
        public override int ExecuteCommand()
        {
            Console.WriteLine("Oracle Command Executed successfully");
            return 1;
        }
    }

    class OledbDataProvider : DataProvider
    {
        public override int OpenConnection()
        {
            Console.WriteLine("OLEDB Connection opened successfully");
            return 1;
        }
        public override int CloseConnection()
        {
            Console.WriteLine("OLEDB Connection closed successfully");
            return 1;
        }
        public override int ExecuteCommand()
        {
            Console.WriteLine("OEDB Command Executed successfully");
            return 1;
        }
    }
    class OpenClosePrincipleDemo
    {
        public static void OSPDemo()
        {
            Console.WriteLine("\n\nOpen Close Principle Demo ");

            DataProvider DataProviderObject = new SqlDataProvider();
            DataProviderObject.OpenConnection();
            DataProviderObject.ExecuteCommand();
            DataProviderObject.CloseConnection();

            DataProviderObject = new OracleDataProvider();
            DataProviderObject.OpenConnection();
            DataProviderObject.ExecuteCommand();
            DataProviderObject.CloseConnection();
        }
    }
}