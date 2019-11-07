using Microsoft.Maps.MapControl.WPF;

namespace ClusterEngine
{
    /// <summary>
    /// An abstract class used to create custom defined options for clustering.
    /// </summary>
    public abstract class ClusterOptions
    {
        private int _clusterRadius = 45;

        /// <summary>
        /// Radius used by the clustering algorithms. The larger the radius the faster 
        /// the algorithms run and the more spread out the data will be.
        /// </summary>
        public int ClusterRadius
        {
            get
            {
                return _clusterRadius;
            }
            set
            {
                if (value > 0)
                {
                    _clusterRadius = value;
                }
            }
        }

        /// <summary>
        /// A callback method that is used to generate a pushpin for a single entity.
        /// </summary>
        /// <param name="entity">An Entity</param>
        /// <returns>A pushpin that represents the entity.</returns>
        public abstract Pushpin RenderEntity(Entity entity);

        /// <summary>
        /// A callback method that is used to generate a pushpin for a cluster of entities.
        /// </summary>
        /// <param name="cluster">A clustered point.</param>
        /// <returns>A pushpin that represents the clustered entities.</returns>
        public abstract Pushpin RenderCluster(ClusteredPoint cluster);
    }
}
