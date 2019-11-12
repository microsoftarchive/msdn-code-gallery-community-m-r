using System;
using System.Linq;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.DependencyInjection;

namespace Visitors
{
    /// <summary>
    /// Summary description for VisitorContext
    /// </summary>
    public class VisitorContext : DbContext
    {
        IConfiguration _configuration;
        public VisitorContext(IServiceProvider sp, IConfiguration configuration) : base(sp)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptions builder)
        {
            builder.UseSqlServer(_configuration.Get("Data:DefaultConnection:ConnectionString"));
        }

        /// <summary>
        /// Employee Collection
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Employee Picture Collection
        /// </summary>
        public DbSet<EmployeePicture> EmployeePictures { get; set; }

        /// <summary>
        /// Visitor Collection
        /// </summary>
        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<VisitorPicture> VisitorPictures { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Automatic pluralization doesn't exist in the new EF stack.
            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<EmployeePicture>()
                   .ForeignKeys(fk => fk.ForeignKey<Employee>(p => p.EmployeeId))
                   .ToTable("EmployeePictures");

            builder.Entity<VisitorPicture>()
                .ForeignKeys(fk => fk.ForeignKey<Visitor>(p => p.VisitorId))
                .ToTable("VisitorPictures");
            builder.Entity<Visitor>().ToTable("Visitors");

            var visitor = builder.Model.GetEntityType(typeof(Visitor));
            var visitorPhoto = builder.Model.GetEntityType(typeof(VisitorPicture));
            visitor.AddNavigation(new Navigation(visitorPhoto.ForeignKeys.Single(), "VisitorPictures"));

            var employee = builder.Model.GetEntityType(typeof(Employee));
            var employeePicture = builder.Model.GetEntityType(typeof(EmployeePicture));

            employee.AddNavigation(new Navigation(employeePicture.ForeignKeys.Single(), "EmployeePictures"));
        }


    }
}