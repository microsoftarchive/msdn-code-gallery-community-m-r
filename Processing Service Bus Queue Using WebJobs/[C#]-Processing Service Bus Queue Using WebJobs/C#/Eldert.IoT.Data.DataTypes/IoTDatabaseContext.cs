using System.Data.Entity;

namespace Eldert.IoT.Data.DataTypes
{
    public class IoTDatabaseContext : DbContext
    {
        public IoTDatabaseContext() : base("name=IoTDatabaseContext")
        {
        }

        public virtual DbSet<ErrorAndWarning> ErrorAndWarningsEntries { get; set; }
    }
}
