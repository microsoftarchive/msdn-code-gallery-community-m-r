using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessEfCore.DataAccess;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories.Abstractions;

namespace DataAccessEfCore.Repositories
{
    public class StyleRepo: GeneralRepo<Style>, IStyleRepo
    {
        public StyleRepo(SkiShopDbContext dbContext, IConfigurationProvider mapperProvider, IMapper mapper) 
            : base(dbContext, mapperProvider, mapper) { }

        public IQueryable<StyleForListDTO> GetPopulars()
        {
            var polularIds = new List<int> { 9, 17, 24 };

            return _DbContext.VwStyles.Where(vwStyle => polularIds.Contains(vwStyle.StyleId))
                .ProjectTo<StyleForListDTO>(_mapperProvider);
        }

        public IQueryable<StyleForListDTO> GetClearances()
        {
            var clearanceIds = new List<int> { 3, 8, 22 };

            return _DbContext.VwStyles.Where(vwStyle => clearanceIds.Contains(vwStyle.StyleId))
                .ProjectTo<StyleForListDTO>(_mapperProvider);
        }

        public StylesPopularClearanceDTO GetStylesHomePage()
        {
            var polularIds = new List<int> { 9, 17, 24 };
            var clearanceIds = new List<int> { 3, 8, 22 };

            var styles = _DbContext.VwStyles.Where(vwStyle => clearanceIds.Contains(vwStyle.StyleId) 
                                                              || polularIds.Contains(vwStyle.StyleId))
                .ProjectTo<StyleForListDTO>(_mapperProvider);

            var result = new StylesPopularClearanceDTO
            {
                Polulars = styles.Where(style => style.PriceCurrent == style.PriceRegular),
                Clearances = styles.Where(style => style.PriceCurrent < style.PriceRegular)
            };

            return result;
        }

        public StylesFilteredDTO FilterStyles(StylesFilterDTO filter)
        {
            var result = new StylesFilteredDTO();

            var styleIds = _DbContext.VwStyleIdealFors
                .Where(style => style.CategoryId == filter.CategoryId
                                && (filter.BrandIds == null || !filter.BrandIds.Any() || filter.BrandIds.Contains(style.BrandId))
                                && (filter.GenderIds == null || !filter.GenderIds.Any() || filter.GenderIds.Contains(style.GenderId))
                                && (filter.IdealForIds == null || !filter.IdealForIds.Any() || filter.IdealForIds.Contains(style.IdealForId)))
                .Select(style => style.StyleId).Distinct();

            result.TotalCount = styleIds.Count();

            var brands = _DbContext.VwStyles
                .Where(style => style.CategoryId == filter.CategoryId)
                .Select(style => new
                {
                    style.BrandId,
                    style.BrandName
                }).Distinct();

            var genders = _DbContext.VwStyles
                .Where(style => style.CategoryId == filter.CategoryId)
                .Select(style => new
                {
                    style.GenderId,
                    style.GenderName
                }).Distinct();

            var idealFors = _DbContext.VwStyleIdealFors
                .Where(style => style.CategoryId == filter.CategoryId)
                .Select(style => new
                {
                    style.IdealForId,
                    style.IdealForSpec
                }).Distinct();

            result.BrandCounts = brands.GroupJoin(_DbContext.VwStyleIdealFors,
                    brand => brand.BrandId,
                    style => style.BrandId,
                    (brand, style) => new
                    {
                        Brand = brand, Style = style.Where(s => s.CategoryId == filter.CategoryId
                                && (filter.GenderIds == null || !filter.GenderIds.Any() || filter.GenderIds.Contains(s.GenderId))
                                && (filter.IdealForIds == null || !filter.IdealForIds.Any() || filter.IdealForIds.Contains(s.IdealForId)))
                            .Select(s => s.StyleId).Distinct()
                    })
                .SelectMany(
                    x => x.Style.DefaultIfEmpty(),
                    (x, y) => new BrandCountDTO
                    {
                        BrandId = x.Brand.BrandId,
                        BrandName = x.Brand.BrandName,
                        BrandCount = x.Style.Count()
                    }
                )
                .GroupBy(x => x.BrandId)
                .Select(y => y.FirstOrDefault());

            result.GenderCounts = genders.GroupJoin(_DbContext.VwStyleIdealFors,
                    gender => gender.GenderId,
                    style => style.GenderId,
                    (gender, style) => new
                    {
                        Gender = gender, Style = style.Where(s => s.CategoryId == filter.CategoryId
                                  && (filter.BrandIds == null || !filter.BrandIds.Any() || filter.BrandIds.Contains(s.BrandId))
                                  && (filter.IdealForIds == null || !filter.IdealForIds.Any() || filter.IdealForIds.Contains(s.IdealForId)))
                            .Select(s => s.StyleId).Distinct()
                    })
                .SelectMany(
                    x => x.Style.DefaultIfEmpty(),
                    (x, y) => new GenderCountDTO
                    {
                        GenderId = x.Gender.GenderId,
                        GenderName = x.Gender.GenderName,
                        GenderCount = x.Style.Count()
                    }
                )
                .GroupBy(x => x.GenderId)
                .Select(y => y.FirstOrDefault());

            result.IdealForCounts = idealFors.GroupJoin(_DbContext.VwStyleIdealFors,
                    idealFor => idealFor.IdealForId,
                    style => style.IdealForId,
                    (idealFor, style) => new
                    {
                        IdealFor = idealFor, Style = style.Where(s => s.CategoryId == filter.CategoryId
                              && (filter.BrandIds == null || !filter.BrandIds.Any() || filter.BrandIds.Contains(s.BrandId))
                              && (filter.GenderIds == null || !filter.GenderIds.Any() || filter.GenderIds.Contains(s.GenderId)))
                        .Select(s => s.StyleId).Distinct()
                    })
                .SelectMany(
                    x => x.Style.DefaultIfEmpty(),
                    (x, y) => new IdealForCountDTO
                    {
                        IdealForId = x.IdealFor.IdealForId,
                        IdealForSpec = x.IdealFor.IdealForSpec,
                        IdealForCount = x.Style.Count()
                    }
                )
                .GroupBy(x => x.IdealForId)
                .Select(y => y.FirstOrDefault());

            var stylesFiltered = _DbContext.VwStyles.Where(style => styleIds.Contains(style.StyleId));

            if (filter.Sort == 1)
            {
                stylesFiltered = stylesFiltered.OrderBy(style => style.PriceCurrent);
            } else if (filter.Sort == 2)
            {
                stylesFiltered = stylesFiltered.OrderByDescending(style => style.PriceCurrent);
            }

            result.StylesFiltered = stylesFiltered.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ProjectTo<StyleForListDTO>(_mapperProvider);

            return result;
        }
    }
}
