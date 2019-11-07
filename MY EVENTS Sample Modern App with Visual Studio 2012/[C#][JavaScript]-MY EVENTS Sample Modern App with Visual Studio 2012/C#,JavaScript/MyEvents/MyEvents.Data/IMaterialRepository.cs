using System.Collections.Generic;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// Repository to access to Material entities
    /// </summary>
    public interface IMaterialRepository
    {
        /// <summary>
        /// Gets the material with the specified id.
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        Material Get(int materialId);

        /// <summary>
        /// Get All Materials
        /// </summary>
        /// <param name="sessionId">sessionId</param>
        /// <returns>List of Materials</returns>
        IList<Material> GetAll(int sessionId);

        /// <summary>
        /// Add new material
        /// </summary>
        /// <param name="material">material information</param>
        /// <returns>materialId</returns>
        int Add(Material material);

        /// <summary>
        /// Delete Material
        /// </summary>
        /// <param name="materialId">Material to delete</param>
        void Delete(int materialId);

        /// <summary>
        /// Get the organizerId of the event
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        int GetOrganizerId(int materialId);
    }
}
