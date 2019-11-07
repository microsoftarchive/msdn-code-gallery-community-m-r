using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model.Dtos
{
    public class ServicesSummaryDto
    {
        public SatisfactionDto[] Satisfactions { get; set; }

        public double PositivesDifference { get; set; }

        public double AcceptedDifference { get; set; }

        public ServicesSummaryDto(IEnumerable<Ride> current, IEnumerable<Ride> previous)
        {
            Satisfactions = new SatisfactionDto[2];

            var sets = new IEnumerable<Ride>[] { current, previous };

            Satisfactions[0] = new SatisfactionDto(current);
            Satisfactions[1] = new SatisfactionDto(previous);

            PositivesDifference = ((double)(Satisfactions[0].Positives - Satisfactions[1].Positives)) /
                Satisfactions[1].Positives * 100.0;

            AcceptedDifference = ((double)(Satisfactions[0].Accepted - Satisfactions[1].Accepted)) /
                Satisfactions[1].Accepted * 100.0;


        }
    }

    public class SatisfactionDto
    {
        public double[] Ratings { get; set; }
        public double SatisfactionPercent { get; set; }
        public int Positives { get; set; }
        public int Accepted { get; set; }
        public int Total { get; set; }
        public double AcceptedPercent { get; set; }

        public SatisfactionDto(IEnumerable<Ride> rides)
        {
            Total = rides.Count();
            Ratings = new double[Total];
            var idx = 0;
            foreach (var ride in rides)
            {
                Ratings[idx++] = ride.Rating;
                Positives += ride.Rating >= 3.5 ? 1 : 0;
                Accepted += ride.Rating > 0.0 ? 1 : 0;
            }
            SatisfactionPercent = (double)Positives / (double)Accepted * 100.0;
            AcceptedPercent = (double)Accepted / (double)Total * 100.0;
        }
    }
}
