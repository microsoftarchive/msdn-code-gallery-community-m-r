using System.Collections.Generic;
using System.Linq;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// <see cref="MyEvents.Data.IMaterialRepository"/>
    /// </summary>
    public class MaterialRepository : IMaterialRepository
    {
        /// <summary>
        ///  <see cref="MyEvents.Data.IMaterialRepository"/>
        /// </summary>
        /// <param name="materialId"><see cref="MyEvents.Data.IMaterialRepository"/></param>
        /// <returns></returns>
        public Material Get(int materialId)
        {
            using (var context = new MyEventsContext())
            {
                return context.Materials.Single(q => q.MaterialId == materialId);
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IMaterialRepository"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Data.IMaterialRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IMaterialRepository"/></returns>
        public IList<Material> GetAll(int sessionId)
        {
            using (var context = new MyEventsContext())
            {
                return context.Materials.Where(q => q.SessionId == sessionId).ToList();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IMaterialRepository"/>
        /// </summary>
        /// <param name="material"><see cref="MyEvents.Data.IMaterialRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IMaterialRepository"/></returns>
        public int Add(Material material)
        {
            using (var context = new MyEventsContext())
            {
                context.Materials.Add(material);
                context.SaveChanges();
                return material.MaterialId;
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IMaterialRepository"/>
        /// </summary>
        /// <param name="materialId"><see cref="MyEvents.Data.IMaterialRepository"/></param>
        public void Delete(int materialId)
        {
            using (var context = new MyEventsContext())
            {
                var material = context.Materials.FirstOrDefault(q => q.MaterialId == materialId);
                if (material != null)
                {
                    context.Materials.Remove(material);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        ///  <see cref="MyEvents.Data.IMaterialRepository"/>
        /// </summary>
        /// <param name="materialId"> <see cref="MyEvents.Data.IMaterialRepository"/></param>
        /// <returns> <see cref="MyEvents.Data.IMaterialRepository"/></returns>
        public int GetOrganizerId(int materialId)
        {
            int id = 0;
            using (var context = new MyEventsContext())
            {
                var material = context.Materials.Include("Session.EventDefinition")
                    .FirstOrDefault(q => q.MaterialId == materialId);
                if (material != null)
                {
                    id = material.Session.EventDefinition.OrganizerId;
                }
            }
            return id;
        }
    }
}
