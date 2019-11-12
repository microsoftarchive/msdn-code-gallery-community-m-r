
namespace MyShuttle.MobileServices.Models
{
    using Microsoft.WindowsAzure.Mobile.Service;
    using Microsoft.WindowsAzure.Mobile.Service.Tables;
    using MyShuttle.MobileServices.DataObjects;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class MobileServiceContext : DbContext
    {
        public MobileServiceContext()
            : base(ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ToString())
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Ride> Rides { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = ServiceSettingsDictionary.GetSchemaName();
            if (!string.IsNullOrEmpty(schema))
            {
                modelBuilder.HasDefaultSchema("dbo");
            }

            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }
    }

}
