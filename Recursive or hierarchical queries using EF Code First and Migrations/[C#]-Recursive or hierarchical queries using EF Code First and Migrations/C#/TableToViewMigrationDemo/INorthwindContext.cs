using System.Data.Entity;
using System.Linq;
namespace TableToViewMigrationDemo
{
    public interface INorthwindContext : IUnitOfWork
    {
        IDbSet<Employee> Employees { get; }
        IQueryable<Employee> GetEmployeesForManagerRecursively(int managerEmployeeID);
        IQueryable<Employee> GetManagersForEmployeeRecursively(int employeeID);
    }
}
