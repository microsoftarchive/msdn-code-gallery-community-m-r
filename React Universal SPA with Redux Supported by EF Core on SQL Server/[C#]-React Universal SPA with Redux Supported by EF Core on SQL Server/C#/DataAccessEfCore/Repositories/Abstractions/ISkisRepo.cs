using System.Linq;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;

namespace DataAccessEfCore.Repositories.Abstractions
{
    public interface ISkisRepo
    {
        SkisDTO GetSkis(int styleId);
        StyleBasicDTO GetStyleBasic(int styleId);
        IQueryable<spSpec> GetSpecs(int styleId);
        ReviewsDTO GetReviews(int styleId);
    }
}