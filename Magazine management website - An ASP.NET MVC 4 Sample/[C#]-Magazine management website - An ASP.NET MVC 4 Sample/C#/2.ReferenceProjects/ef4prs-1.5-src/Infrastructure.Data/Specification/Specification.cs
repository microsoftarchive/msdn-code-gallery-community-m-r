using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Infrastructure.Data.Extensions;

namespace Infrastructure.Data.Specification
{
    public class Specification<TEntity> : ISpecification<TEntity>
    {   
        public Specification(Expression<Func<TEntity, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Specification<TEntity> And(Specification<TEntity> specification)
        {
            return new Specification<TEntity>(this.Predicate.And(specification.Predicate));
        }

        public Specification<TEntity> And(Expression<Func<TEntity, bool>> predicate)
        {
            return new Specification<TEntity>(this.Predicate.And(predicate));
        }    

        public Specification<TEntity> Or(Specification<TEntity> specification)
        {
            return new Specification<TEntity>(this.Predicate.Or(specification.Predicate));
        }

        public Specification<TEntity> Or(Expression<Func<TEntity, bool>> predicate)
        {
            return new Specification<TEntity>(this.Predicate.Or(predicate));
        }

        public TEntity SatisfyingEntityFrom(IQueryable<TEntity> query)
        {
            return query.Where(Predicate).SingleOrDefault();
        }

        public IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query)
        {
            return query.Where(Predicate);
        }

        public Expression<Func<TEntity, bool>> Predicate;
    }
}
