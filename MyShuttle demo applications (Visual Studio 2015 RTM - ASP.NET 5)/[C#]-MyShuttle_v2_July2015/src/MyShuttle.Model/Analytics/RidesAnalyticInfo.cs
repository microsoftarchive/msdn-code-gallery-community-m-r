
namespace MyShuttle.Model
{
    using System.Collections.Generic;
    using System;

    public class RidesAnalyticInfo
    {
        public int LastDaysRides { get; set; }

        public int LastDaysPassengers { get; set; }

        public RideGroupInfo RidesEvolution { get; set; }
    }

    public class RideGroupInfo
    {
        public IList<int> Days { get; set; }

        public IList<int> Values { get; set; }
    }

    public class RideResult
    {
        public DateTime Date { get; set; }

        public int Value { get; set; }
    }
}