using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessEfCore.Repositories.Abstractions
{
    public interface IGeneralRepo<T> where T : class 
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);

        void SaveChange(T input);
    }
}