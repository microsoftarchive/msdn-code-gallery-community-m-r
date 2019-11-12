using System.Collections.Generic;
using System.Linq;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories;
using Xunit;

namespace DataAccessEfCoreTesting
{
    public class StyleRepoTests:BaseTests
    {

        [Fact]
        public void TestGetPopulars()
        {
            // arrange
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            //act
            var results = new StyleRepo(_dbContext, _mapperProvider, _mapper).GetPopulars().ToList();

            // assert
            Assert.IsType<List<StyleForListDTO>>(results);

            Assert.True(results.Count >= 3);
            Assert.All(results, popular =>
            {
                Assert.True(popular.StyleId > 0 && popular.CategoryId > 0);
                Assert.False(string.IsNullOrEmpty(popular.StyleName) || string.IsNullOrEmpty(popular.BrandName)
                    || string.IsNullOrEmpty(popular.GenderName) || string.IsNullOrEmpty(popular.ImageSmall));
                Assert.InRange(popular.AverageRatings, 0, 5);
                Assert.True(popular.PriceCurrent == popular.PriceRegular);
            });
        }

        [Fact]
        public void TestGetClearances()
        {
            // arrange
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var results = new StyleRepo(_dbContext, _mapperProvider, _mapper).GetClearances().ToList();

            // assert
            Assert.IsType<List<StyleForListDTO>>(results);

            Assert.True(results.Count >= 3);
            Assert.All(results, clearance =>
            {
                Assert.True(clearance.StyleId > 0 && clearance.CategoryId > 0);
                Assert.False(string.IsNullOrEmpty(clearance.StyleName) || string.IsNullOrEmpty(clearance.BrandName)
                                                                     || string.IsNullOrEmpty(clearance.GenderName) || string.IsNullOrEmpty(clearance.ImageSmall));
                Assert.InRange(clearance.AverageRatings, 0, 5);
                Assert.True(clearance.PriceCurrent < clearance.PriceRegular);
            });
        }

        public static IEnumerable<object[]> GetFilters(int countOfTests)
        {
            var allData = new List<object[]>
            {
                new object[]
                {
                    new StylesFilterDTO
                    {
                       CategoryId = 1,
                       PageNumber = 1,
                       PageSize = 9,
                       Sort = 2,
                       BrandIds = new List<short> { 3, 10 },
                       GenderIds = new List<byte> { 1 },
                       IdealForIds = new List<byte> { 5 }
                    }
                }
            };

            return allData.Take(countOfTests);
        }

        [Theory]
        [MemberData(nameof(GetFilters), parameters: 1)]
        public void TestFilterStyles(StylesFilterDTO filter)
        {
            // arrange
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new StyleRepo(_dbContext, _mapperProvider, _mapper).FilterStyles(filter);

            // assert
            Assert.IsType<StylesFilteredDTO>(result);

            Assert.True(result.TotalCount == 1);

            Assert.True(result.BrandCounts.Count() == 7);
            Assert.All(result.BrandCounts, brandCount =>
            {
                Assert.True(brandCount.BrandId > 0);
                Assert.False(string.IsNullOrEmpty(brandCount.BrandName));
                Assert.True(brandCount.BrandCount >= 0);
            });

            Assert.True(result.GenderCounts.Count() == 3);
            Assert.All(result.GenderCounts, genderCount =>
            {
                Assert.True(genderCount.GenderId > 0);
                Assert.False(string.IsNullOrEmpty(genderCount.GenderName));
                Assert.True(genderCount.GenderCount >= 0);
            });

            Assert.True(result.IdealForCounts.Count() == 4);
            Assert.All(result.IdealForCounts, idealForCount =>
            {
                Assert.True(idealForCount.IdealForId > 0);
                Assert.False(string.IsNullOrEmpty(idealForCount.IdealForSpec));
                Assert.True(idealForCount.IdealForCount >= 0);
            });

            Assert.True(result.StylesFiltered.Count() == 1);
            Assert.All(result.StylesFiltered, style =>
            {
                Assert.True(style.StyleId > 0);
                Assert.False(string.IsNullOrEmpty(style.StyleName));
                Assert.True(style.CategoryId > 0);
                Assert.False(string.IsNullOrEmpty(style.BrandName));
                Assert.False(string.IsNullOrEmpty(style.GenderName));
                Assert.False(string.IsNullOrEmpty(style.ImageSmall));
                Assert.True(style.PriceCurrent > 0);
                Assert.True(style.PriceRegular > 0);
                Assert.True(style.AverageRatings >= 0);
                Assert.True(style.ReviewCount >= 0);
                Assert.IsType<bool>(style.SoldOut);
            });
        }
    }
}
