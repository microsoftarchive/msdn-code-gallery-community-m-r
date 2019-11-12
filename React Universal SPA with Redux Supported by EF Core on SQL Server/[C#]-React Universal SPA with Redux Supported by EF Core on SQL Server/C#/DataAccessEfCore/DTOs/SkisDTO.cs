using System.Collections.Generic;

namespace DataAccessEfCore.DTOs
{
    public class SkisDTO
    {
        public StyleExtraDTO StyleExtra { get; set; }
        public StyleStateDTO State { get; set; }
        public IEnumerable<SkuDTO> Skus { get; set; }
        public IEnumerable<DescDTO> Descs { get; set; }
    }
}
