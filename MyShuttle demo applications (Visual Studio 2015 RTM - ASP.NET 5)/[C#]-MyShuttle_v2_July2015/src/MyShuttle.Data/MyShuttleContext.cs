
namespace MyShuttle.Data
{
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Metadata;
    using Microsoft.Framework.OptionsModel;
    using MyShuttle.Model;

    public class MyShuttleContext : IdentityDbContext<ApplicationUser>
    {
        public MyShuttleContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().Key(c => c.CustomerId);
            builder.Entity<Carrier>().Key(c => c.CarrierId);
            builder.Entity<Employee>().Key(e => e.EmployeeId);
            builder.Entity<Vehicle>().Key(v => v.VehicleId);
            builder.Entity<Driver>().Key(d => d.DriverId);
            builder.Entity<Ride>().Key(r => r.RideId);
            builder.Entity<OBDSecurityBeltWarning>().Key(r => r.OBDSecurityBeltWarningId);

            base.OnModelCreating(builder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<OBDSecurityBeltWarning> OBDSecurityBeltWarnings { get; set; }
    }


}