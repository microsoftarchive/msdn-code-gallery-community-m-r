namespace TableToViewMigrationDemo.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<NorthwindContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            // Seed data: 
            //   Override the Seed method in this class to add seed data.
            //    - The Seed method will be called after migrating to the latest version.
            //    - You can use the DbContext.AddOrUpdate() helper extension method to avoid creating
            //      duplicate seed data. E.g.
            //
            //          myContext.AddOrUpdate(c => c.FullName,
            //              new Customer { FullName = "Andrew Peters", CustomerNumber = 123 },
            //              new Customer { FullName = "Brice Lambson", CustomerNumber = 456 },
            //              new Customer { FullName = "Rowan Miller", CustomerNumber = 789 }
            //          );
            //
        }

        protected override void Seed(NorthwindContext context)
        {
            context.AddOrUpdate(
                new Employee { EmployeeID = 1, LastName = "Davolio", FirstName = "Nancy Lynn", ReportsToEmployeeID = 2 },
                new Employee { EmployeeID = 2, LastName = "Fuller", FirstName = "Andrew", ReportsToEmployeeID = null },
                new Employee { EmployeeID = 3, LastName = "Leverling", FirstName = "Janet", ReportsToEmployeeID = 2 },
                new Employee { EmployeeID = 4, LastName = "Peacock", FirstName = "Margaret", ReportsToEmployeeID = 3 },
                new Employee { EmployeeID = 5, LastName = "Buchanan", FirstName = "Steven", ReportsToEmployeeID = 8 },
                new Employee { EmployeeID = 6, LastName = "Suyama", FirstName = "Michael", ReportsToEmployeeID = 2 },
                new Employee { EmployeeID = 8, LastName = "Callahan", FirstName = "Laura", ReportsToEmployeeID = 3 },
                new Employee { EmployeeID = 9, LastName = "Dodsworth", FirstName = "Anne", ReportsToEmployeeID = 6 },
                new Employee { EmployeeID = 10, LastName = "Pindlegrass", FirstName = "Pearl", ReportsToEmployeeID = 6 }
                );
        }
    }
}
