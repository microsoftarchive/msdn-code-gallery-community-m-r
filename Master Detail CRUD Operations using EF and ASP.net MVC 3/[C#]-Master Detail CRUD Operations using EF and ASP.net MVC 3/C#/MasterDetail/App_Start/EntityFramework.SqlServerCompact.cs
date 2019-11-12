using System.Data.Entity;
using System.Data.Entity.Database;
using System.Data.Entity.Infrastructure;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MasterDetail.App_Start.EntityFramework_SqlServerCompact), "Start")]

namespace MasterDetail.App_Start {
    public static class EntityFramework_SqlServerCompact {
        public static void Start() {
            DbDatabase.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
        }
    }
}
