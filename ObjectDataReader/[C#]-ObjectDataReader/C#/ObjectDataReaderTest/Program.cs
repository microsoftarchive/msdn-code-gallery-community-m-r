using Microsoft.Samples.ObjectDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;

namespace ObjectDataReaderTest
{
  public static class ObjectExtensions
  {
    public static T Cast<T>(this Object o)
    {
      return (T)o;
    }
    public static int ExecuteNonQuery(this ObjectContext db, string sql)//, params object[] parameters)
    {
      var con = db.Connection.Cast<EntityConnection>().StoreConnection;
      if (con.State != ConnectionState.Open) con.Open();
      var cmd = con.CreateCommand();
      cmd.CommandText = sql;

      return cmd.ExecuteNonQuery();
      

    }
  }
  class Program
  {
    static void Main(string[] args)
    {
      DateTime t = DateTime.Now;

      RunComplexPropertyTest();

      RunTVPEntityQuery(); Console.WriteLine("RunTVPEntityQuery {0}ms", DateTime.Now.Subtract(t).TotalMilliseconds);
      RunL2SMergeTest(); Console.WriteLine("RunL2SMergeTest {0}ms", DateTime.Now.Subtract(t).TotalMilliseconds);
      RunTVP(); Console.WriteLine("RunTVP {0}ms", DateTime.Now.Subtract(t).TotalMilliseconds);
      RunEFSqlBulkCopy(); Console.WriteLine("RunEFSqlBulkCopy {0}ms", DateTime.Now.Subtract(t).TotalMilliseconds);
      RunL2s(); Console.WriteLine("RunL2s {0}ms", DateTime.Now.Subtract(t).TotalMilliseconds);
      LongReaderSmokeTest(1000 * 1000); Console.WriteLine("LongReader 1M Rows {0}ms", DateTime.Now.Subtract(t).TotalMilliseconds);
      ShortReaderSmokeTest(1000 * 1000); Console.WriteLine("Short 1M Rows {0}ms", DateTime.Now.Subtract(t).TotalMilliseconds);

      Console.WriteLine("Complete.  Hit any key to continue.");
      Console.ReadKey();

    }

    static void RunComplexPropertyTest()
    {
      using (var db = new AdventureWorksEntities())
      {
        var q = db.SalesOrderHeader.Take(100);


        var dt = q.ToDataTable();

        foreach (DataColumn c in dt.Columns)
        {
          Console.WriteLine("{0} {1}", c.ColumnName, c.DataType);
        }
      }
    }


    static void RunL2SMergeTest()
    {
      
      using (var db = new L2S.AdventureWorksDataContext())
      using (var con = db.Connection)
      {
        //db.ObjectTrackingEnabled = false;
        var lo = new System.Data.Linq.DataLoadOptions();
        lo.LoadWith<L2S.SalesOrderHeader>(o => o.SalesOrderDetails);
        db.LoadOptions = lo;
        db.Connection.Open();

        var customerId = (from o in db.SalesOrderHeaders
                          where o.SalesOrderDetails.Count() > 2
                          select o).Take(1).First().CustomerID;
              
        
        var q = from o in db.SalesOrderHeaders
                where o.CustomerID == customerId
                select o;

        var orders = q.ToList();

        foreach (var o in orders)
        {
          foreach (var od in o.SalesOrderDetails)
          {
            od.UnitPrice = od.UnitPrice * 1.04M;
          }
        }

        string sql = @"
declare @output Sales_SalesOrderDetail_type

MERGE Sales.SalesOrderDetail AS target
USING 
( 
  SELECT
    SalesOrderID,
    SalesOrderDetailID,
    UnitPrice
  FROM @OrderDetails 
) AS source (SalesOrderID, SalesOrderDetailID, UnitPrice)
ON 
(
      target.SalesOrderID = source.SalesOrderID
  and target.SalesOrderDetailID = source.SalesOrderDetailID
)
WHEN MATCHED 
    THEN UPDATE SET target.UnitPrice = source.UnitPrice, 
                    target.ModifiedDate = GETDATE()
OUTPUT inserted.*
into @output;

select * from @output;
";

        var cmd = new SqlCommand(sql, (SqlConnection)db.Connection);
        var pOrderDetails = cmd.Parameters.Add(new SqlParameter("@OrderDetails", SqlDbType.Structured));
        pOrderDetails.TypeName = "Sales_SalesOrderDetail_type";
        

        var sql2 = @"
if not exists (select * from sys.table_types where name = 'Sales_SalesOrderDetail_type')
CREATE TYPE Sales_SalesOrderDetail_type as TABLE
(
  [SalesOrderID] [int] NOT NULL,
  [SalesOrderDetailID] [int]  NOT NULL,
  [CarrierTrackingNumber] [nvarchar](25) NULL,
  [OrderQty] [smallint] NOT NULL,
  [ProductID] [int] NOT NULL,
  [SpecialOfferID] [int] NOT NULL,
  [UnitPrice] [money] NOT NULL,
  [UnitPriceDiscount] [money] NOT NULL,
  [LineTotal]  MONEY,
  [rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
  [ModifiedDate] [datetime] NOT NULL
)
";
        db.ExecuteCommand(sql2);
        //project the results into an Anonymous type matching the TVP declaration
        var dtq = from od in orders.SelectMany(o => o.SalesOrderDetails).Take(1000)
                  select new 
                  {
                    SalesOrderID = od.SalesOrderID,
                    SalesOrderDetailId = od.SalesOrderDetailID,
                    CarrierTrackingNumber = od.CarrierTrackingNumber,
                    OrderQty = od.OrderQty,
                    ProductID = od.ProductID,
                    SpecialOfferID = od.SpecialOfferID,
                    UnitPrice = od.UnitPrice,
                    UnitPriceDiscount = od.UnitPriceDiscount,
                    LineTotal = od.LineTotal,
                    rowguid = od.rowguid,
                    ModifiedDate = od.ModifiedDate
                  };

        var i = 0;

        var f = new { a = i++, b = i++, c = i++ };

        //flatten the all the OrderDetails into a single DataReader for sending to the server
        pOrderDetails.Value = dtq.AsDataReader();

        IList<L2S.SalesOrderDetail> results;
        try
        {
            using (var dr = cmd.ExecuteReader())
            {
                results = db.Translate<L2S.SalesOrderDetail>(dr).ToList();
            }
        }
        catch (SqlException ex)
        {
            throw;
            
        }

        
        foreach (var r in results)
        {
          Console.WriteLine("{0} {1}",r.SalesOrderDetailID,r.UnitPrice);

        }

        if (dtq.Count() != results.Count())
        {
          throw new InvalidOperationException("Wrong number of rows affected by MERGE");
        }
        
        

 
      }
    }

    static void DumpQuery<T>(IQueryable<T> q)
    {
      using (var dr = q.AsDataReader())
      while (dr.Read())
      {
        for (int i = 0; i < dr.FieldCount; i++)
        {
          Console.WriteLine("{0} ({1}) [{2}]", dr.GetName(i), dr.GetDataTypeName(i), dr[i] == null ? "(null)" : dr[i]);
        }
        Console.WriteLine("--------");
      }
    }


    static void RunTVPEntityQuery()
    {

      using (var db = new L2S.AdventureWorksDataContext("Data Source=(local);Database=AdventureWorks2012;Integrated Security=true"))
      {
        db.Connection.Open();
        SqlConnection con = (SqlConnection)db.Connection;

        var sql = @"
select *
from Sales.SalesOrderDetail 
where SalesOrderID in (select Value from @ids)
";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlParameter pIds = cmd.Parameters.Add(new SqlParameter("@ids", SqlDbType.Structured));

        db.ExecuteCommand("if not exists (select * from sys.table_types where name = 'Int_TableType') CREATE TYPE Int_TableType AS TABLE(Value int NOT NULL)");
        pIds.TypeName = "Int_TableType";

        //create a list of ID's
        var ids = Enumerable.Range(43659, 1000);
        pIds.Value = ids.AsDataReader();

        List<L2S.SalesOrderDetail> od;
        using (var rdr = cmd.ExecuteReader())
        {
          od = db.Translate<L2S.SalesOrderDetail>(rdr).ToList();
        }
        

        Console.WriteLine(od.Count());
      }
    }
    static void RunTVP()
    {


        using (SqlConnection con = new SqlConnection("Data Source=(local);Database=AdventureWorks2012;Integrated Security=true"))
        {
          con.Open();

          var sql =@"
select max(LineTotal) 
from Sales.SalesOrderDetail 
where SalesOrderID in (select Value from @ids)
";
          SqlCommand cmd = new SqlCommand(sql, con);
          SqlParameter pIds = cmd.Parameters.Add(new SqlParameter("@ids", SqlDbType.Structured));

          //created with this DDL: CREATE TYPE Int_TableType AS TABLE(Value int NOT NULL)
          pIds.TypeName = "Int_TableType";

          //create a list of ID's
          var ids = Enumerable.Range(43659, 1000);
          pIds.Value = ids.AsDataReader();

          object val = cmd.ExecuteScalar();

          Console.WriteLine(val);
        }
      


    }


    static void RunEFSqlBulkCopy()
    {
      SqlConnection con;
      using (var db = new AdventureWorksEntities())
      {        
        db.Connection.Open();
        con = db.Connection.Cast<EntityConnection>().StoreConnection.Cast<SqlConnection>();
        db.ExecuteNonQuery(
@"set XACT_ABORT on
if object_id('tempdb..SalesOrderDetail') is not null drop table tempdb..SalesOrderDetail 
select * into tempdb..SalesOrderDetail from Sales.SalesOrderDetail where 1=2");

        var q2 = from od in db.SalesOrderDetail
                 select new
                 {
                   od.SalesOrderID,
                   od.SalesOrderDetailID,
                   od.CarrierTrackingNumber,
                   od.LineTotal,
                   od.ModifiedDate,
                   od.OrderQty,
                   od.Product.ProductID,
                   od.rowguid,
                   od.SpecialOfferID,
                   od.UnitPrice,
                   od.UnitPriceDiscount
                 };

        var dr = q2.AsDataReader();

       // DumpQuery(q2.Take(1));

        SqlBulkCopy bc = new SqlBulkCopy(con,
          SqlBulkCopyOptions.KeepIdentity
          | SqlBulkCopyOptions.FireTriggers
          | SqlBulkCopyOptions.KeepNulls
          | SqlBulkCopyOptions.TableLock
          | SqlBulkCopyOptions.CheckConstraints, null
          );
        for (int i = 0; i < dr.FieldCount; i++)
        {

          string colName = dr.GetName(i);
          Console.WriteLine(colName);
          bc.ColumnMappings.Add(colName, colName);
        }

        bc.BatchSize = 10000;
        bc.DestinationTableName = "tempdb..SalesOrderDetail";
        bc.NotifyAfter = 10000;
        bc.SqlRowsCopied += new SqlRowsCopiedEventHandler((s,a) => Console.WriteLine(a.RowsCopied) );
        bc.WriteToServer(dr);

        bc.Close();

      }
    }
    
    
    static void LongReaderSmokeTest(int totalRows)
    {
      
      var q = from od in Enumerable.Range(1,totalRows)
               select new
               {
                 SalesOrderID=od,
                 SalesOrderDetailID=od,
                 CarrierTrackingNumber=od.ToString(),
                 LineTotal=(decimal)od,
                 ModifiedDate=DateTime.Now,
                 OrderQty=od,
                 ProductID=od,
                 rowguid= Guid.NewGuid(),
                 SpecialOfferID=od,
                 UnitPrice=(decimal)od,
                 UnitPriceDiscount = (decimal)od
               };

      var dr = q.AsDataReader();
      long sum = 0;
      while (dr.Read())
      {
          var SaledOrderId = dr.GetInt32(0);
          var SalesOrderDetailID = dr.GetInt32(1);
          var LineTotal = dr.GetDecimal(3);
        //for (int i = 0; i < dr.FieldCount; i++)
        //{
        //  var o = dr.GetValue(i);
        //  if (o is Int32)
        //  {
        //    sum += (int)o;
        //  }
        //}
      }
      if (sum == 23)
      {
        Console.WriteLine(sum);
      }


    
    }

    static void ShortReaderSmokeTest(int totalRows)
    {
    for (int iter = 0; iter < totalRows/10; iter++)
      {
        var q = from od in Enumerable.Range(1, 10)
                select new
                {
                  SalesOrderID = od,
                  SalesOrderDetailID = od,
                  CarrierTrackingNumber = od.ToString(),
                  LineTotal = (decimal)od,
                  ModifiedDate = DateTime.Now,
                  OrderQty = od,
                  ProductID = od,
                  rowguid = Guid.NewGuid(),
                  SpecialOfferID = od,
                  UnitPrice = (decimal)od,
                  UnitPriceDiscount = (decimal)od
                };

  
        var dr = q.AsDataReader();
        long sum = 0;
        while (dr.Read())
        {
          for (int i = 0; i < dr.FieldCount; i++)
          {
            var o = dr.GetValue(i);
            if (o is Int32)
            {
              sum += (int)o;
            }
          }
        }
        if (sum == 23)
        {
          Console.WriteLine(sum);
        }
      }



    }
   
    static void RunL2s()
    {
      using (var db = new testDataContext())
      {

        db.Log = Console.Out;

        var q = from t in db.tables
                select new
                {
                  TableName=t.name,
                  ColumnCount=t.all_columns.Count()
                };
        foreach (var t in q)
        {
          Console.WriteLine("{0} {1}", t.TableName, t.ColumnCount);

        }
      }
      using (var otherDB = new other.otherDataContext())
      {
        var prods = from p in otherDB.Products
                    select p;


        DataTable t = prods.Take(10).ToDataTable();
        

        t.WriteXml(Console.Out);
      }
      
    }

  }
}
