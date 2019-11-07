
namespace MyCompany.Travel.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Travel.Model;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// The employee repository implementation
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Creates a new instance of EmployeeRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public EmployeeRepository(MyCompanyContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></returns>
        public async Task<Employee> GetAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.Team.Manager)
                .SingleOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="email"><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></returns>
        public async Task<Employee> GetByEmailAsync(string email, PictureType pictureType)
        {
            var employees = await _context.Employees
                .Include(e => e.ManagedTeams)
                .Where(q => q.Email == email && q.EmployeePictures.Any(p => p.PictureType == pictureType))
                .ToListAsync();

            var result = employees.Select(q => new Employee()
            {
                EmployeeId = q.EmployeeId,
                FirstName = q.FirstName,
                LastName = q.LastName,
                Email = q.Email,
                TeamId = q.TeamId,
                JobTitle = q.JobTitle,
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

            return result;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></returns>
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = await _context.Employees
                    .OrderBy(e => e.FirstName).ThenBy(e => e.LastName)
                    .ToListAsync();

            var employeesWithPictures = employees.Select(q => new Employee()
                {
                    EmployeeId = q.EmployeeId,
                    FirstName = q.FirstName,
                    LastName = q.LastName,
                    Email = q.Email,
                    TeamId = q.TeamId,
                    JobTitle = q.JobTitle,
                    EmployeePictures = _context.EmployeePictures
                                .Where(e => e.EmployeeId == q.EmployeeId && e.PictureType == PictureType.Small)
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
                .ToList();

            return employeesWithPictures;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employee"><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></returns>
        public async Task<int> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.EmployeeId;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employee"><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></param>
        public async Task UpdateAsync(Employee employee)
        {
            _context.Entry<Employee>(employee)
                    .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Travel.Data.Repositories.IEmployeeRepository"/></param>
        public async Task DeleteAsync(int employeeId)
        {
            var employee = _context.Employees
                .Find(employeeId);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
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
