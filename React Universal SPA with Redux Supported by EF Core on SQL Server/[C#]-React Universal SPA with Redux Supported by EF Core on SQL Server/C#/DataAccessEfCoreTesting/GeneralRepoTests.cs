using System.Collections.Generic;
using System.Linq;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.Repositories;
using Xunit;

namespace DataAccessEfCoreTesting
{
    public class GeneralRepoTests: BaseTests
    {
        [Fact]
        public void TestGetCategories()
        {
            // arrange
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();

            // act
            var results = new GeneralRepo<Category>(_dbContext, _mapperProvider, _mapper).GetAll().ToList();

            // assert
            Assert.IsType<List<Category>>(results);

            Assert.True(results.Count > 0);

            Assert.All(results, category =>
            {
                Assert.True(category.CategoryId > 0);
                Assert.False(string.IsNullOrEmpty(category.CategoryName));
            });
        }
    }
}
