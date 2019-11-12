using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;

namespace ClusterEngine
{
    public interface IClusteredLayer
    {
        /// <summary>
        /// Add an entity to the layer.
        /// </summary>
        /// <param name="item">An object that inherits from the Entity Class</param>
        void AddEntity(Entity item);

        /// <summary>
        /// Add multiple entities to the layer.
        /// </summary>
        /// <param name="items">A list of objects that inherit from the Entity Class</param>
        void AddEntities(IList<Entity> items);

        /// <summary>
        /// Clear the layer
        /// </summary>
        void Clear();

        /// <summary>
        /// Get an Entity by id.
        /// </summary>
        /// <param name="id">ID of the entity to get</param>
        /// <returns>An object that inherits from the Entity Class</returns>
        Entity GetEntity(int id);

        /// <summary>
        /// Get a list of Entities by id.
        /// </summary>
        /// <param name="id">A list of entity ID's to get</param>
        /// <returns>A list of objects that inherits from the Entity Class</returns>
        IList<Entity> GetEntities(IList<int> ids);

        /// <summary>
        /// Gets a reference to the base MapLayer used for visualizing clusters. 
        /// </summary>
        /// <returns>MapLayer of clustered data</returns>
        MapLayer GetMapLayer();

        /// <summary>
        /// Method to set the cluster options.
        /// </summary>
        /// <param name="options">Cluster options to use when clustering.</param>
        void SetOptions(ClusterOptions options);
    }
}
