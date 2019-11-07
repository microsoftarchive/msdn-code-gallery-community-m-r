using System.Collections.Generic;
using System.Linq;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories;
using Xunit;

namespace DataAccessEfCoreTesting
{
    public class UserRepoTests: BaseTests
    {
        public static IEnumerable<object[]> GetEmails(int countOfTests)
        {
            var allData = new List<object[]>
            {
                new object[] {"Alice.Brown@example.com"},
                new object[] {"Ann.Blare@example.com"},
                new object[] {"Cathy.Jones@example.com"},
                new object[] {"John.Miller@example.com"}
            };


            return allData.Take(countOfTests);
        }

        [Theory]
        [MemberData(nameof(GetEmails), parameters: 4)]
        public void TestGetUserByEmail(string email)
        {
            // arrange
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new UserRepo(_dbContext, _mapperProvider, _mapper).GetUserByEmail(email);

            // assert
            Assert.IsType<UserDTO>(result);
            Assert.True(result.UserId > 0);
            Assert.False(string.IsNullOrEmpty(result.FullName));
            Assert.False(string.IsNullOrEmpty(result.Email));
            Assert.False(string.IsNullOrEmpty(result.ScreenName));
        }

    }
}
