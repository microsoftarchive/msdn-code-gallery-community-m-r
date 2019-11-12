using System.Linq;

namespace TableToViewMigrationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer<NorthwindContext>(new NorthwindDatabaseInitializer());

            using (INorthwindContext context = new NorthwindContext())
            {
                var manager1 = context.Employees.FirstOrDefault(e => e.ReportsTo == null);
                if (manager1 != null)
                {
                    var employees1 = context.GetEmployeesForManagerRecursively(manager1.EmployeeID).ToList();
                }

                var manager2 = context.Employees.FirstOrDefault(e => e.LastName == "Leverling" && e.FirstName == "Janet");
                if (manager2 != null)
                {
                    var employees2 = context.GetEmployeesForManagerRecursively(manager2.EmployeeID).ToList();
                }

                var employee1 = context.Employees.FirstOrDefault(e => e.LastName == "Leverling" && e.FirstName == "Janet");
                if (employee1 != null)
                {
                    var managers1 = context.GetManagersForEmployeeRecursively(employee1.EmployeeID).ToList();
                }

                var employee2 = context.Employees.FirstOrDefault(e => e.LastName == "Buchanan" && e.FirstName == "Steven");
                if (employee2 != null)
                {
                    var managers2 = context.GetManagersForEmployeeRecursively(employee2.EmployeeID).ToList();
                }
            }
        }
    }
}
