
namespace MyCompany.Vacation.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Vacation.Model;
    using System.Data.Entity;
    using System;

    /// <summary>
    /// The office repository implementation
    /// </summary>
    public class OfficeRepository : IOfficeRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="context">the context dependency</param>
        public OfficeRepository(MyCompanyContext context)
        {
            if (context == null) 
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IOfficeRepository"/>
        /// </summary>
        /// <param name="office"><see cref="MyCompany.Vacation.Data.Repositories.IOfficeRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IOfficeRepository"/></returns>
        public int Add(Office office)
        {
            _context.Offices.Add(office);
            _context.SaveChanges();
            return office.OfficeId;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IOfficeRepository"/>
        /// </summary>
        /// <param name="office"><see cref="MyCompany.Vacation.Data.Repositories.IOfficeRepository"/></param>
        public void Update(Office office)
        {
            _context.Entry<Office>(office)
                .State = EntityState.Modified;

            _context.SaveChanges();
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IOfficeRepository"/>
        /// </summary>
        /// <param name="officeId"><see cref="MyCompany.Vacation.Data.Repositories.IOfficeRepository"/></param>
        public void Delete(int officeId)
        {
            var office = _context.Offices
                .Find(officeId);
            if (office != null)
            {
                _context.Offices.Remove(office);
                _context.SaveChanges();
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
