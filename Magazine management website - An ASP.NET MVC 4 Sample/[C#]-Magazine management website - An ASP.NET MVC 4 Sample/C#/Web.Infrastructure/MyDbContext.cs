namespace CIK.News.Web.Infras
{
    using System.Data.Entity;
    
    using CIK.News.Entities.NewsAgg;
    using CIK.News.Entities.UserAgg;
    using CIK.News.Mapping;

    public class MyDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public MyDbContext(string connStringName) :
            base(connStringName)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new ItemMapping());
            modelBuilder.Configurations.Add(new ItemContentMapping());
            modelBuilder.Configurations.Add(new UserMapping());
        }
    }
}