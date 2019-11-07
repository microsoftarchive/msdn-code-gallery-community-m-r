
using System.Linq;

namespace DataAccessEfCore.DTOs
{
    public class StylesFilteredDTO
    {
        public int TotalCount { get; set; }

        public IQueryable<BrandCountDTO> BrandCounts { get; set; }

        public IQueryable<GenderCountDTO> GenderCounts { get; set; }

        public IQueryable<IdealForCountDTO> IdealForCounts { get; set; }

        public IQueryable<StyleForListDTO> StylesFiltered { get; set; }
    }
}
