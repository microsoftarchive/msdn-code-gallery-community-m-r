
// Houssem Dellai
// houssem.dellai@live.com
// @HoussemDellai
// +216 95 325 964

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IGenericRepository<TEntity>
    {

        Task<TEntity> GetByIdAsync(int id);
        
        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        Task EditAsync(TEntity entity);

        Task InsertAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}