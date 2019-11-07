using System.Collections.Generic;

namespace DataAccessEfCore.DTOs
{
    public class ReviewsDTO
    {
        public StyleStateDTO StyleState { get; set; }
        public IEnumerable<ReviewDTO> Reviews { get; set; }
    }
}
