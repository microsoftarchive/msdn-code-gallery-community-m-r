namespace MyShuttle.API.Results
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CollectionResult
    {
        public int RequestedItems { get; set; }
        public int ActualItems { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable Items { get; set; }

        public CollectionResult(IEnumerable<dynamic> items, int requested, int total)
        {
            RequestedItems = requested;
            ActualItems = items != null ? items.Count() : 0;
            Items = items;
            TotalItems = total;
        }

        public static CollectionResult Empty(int requested)
        {
            return new CollectionResult(Enumerable.Empty<object>(), requested, 0);
        }

    }
}
