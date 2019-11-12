using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessEfCore.DbModels;

namespace DataAccessEfCore.DTOs
{
    public class ResultDTO_AddOrder
    {
        public IEnumerable<int> SkuIdsOverStock { get; set; }

        public int OrderId { get; set; }

        public Guid CustomerOrderId { get; set; }

        public IEnumerable<SkuStyleDTO> Skus { get; set; }

        public IEnumerable<StyleState> StyleStates { get; set; }
    }
}
