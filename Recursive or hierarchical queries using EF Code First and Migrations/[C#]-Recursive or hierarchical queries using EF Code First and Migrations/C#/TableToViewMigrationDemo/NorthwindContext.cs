using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace TableToViewMigrationDemo
{
    public class NorthwindContext : DbContext, INorthwindContext
    {
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<ManagerEmployee> ManagerEmployees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Entity<Employee>().HasOptional(e => e.ReportsTo).WithMany(e => e.Manages).HasForeignKey(e => e.ReportsToEmployeeID).WillCascadeOnDelete(false);
            modelBuilder.Entity<ManagerEmployee>().HasKey(me => new { me.ManagerEmployeeID, me.EmployeeID });
        }

        public IQueryable<Employee> GetEmployeesForManagerRecursively(int managerEmployeeID)
        {
            return from managerEmployee in this.ManagerEmployees
                   join employee in this.Employees on managerEmployee.EmployeeID equals employee.EmployeeID
                   where managerEmployee.ManagerEmployeeID == managerEmployeeID
                   select employee;
        }

        public IQueryable<Employee> GetManagersForEmployeeRecursively(int employeeID)
        {
            return from managerEmployee in this.ManagerEmployees
                   join manager in this.Employees on managerEmployee.ManagerEmployeeID equals manager.EmployeeID
                   where managerEmployee.EmployeeID == employeeID
                   select manager;
        }
    }
}
