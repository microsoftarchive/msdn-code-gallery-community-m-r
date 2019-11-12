using System;

namespace DataAccessEfCore.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

        public string ScreenName { get; set; }

        public byte Rating { get; set; }

        public string ReviewText { get; set; }

        public string CreatedDateTime { get; set; }
    }
}
