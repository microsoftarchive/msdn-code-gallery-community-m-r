using System.Linq;
using DataAccessEfCore.DbModels;

namespace DataAccessEfCore.DTOs
{
    public class InitialHomePageDTO
    {
        public IQueryable<Category> Categories { get; set; }
        public IQueryable<StyleForListDTO> Populars { get; set; }
        public IQueryable<StyleForListDTO> Clearances { get; set; }

    }
}
