using System.Linq;
using DataAccessEfCore.DTOs;

namespace DataAccessEfCore.Repositories.Abstractions
{
    public interface IStyleRepo
    {
        IQueryable<StyleForListDTO> GetPopulars();
        IQueryable<StyleForListDTO> GetClearances();

        StylesPopularClearanceDTO GetStylesHomePage();

        StylesFilteredDTO FilterStyles(StylesFilterDTO filter);
    }
}