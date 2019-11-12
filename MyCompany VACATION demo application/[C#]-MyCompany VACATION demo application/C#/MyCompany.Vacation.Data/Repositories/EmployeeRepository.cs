
namespace MyCompany.Vacation.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Vacation.Model;
    using System.Data.Entity;
    using System;

    /// <summary>
    /// The employee repository implementation
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="context">the context dependency</param>
        public EmployeeRepository(MyCompanyContext context)
        {
            if (context == null) 
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></returns>
        public Employee Get(int employeeId)
        {
            return _context.Employees
                .Include(e => e.Team.Manager)
                .SingleOrDefault(q => q.EmployeeId == employeeId);
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="email"><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></returns>
        public Employee GetByEmail(string email, PictureType pictureType)
        {
            //get the first employee with this email and a 
            //picture of type specified

            var result = _context.Employees
                .Include(e => e.Team)
                .Include("Team.Manager")
                .Where(e => e.Email == email)
                .Select(e => new
                {
                    Employee = e,
                    ManagedTeams = e.ManagedTeams,
                    Team = e.Team,
                    Manager = e.Team.Manager,
                    Pictures = e.EmployeePictures.Where(ep => ep.PictureType == pictureType)
                })
                .FirstOrDefault();

            if (result != null)
            {
                return BuildEmployee(result.Employee);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></returns>
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employee"><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></returns>
        public int Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee.EmployeeId;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employee"><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></param>
        public void Update(Employee employee)
        {
            _context.Entry<Employee>(employee)
                .State = EntityState.Modified;

            _context.SaveChanges();
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Vacation.Data.Repositories.IEmployeeRepository"/></param>
        public void Delete(int employeeId)
        {
            var employee = _context.Employees
                    .Find(employeeId);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

        }

        Employee BuildEmployee(Employee employee)
        {
            if (employee == null)
                return null;

            return new Employee()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                OfficeId = employee.OfficeId,
                TeamId = employee.TeamId,
                Team = employee.Team != null ? new Team()
                {
                    TeamId = employee.Team.TeamId,
                    ManagerId = employee.Team.ManagerId, 
                    OfficeId = employee.Team.OfficeId,
                    Manager = employee.Team.Manager != null ?
                            new Employee() { 
                                FirstName = employee.Team.Manager.FirstName,
                                EmployeeId = employee.Team.Manager.EmployeeId,                                
                                Email = employee.Team.Manager.Email 
                            } : null,                 
                } : null,
                ManagedTeams = (employee.ManagedTeams != null) ? 
                        employee.ManagedTeams.Select(m => new Team() { TeamId = m.TeamId }).ToList() 
                        : null,
                EmployeePictures = (employee.EmployeePictures != null) ? 
                            employee.EmployeePictures
                            .Select(e => new EmployeePicture()
                            {
                                Employee = null,
                                PictureType = e.PictureType,
                                EmployeePictureId = e.EmployeePictureId,
                                EmployeeId = e.EmployeeId,
                                Content = e.Content
                            }).ToList()
                            : null,
            };
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
