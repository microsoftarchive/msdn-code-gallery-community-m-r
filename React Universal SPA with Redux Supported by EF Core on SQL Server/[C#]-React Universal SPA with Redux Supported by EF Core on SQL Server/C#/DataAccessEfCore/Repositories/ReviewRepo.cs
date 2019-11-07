using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessEfCore.DataAccess;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories.Abstractions;

namespace DataAccessEfCore.Repositories
{
    public class ReviewRepo : GeneralRepo<Review>, IReviewRepo
    {
        public ReviewRepo(SkiShopDbContext dbContext, IConfigurationProvider mapperProvider, IMapper mapper)
            : base(dbContext, mapperProvider, mapper) { }


        public ReviewAddReturnDTO AddReview(Review review)
        {
            var styleState = _DbContext.VwStyles
                .Where(style => style.StyleId == review.StyleId)
                .ProjectTo<StyleStateDTO>(_mapperProvider).First();

            SaveChange(review);

            var updatedAverageRating = (styleState.AverageRatings * styleState.ReviewCount + review.Rating) /
                                       (styleState.ReviewCount + 1);

            var result = new ReviewAddReturnDTO
            {
                ReviewId = review.ReviewId,
                StyleState = new StyleStateDTO
                {
                    AverageRatings = updatedAverageRating,
                    ReviewCount = styleState.ReviewCount + 1,
                    SoldOut = styleState.SoldOut
                }
            };

            return result;
        }
    }
}
