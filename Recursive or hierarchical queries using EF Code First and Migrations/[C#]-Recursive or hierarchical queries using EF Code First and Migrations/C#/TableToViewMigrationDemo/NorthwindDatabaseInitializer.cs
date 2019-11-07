using System.Collections.Generic;
using System.Data.Entity;

namespace TableToViewMigrationDemo
{
    public class NorthwindDatabaseInitializer : IDatabaseInitializer<NorthwindContext>
    {
        public void InitializeDatabase(NorthwindContext context)
        {
            if (context.Database.Exists() && !context.Database.CompatibleWithModel(true))
            {
                context.Database.Delete();
            }

            if (!context.Database.Exists())
            {
                context.Database.Create();

                context.Database.ExecuteSqlCommand("DROP TABLE [dbo].[ManagerEmployees]");

                context.Database.ExecuteSqlCommand(@"CREATE VIEW [dbo].[ManagerEmployees]
AS
    WITH    cte ( ManagerEmployeeID, EmployeeID )
              AS ( SELECT   EmployeeID ,
                            EmployeeID
                   FROM     dbo.Employees
                   UNION ALL
                   SELECT   e.EmployeeID ,
                            cte.EmployeeID
                   FROM     cte
                            INNER JOIN dbo.Employees AS e ON e.ReportsToEmployeeID = cte.ManagerEmployeeID
                 )
    SELECT  ISNULL(EmployeeID, 0) AS ManagerEmployeeID ,
            ISNULL(ManagerEmployeeID, 0) AS EmployeeID
    FROM    cte");

                Seed(context);

                context.SaveChanges();
            }
        }

        private void Seed(NorthwindContext context)
        {
            new List<Employee>
            {
                new Employee { EmployeeID = 1, LastName = "Davolio", FirstName = "Nancy Lynn", ReportsToEmployeeID = 2 },
                new Employee { EmployeeID = 2, LastName = "Fuller", FirstName = "Andrew", ReportsToEmployeeID = null },
                new Employee { EmployeeID = 3, LastName = "Leverling", FirstName = "Janet", ReportsToEmployeeID = 2 },
                new Employee { EmployeeID = 4, LastName = "Peacock", FirstName = "Margaret", ReportsToEmployeeID = 3 },
                new Employee { EmployeeID = 5, LastName = "Buchanan", FirstName = "Steven", ReportsToEmployeeID = 8 },
                new Employee { EmployeeID = 6, LastName = "Suyama", FirstName = "Michael", ReportsToEmployeeID = 2 },
                new Employee { EmployeeID = 8, LastName = "Callahan", FirstName = "Laura", ReportsToEmployeeID = 3 },
                new Employee { EmployeeID = 9, LastName = "Dodsworth", FirstName = "Anne", ReportsToEmployeeID = 6 },
                new Employee { EmployeeID = 10, LastName = "Pindlegrass", FirstName = "Pearl", ReportsToEmployeeID = 6 }                
            }.ForEach(e => context.Employees.Add(e));
        }
    }
}
