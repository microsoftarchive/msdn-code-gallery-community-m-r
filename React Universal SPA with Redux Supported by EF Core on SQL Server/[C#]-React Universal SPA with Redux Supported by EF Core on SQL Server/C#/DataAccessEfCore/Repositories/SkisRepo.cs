using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessEfCore.DataAccess;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEfCore.Repositories
{
    public class SkisRepo: GeneralRepo<Style>, ISkisRepo
    {
        public SkisRepo(SkiShopDbContext dbContext, IConfigurationProvider mapperProvider, IMapper mapper) 
            : base(dbContext, mapperProvider, mapper) { }

        public SkisDTO GetSkis(int styleId)
        {
            var style = _DbContext.Styles
                .AsNoTracking()
                .Include(s => s.StyleState)
                .Include(s => s.Skus)
                .Include(s => s.Descriptions)
                .FirstOrDefault(s => s.StyleId == styleId);

            return _mapper.Map<SkisDTO>(style);
        }

        public StyleBasicDTO GetStyleBasic(int styleId)
        {
            var style = _DbContext.VwStyles
                .AsNoTracking()
                .FirstOrDefault(s => s.StyleId == styleId);

            return _mapper.Map<StyleBasicDTO>(style);
        }

        public IQueryable<spSpec> GetSpecs(int styleId)
        {
            var styleIdParameter = new SqlParameter("@styleId", styleId);

            string sql = "EXEC [dbo].[uspSkisSpecs_Get]" + "@styleId";

            var specs = _DbContext.SpSpecs.FromSql(sql, styleIdParameter);

            return specs;
        }

        public ReviewsDTO GetReviews(int styleId)
        {
            return _DbContext.Styles.Where(style => style.StyleId == styleId)
                .ProjectTo<ReviewsDTO>(_mapperProvider).FirstOrDefault();
        }

    }
}
