using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DataAccessEfCore.DataAccess;
using DataAccessEfCore.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEfCore.Repositories
{
    public class GeneralRepo<T> : IGeneralRepo<T> 
        where T:class
    {
        protected readonly SkiShopDbContext _DbContext;
        protected readonly IConfigurationProvider _mapperProvider;
        protected readonly IMapper _mapper;
        protected DbSet<T> _DbSet;

        public GeneralRepo(SkiShopDbContext dbContext, IConfigurationProvider mapperProvider, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapperProvider = mapperProvider;
            _mapper = mapper;
            _DbSet = _DbContext.Set<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return predicate != null ? _DbSet.Where(predicate) : _DbSet;
        }

        public void SaveChange(T input)
        {
            _DbSet.Add(input);
            _DbContext.SaveChanges();
        }

    }

}
