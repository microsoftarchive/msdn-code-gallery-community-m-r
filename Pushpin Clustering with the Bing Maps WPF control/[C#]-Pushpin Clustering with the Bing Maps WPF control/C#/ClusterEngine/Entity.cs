using Microsoft.Maps.MapControl.WPF;

namespace ClusterEngine
{
    /// <summary>
    /// A base class that is used to represent a single data point within a cluster. 
    /// All data objects passed into the clustering layers should derive from this class.
    /// </summary>
    public class Entity
    {
        public int ID { get; set; }

        public Location Location { get; set; }
    }
}
