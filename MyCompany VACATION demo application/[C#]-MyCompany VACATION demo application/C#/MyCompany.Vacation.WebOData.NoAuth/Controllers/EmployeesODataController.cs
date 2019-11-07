namespace MyCompany.Vacation.Web.Controllers
{
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Vacation.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.OData;

    /// <summary>
    /// Employees odata controller
    /// </summary>
    public class EmployeesODataController : EntitySetController<Employee, int>
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public EmployeesODataController(MyCompanyContext context)
        {
            _context = context;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Employee> Get()
        {
            var queryOptions = this.QueryOptions;
            IQueryable<Employee> employees = _context.
                Employees
                .AsQueryable();

            queryOptions.ApplyTo(employees);
            return employees.AsQueryable();
        }

        /// <summary>
        /// Gets the employee vacation requests
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<VacationRequest> GetVacationRequests([FromODataUri] int key)
        {
            IEnumerable<VacationRequest> vacationRequests = 
                _context.VacationRequests.Where(vr => vr.EmployeeId == key && vr.From.Year == DateTime.Now.Year).ToList();

            return vacationRequests;
        }
 
        /// <summary>
        /// Gets the employee by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override Employee GetEntityByKey(int key)
        {
            Employee employee = _context.Employees.FirstOrDefault(e=> e.EmployeeId == key);
            return employee;
        }

    }
}
