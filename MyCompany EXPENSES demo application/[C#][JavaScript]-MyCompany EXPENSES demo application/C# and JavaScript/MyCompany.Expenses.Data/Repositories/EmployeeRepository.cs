
namespace MyCompany.Expenses.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Expenses.Model;
    using System.Data.Entity;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// The employee repository implementation
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Creates a new instance of EmployeeRepository class
        /// </summary>
        /// <param name="context"></param>
        public EmployeeRepository(MyCompanyContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></returns>
        public async Task<Employee> GetAsync(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></returns>
        public async Task<Employee> GetEmployeeAsync(int employeeId, PictureType pictureType)
        {
            var result = await _context.Employees
                    .Where(q => q.EmployeeId == employeeId)
                    .ToListAsync();

            return result.Select(q => new Employee()
             {
                 EmployeeId = q.EmployeeId,
                 FirstName = q.FirstName,
                 LastName = q.LastName,
                 Email = q.Email,
                 TeamId = q.TeamId,
                 EmployeePictures = _context.EmployeePictures
                             .Where(e => e.EmployeeId == q.EmployeeId && e.PictureType == pictureType)
                             .ToList()
                             .Select(e => new EmployeePicture()
                             {
                                 Employee = null,
                                 PictureType = e.PictureType,
                                 EmployeePictureId = e.EmployeePictureId,
                                 EmployeeId = e.EmployeeId,
                                 Content = e.Content
                             }).ToList(),
             })
             .FirstOrDefault();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="email"><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></returns>
        public async Task<Employee> GetByEmailAsync(string email, PictureType pictureType)
        {
            var employees = await _context.Employees
                    .Include(e => e.ManagedTeams)
                    .Where(q => q.Email == email && q.EmployeePictures.Any(p => p.PictureType == pictureType))
                    .ToListAsync();

            return employees.Select(q => new Employee()
            {
                EmployeeId = q.EmployeeId,
                FirstName = q.FirstName,
                LastName = q.LastName,
                Email = q.Email,
                TeamId = q.TeamId,
                ManagedTeams = q.ManagedTeams.Select(m => new Team() { TeamId = m.TeamId }).ToList(),
                EmployeePictures = _context.EmployeePictures
                            .Where(e => e.EmployeeId == q.EmployeeId && e.PictureType == pictureType)
                            .ToList()
                            .Select(e => new EmployeePicture()
                            {
                                Employee = null,
                                PictureType = e.PictureType,
                                EmployeePictureId = e.EmployeePictureId,
                                EmployeeId = e.EmployeeId,
                                Content = e.Content
                            }).ToList(),
            })
            .FirstOrDefault();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></returns>
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employee"><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></returns>
        public async Task<int> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.EmployeeId;
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employee"><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></param>
        public async Task UpdateAsync(Employee employee)
        {
            _context.Entry<Employee>(employee)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Expenses.Data.Repositories.IEmployeeRepository"/></param>
        public async Task DeleteAsync(int employeeId)
        {
            var comment = _context.Employees.Find(employeeId);
            if (comment != null)
            {
                _context.Employees.Remove(comment);
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