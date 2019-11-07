using AutoMapper;
using DataAccessEfCore.DataAccess;

namespace DataAccessEfCoreTesting
{
    public class BaseTests
    {
        protected SkiShopDbContext _dbContext;
        protected IConfigurationProvider _mapperProvider;
        protected IMapper _mapper;

        public BaseTests()
        {
            _dbContext = Configurations.GetDbContext();
            _mapperProvider = Configurations.GetMapperProvider();
            _mapper = Configurations.GetMapper();
        }
    }
}
