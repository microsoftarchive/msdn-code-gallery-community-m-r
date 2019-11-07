using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Infrastructure.Tests.Data.Domain;
using Infrastructure.Tests.Data.Domain.Mapping;
using System.Data.Entity.Infrastructure;

namespace Infrastructure.Tests.Data.Lab
{
    public class MyDbContext : DbContext
    {       
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public MyDbContext(string connStringName) : 
            base(connStringName)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add<Customer>(new CustomerMapping());
            modelBuilder.Configurations.Add<Product>(new ProductMapping());
            modelBuilder.Configurations.Add<Category>(new CategoryMapping());
            modelBuilder.Configurations.Add<Order>(new OrderMapping());
            modelBuilder.Configurations.Add<OrderLine>(new OrderLineMapping());
        }
    }
}
