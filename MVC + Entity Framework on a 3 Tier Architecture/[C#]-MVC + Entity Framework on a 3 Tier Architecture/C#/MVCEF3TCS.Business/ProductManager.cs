namespace MVCEF3TCS.Business
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using MVCEF3TCS.Data;
    using MVCEF3TCS.Entities;       

    public class ProductManager
    {
        private DatabaseContext db = new DatabaseContext();

        public IQueryable<Product> FindAll()
        {
            return this.db.ProductList.Include(p => p.Category);
        }

        public Product Find(int id)
        {
            return this.db.ProductList.Find(id);
        }

        public void Save(Product product)
        {
            this.db.ProductList.Add(product);
            this.db.SaveChanges();
        }

        public void Modify(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Product product = this.Find(id);
            db.ProductList.Remove(product);
            db.SaveChanges();
        }

        public IEnumerable<Category> CategoryList { 
            get
            {
                return this.db.CategoryList;
            }
        }

        public void Dispose()
        {
            this.db.Dispose();
        }
    }
}
