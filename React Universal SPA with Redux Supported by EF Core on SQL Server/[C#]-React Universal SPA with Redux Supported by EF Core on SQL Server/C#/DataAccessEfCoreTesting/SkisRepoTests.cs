using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories;
using DataAccessEfCoreTesting.Helpers;
using Xunit;

namespace DataAccessEfCoreTesting
{
    public class SkisRepoTests: BaseTests
    {
        public static IEnumerable<object[]> GetStyleIds(int countOfTests)
        {
            const int styleCounts = 25;
            var styleIds = NumberHelper.GenerateRandomIntArray(styleCounts);

            var allData = new List<object[]>();

            for (var i = 0; i < styleCounts; i++)
            {
                allData.Add(new object[] { styleIds[i] });
            }

            return allData.Take(countOfTests);
        }

        [Theory]
        [MemberData(nameof(GetStyleIds), parameters: 25)]
        public void TestGetSkis(int styleId)
        {
            // arrange
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new SkisRepo(_dbContext, _mapperProvider, _mapper).GetSkis(styleId);

            // assert
            Assert.IsType<SkisDTO>(result);
            Assert.NotNull(result.StyleExtra);
            Assert.NotNull(result.State);
            Assert.NotEmpty(result.Skus);
            Assert.NotEmpty(result.Descs);
        }

        [Theory]
        [MemberData(nameof(GetStyleIds), parameters: 25)]
        public void TestGetStyleBasic(int styleId)
        {
            // arrange
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new SkisRepo(_dbContext, _mapperProvider, _mapper).GetStyleBasic(styleId);

            // assert
            Assert.IsType<StyleBasicDTO>(result);
            Assert.True(result.StyleId > 0);
            Assert.True(result.CategoryId > 0);
            Assert.False(string.IsNullOrEmpty(result.StyleName));
            Assert.False(string.IsNullOrEmpty(result.StyleName));
            Assert.False(string.IsNullOrEmpty(result.GenderName));
            Assert.False(string.IsNullOrEmpty(result.ImageSmall));
            Assert.True(result.PriceCurrent > 0);
            Assert.True(result.PriceRegular > 0);
        }

        [Theory]
        [MemberData(nameof(GetStyleIds), parameters: 25)]
        public void TestGetSpecs(int styleId)
        {
            // arrange
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var results = new SkisRepo(_dbContext, _mapperProvider, _mapper).GetSpecs(styleId)
                .OrderBy(r => r.DisplayIndex).ToList();

            // assert
            Assert.IsType<List<spSpec>>(results);

            for (var i = 0; i < results.Count; i++)
            {
                Assert.True(results[i].DisplayIndex == i + 1);
                Assert.NotNull(results[i].SpecKeyName);
                Assert.NotNull(results[i].SpecText);
            }
        }

        [Theory]
        [MemberData(nameof(GetStyleIds), parameters: 25)]
        public void TestGetReviews(int styleId)
        {
            // arrange
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var result = new SkisRepo(_dbContext, _mapperProvider, _mapper).GetReviews(styleId);

            // assert
            Assert.IsType<ReviewsDTO>(result);

            Assert.NotNull(result.StyleState);

            if (result.Reviews.Any())
            {
                foreach (var review in result.Reviews)
                {
                    Assert.True(review.ReviewId > 0);
                    Assert.False(string.IsNullOrEmpty(review.ScreenName));
                    Assert.True(review.Rating > 0);
                    Assert.False(string.IsNullOrEmpty(review.ReviewText));
                    Assert.False(string.IsNullOrEmpty(review.CreatedDateTime));
                }
            }
            else
                Assert.Empty(result.Reviews);
        }

        public static IEnumerable<object[]> GetNewReview(int countOfTests)
        {
            var allData = new List<object[]>
            {
                new object[] { new Review
                {
                    StyleId = 9,
                    UserId = 1,
                    Rating = 5,
                    ReviewText = "testing for EF Core",
                    CreatedDateTime = DateTime.Now
                } }
            };

            return allData.Take(countOfTests);
        }

        [Theory]
        [MemberData(nameof(GetNewReview), parameters: 1)]
        public void TestAddReview(Review review)
        {
            // arrange 
            _dbContext = _dbContext ?? Configurations.GetDbContext();
            _mapperProvider = _mapperProvider ?? Configurations.GetMapperProvider();
            _mapper = _mapper ?? Configurations.GetMapper();

            // act
            var styleStateBefore = new SkisRepo(_dbContext, _mapperProvider, _mapper).GetSkis(review.StyleId).State;
            var result = new ReviewRepo(_dbContext, _mapperProvider, _mapper).AddReview(review);

            // assert
            Assert.IsType<ReviewAddReturnDTO>(result);

            Assert.True(result.ReviewId > 0);

            var averageRating = (styleStateBefore.AverageRatings * styleStateBefore.ReviewCount + review.Rating) /
                                (styleStateBefore.ReviewCount + 1);
            Assert.True(result.StyleState.AverageRatings == averageRating);
            Assert.True(result.StyleState.ReviewCount == styleStateBefore.ReviewCount + 1);
            Assert.True(result.StyleState.SoldOut == styleStateBefore.SoldOut);
        }

    }
}
