using System.Collections.Generic;

namespace DataAccessEfCore.DTOs
{
    public class StylesFilterDTO
    {
        public byte CategoryId { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 6;

        public byte Sort { get; set; }

        public IEnumerable<short> BrandIds { get; set; } 

        public IEnumerable<byte> GenderIds { get; set; } 

        public IEnumerable<byte> IdealForIds { get; set; } 
    }
}
