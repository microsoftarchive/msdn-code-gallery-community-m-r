using System;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Visitors
{
    /// <summary>
    /// Summary description for EmployeesController
    /// </summary>
    public class EmployeesController : Controller
    {
        private readonly ISecurityHelper _securityHelper = null;
        private readonly VisitorContext _visitorContext = null;
        
        public EmployeesController([NotNull]VisitorContext employeeRepository, [NotNull]ISecurityHelper securityHelper)
        {
            _visitorContext = employeeRepository;
            _securityHelper = securityHelper;
        }

        public JsonResult Get(PictureType pictureType)
        {
            var pictures = _visitorContext.EmployeePictures.ToList();

            var email = _securityHelper.GetUser();

            var employee = _visitorContext.Employees
               .Where(e => e.Email == email)
               .Select(e => new
                {
                    Employee = e,
                    Pictures = e.EmployeePictures.Where(ep => ep.PictureType == (int)pictureType)
                })
               .FirstOrDefault();

            return Json(employee.Employee);
        }
    }
}