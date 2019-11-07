
namespace MyCompany.Travel.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Travel.Model;
    using System.Data.Entity;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// The attachment repository implementation
    /// </summary>
    public class TravelAttachmentRepository : ITravelAttachmentRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Creates a new instance of class TravelAttachmentRepository
        /// </summary>
        /// <param name="context">The EF context</param>
        public TravelAttachmentRepository(MyCompanyContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/>
        /// </summary>
        /// <param name="travelAttachmentId"><see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/></returns>
        public async Task<TravelAttachment> GetAsync(int travelAttachmentId)
        {
            return await _context.TravelAttachments.FindAsync(travelAttachmentId);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/></returns>
        public async Task<IEnumerable<TravelAttachment>> GetAllAsync()
        {
            return await _context.TravelAttachments.ToListAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/>
        /// </summary>
        /// <param name="travelAttachment"><see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/></returns>
        public async Task<int> AddAsync(TravelAttachment travelAttachment)
        {
            _context.TravelAttachments.Add(travelAttachment);
            await _context.SaveChangesAsync();
            return travelAttachment.TravelAttachmentId;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/>
        /// </summary>
        /// <param name="travelAttachment"><see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/></param>
        public async Task UpdateAsync(TravelAttachment travelAttachment)
        {
            _context.Entry<TravelAttachment>(travelAttachment)
                    .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/>
        /// </summary>
        /// <param name="travelAttachmentId"><see cref="MyCompany.Travel.Data.Repositories.ITravelAttachmentRepository"/></param>
        public async Task DeleteAsync(int travelAttachmentId)
        {
            var attachment = _context.TravelAttachments
                    .Find(travelAttachmentId);
            if (attachment != null)
            {
                _context.TravelAttachments.Remove(attachment);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose all resource
        /// </summary>
        /// <param name="disposing">Dispose managed resources check</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
