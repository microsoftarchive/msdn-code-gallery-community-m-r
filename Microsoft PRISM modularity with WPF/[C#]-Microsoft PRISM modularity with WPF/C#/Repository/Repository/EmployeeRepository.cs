using Repository.Model;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Repository.Repository
{
    [Export(typeof(IEmployeeRepository))]
    public class EmployeeRepository : IEmployeeRepository
    {

        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
            {
                new Employee(){ID=1,FirstName="ABC",LastName="FGH",Email="ABC@gmai.com",Mobile="123464344"},
                new Employee(){ID=2,FirstName="BCD",LastName="GHI",Email="BCD@gmai.com",Mobile="343534534"},
                new Employee(){ID=3,FirstName="CDE",LastName="HIJ",Email="CDE@gmai.com",Mobile="343534534"},
                new Employee(){ID=4,FirstName="DEF",LastName="IJK",Email="DEF@gmai.com",Mobile="345345345"},
                new Employee(){ID=5,FirstName="EFG",LastName="JKL",Email="EFG@gmai.com",Mobile="656465432"},
            };
        }
    }
}
