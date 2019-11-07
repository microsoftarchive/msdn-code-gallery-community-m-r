using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;

namespace DataAccessEfCore.Repositories.Abstractions
{
    public interface IReviewRepo
    {
        ReviewAddReturnDTO AddReview(Review review);
    }
}
