using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessEfCore.DTOs
{
    public class StylesPopularClearanceDTO
    {
        public IQueryable<StyleForListDTO> Polulars { get; set; }

        public IQueryable<StyleForListDTO> Clearances { get; set; }
    }
}
