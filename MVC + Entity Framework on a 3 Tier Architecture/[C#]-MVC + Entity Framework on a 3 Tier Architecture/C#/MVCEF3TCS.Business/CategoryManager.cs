//-----------------------------------------------------------------------
// <copyright file="CategoryManager.cs" company="Edson Castañeda">
//     Copyright © Edson Castañeda. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MVCEF3TCS.Business
{
    using System.Data;
    using System.Linq;
    using MVCEF3TCS.Data;
    using MVCEF3TCS.Entities;    

    /// <summary>
    /// The 'CategoryManager' class
    /// </summary>
    public class CategoryManager
    {
        private DatabaseContext db = new DatabaseContext();

        public IQueryable<Category> FindAll()
        {
            return this.db.CategoryList;
        }

        public Category Find(int id)
        {
            return this.db.CategoryList.Find(id);
        }

        public void Save(Category category)
        {
            this.db.CategoryList.Add(category);
            this.db.SaveChanges();
        }

        public void Modify(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Category category = this.Find(id);
            db.CategoryList.Remove(category);
            db.SaveChanges();
        }

        public void Dispose()
        {
            this.db.Dispose();
        }
    }
}