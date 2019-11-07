using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleArch.Model;

using System.Data.Entity;

namespace SampleArch.Service
{
    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        protected IContext _context;
        protected IDbSet<T> _dbset;

        public EntityService(IContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }     


        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _dbset.Add(entity);
           _context.SaveChanges(); 
        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges(); 
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");         
              _dbset.Remove(entity);
              _context.SaveChanges(); 
        }

        public virtual IEnumerable<T> GetAll()
        {         
             return _dbset.AsEnumerable<T>();
        }
    }
}
