namespace MVCEF3TCS.Data
{
    using System.Data.Entity;
    using MVCEF3TCS.Entities;
    
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Product> ProductList { get; set; }
        public DbSet<Category> CategoryList { get; set; }
    }
}
