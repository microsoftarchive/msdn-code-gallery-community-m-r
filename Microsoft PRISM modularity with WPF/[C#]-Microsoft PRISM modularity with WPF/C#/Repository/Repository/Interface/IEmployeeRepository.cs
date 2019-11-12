using Repository.Model;
using System.Collections.Generic;

namespace Repository.Repository.Interface
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
    }
}
