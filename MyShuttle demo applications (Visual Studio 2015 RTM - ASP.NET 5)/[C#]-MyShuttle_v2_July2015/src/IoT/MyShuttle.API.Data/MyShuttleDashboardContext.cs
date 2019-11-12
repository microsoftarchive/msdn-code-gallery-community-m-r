using MyShuttle.API.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data
{
    public class MyShuttleDashboardContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Ride> Rides { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ride>().HasKey(r => r.RideId);
            modelBuilder.Entity<Ride>().Property(r => r.EmployeeId).HasColumnName("EmployeeId");

            modelBuilder.Entity<Driver>().HasKey(d => d.DriverId);
            modelBuilder.Entity<Vehicle>().HasKey(v => v.VehicleId);
        }
    }
}
