using System.Data;
using System.Data.Linq.Mapping;
namespace Jabil.Framework.Common
{
  public class MyDataContext : DataContext
  {

    IDbConnection con;
    bool ownsConnection = false;
    bool connectionWasOpen = false;

    public MyDataContext(string connection, MappingSource mappingSource) :
      base(connection, mappingSource)
    {

      ownsConnection = true;
      con = (SqlConnection)this.Connection;
    }

    public MyDataContext(IDbConnection connection, MappingSource mappingSource) :
      base(connection, mappingSource)
    {
      
      ownsConnection = false;
      connectionWasOpen = (connection.State == ConnectionState.Open);
      con = (SqlConnection)this.Connection;
    }

    protected override void Dispose(bool disposing)
    {
      if (ownsConnection)
      {
        con.Dispose();
      }
      else if (!connectionWasOpen)
      {
        con.Close();
      }

      base.Dispose(disposing);
    }

  }
}