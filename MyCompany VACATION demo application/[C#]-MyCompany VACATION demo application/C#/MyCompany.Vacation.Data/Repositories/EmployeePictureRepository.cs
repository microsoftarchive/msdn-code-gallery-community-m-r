namespace MyCompany.Vacation.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Vacation.Model;
    using System.Data.Entity;
    using System;

    /// <summary>
    /// The employee picture repository implementation
    /// </summary>
    public class EmployeePictureRepository : IEmployeePictureRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="context">the context dependency</param>
        public EmployeePictureRepository(MyCompanyContext context)
        {
            if (context == null) 
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IEmployeePictureRepository"/>
        /// </summary>
        /// <param name="employeePicture"><see cref="MyCompany.Vacation.Data.Repositories.IEmployeePictureRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IEmployeePictureRepository"/></returns>
        public int Add(EmployeePicture employeePicture)
        {
            _context.EmployeePictures.Add(employeePicture);
            _context.SaveChanges();
            return employeePicture.EmployeePictureId;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IEmployeePictureRepository"/>
        /// </summary>
        /// <param name="employeePicture"><see cref="MyCompany.Vacation.Data.Repositories.IEmployeePictureRepository"/></param>
        public void Update(EmployeePicture employeePicture)
        {
            _context.Entry<EmployeePicture>(employeePicture)
                .State = EntityState.Modified;

            _context.SaveChanges();
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IEmployeePictureRepository"/>
        /// </summary>
        /// <param name="employeePictureId"><see cref="MyCompany.Vacation.Data.Repositories.IEmployeePictureRepository"/></param>
        public void Delete(int employeePictureId)
        {
            var picture = _context.EmployeePictures
                .Find(employeePictureId);

            if (picture != null)
            {
                _context.EmployeePictures.Remove(picture);
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
