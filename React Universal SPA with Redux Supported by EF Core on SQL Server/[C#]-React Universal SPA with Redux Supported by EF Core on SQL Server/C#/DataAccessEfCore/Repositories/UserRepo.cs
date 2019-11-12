using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessEfCore.DataAccess;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories.Abstractions;

namespace DataAccessEfCore.Repositories
{
    public class UserRepo: GeneralRepo<UserIdentity>, IUserRepo
    {
        public UserRepo(SkiShopDbContext dbContext, IConfigurationProvider mapperProvider, IMapper mapper)
            : base(dbContext, mapperProvider, mapper) { }

        public UserDTO GetUserByEmail(string email)
        {
            return GetAll(user => user.Email == email)
                .ProjectTo<UserDTO>(_mapperProvider).FirstOrDefault();
        }

    }
}
