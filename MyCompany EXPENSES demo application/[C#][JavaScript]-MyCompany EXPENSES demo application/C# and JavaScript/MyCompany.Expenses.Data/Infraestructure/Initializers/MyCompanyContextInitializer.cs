namespace MyCompany.Expenses.Data.Infrastructure.Initializers
{
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Model;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The default initializer for testing . You can learn more about initializers in 
    /// http://msdn.microsoft.com/en-us/library/gg696323(v=VS.103).aspx
    /// </summary>
    public class MyCompanyContextInitializer
        : DropCreateDatabaseIfModelChanges<MyCompanyContext>
    {
        private static readonly Random _randomize = new Random();
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private string _picturePath = "FakeImages\\{0}.jpg";
        private string _smallPicturePath = "FakeImages\\{0} - small.jpg";
        private List<string> _employeeEmails = new List<string>() 
        {
            String.Format("Andrew.Davis@{0}", tenant),
            String.Format("Christen.Anderson@{0}", tenant),
            String.Format("David.Alexander@{0}", tenant),
            String.Format("Robin.Counts@{0}", tenant),
            String.Format("Thomas.Andersen@{0}", tenant),
            String.Format("Josh.Bailey@{0}", tenant),
            String.Format("Adam.Barr@{0}", tenant),
            String.Format("Christa.Geller@{0}", tenant),
            String.Format("Carole.Poland@{0}", tenant),
            String.Format("Cristina.Potra@{0}", tenant)
        };
        private List<string> _employeeNames = new List<string>() 
        {
            "Andrew Davis",
            "Christen Anderson", 
            "David Alexander",
            "Robin Counts",
            "Thomas Andersen", 
            "Josh Bailey", 
            "Adam Barr",
            "Christa Geller",
            "Carole Poland",
            "Cristina Potra"
        };

        /// <summary>
        /// Seed
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyCompanyContext context)
        {
            CreateTeamManagers(context);
            CreateEmployees(context);
            CreateEmployeePictures(context);
            CreateExpenses(context);
        }

        private void CreateTeamManagers(MyCompanyContext context)
        {
            int managersCount = 2;

            for (int i = 0; i < managersCount; i++)
            {
                int id = i + 1;
                var name = _employeeNames[i];
                var split = name.Split(' ');
                context.Employees.Add(new Employee()
                {
                    EmployeeId = id,
                    FirstName = split[0],
                    LastName = split[1],
                    Email = _employeeEmails[i],
                    JobTitle = "Team Lead",
                });

                context.Teams.Add(new Team() { TeamId = id, ManagerId = id });
            }

            context.SaveChanges();
        }

        private void CreateEmployees(MyCompanyContext context)
        {
            int initialId = context.Employees.Count() + 1;
            int employeesCount = _employeeEmails.Count + 1;

            int teamOneId = context.Teams.OrderBy(t => t.TeamId).First().TeamId;
            int teamTwoId = context.Teams.OrderByDescending(t => t.TeamId).First().TeamId;

            for (int i = initialId; i < employeesCount; i++)
            {
                int index = i - 1;
                var name = _employeeNames[index];

                var split = name.Split(' ');
                context.Employees.Add(new Employee()
                {
                    EmployeeId = i,
                    FirstName = split[0],
                    LastName = split[1],
                    Email = _employeeEmails[index],
                    JobTitle = GetPosition(i),
                    TeamId = i > (_employeeEmails.Count / 2) ? teamOneId : teamTwoId
                });
            }

            context.SaveChanges();
        }

        private void CreateEmployeePictures(MyCompanyContext context)
        {
            int employeePictureId = 1;

            foreach (var employee in context.Employees)
            {
                string employeeName = string.Format("{0} {1}", employee.FirstName, employee.LastName);
                string path = string.Format(_smallPicturePath, employeeName);
                context.EmployeePictures.Add(new EmployeePicture()
                {
                    EmployeePictureId = employeePictureId,
                    EmployeeId = employee.EmployeeId,
                    PictureType = PictureType.Small,
                    Content = GetPicture(path)
                });
                employeePictureId++;

                path = string.Format(_picturePath, employeeName);
                context.EmployeePictures.Add(new EmployeePicture()
                {
                    EmployeePictureId = employeePictureId,
                    EmployeeId = employee.EmployeeId,
                    PictureType = PictureType.Big,
                    Content = GetPicture(path)
                });
                employeePictureId++;
            }

            context.SaveChanges();
        }

        void CreateExpenses(MyCompanyContext context)
        {
            foreach (var employee in context.Employees)
            {
                int expensesCount = _randomize.Next(0, 10);

                for (int i = 0; i < expensesCount; i++)
                {
                    int days = _randomize.Next(-10, -1);

                    var expense = new Expense()
                        {
                            Description = string.Empty,
                            CreationDate = DateTime.UtcNow.AddDays(days),
                            LastModifiedDate = DateTime.UtcNow,
                            Status = GetStatus(),
                            Amount = _randomize.Next(1, 9999),
                            Contact = "Jeff Phillips",
                            ExpenseType = GetExpenseType(),
                            RelatedProject = "MyCompany",
                            EmployeeId = employee.EmployeeId,
                        };

                    expense.Name = GetExpenseName(expense.ExpenseType);
                    expense.Picture = GetExpensePicture();
                    if (expense.ExpenseType == ExpenseType.Travel)
                        CreateRoute(context, expense);

                    context.Expenses.Add(expense);
                }
            }

            context.SaveChanges();
        }

        private static void CreateRoute(MyCompanyContext context, Expense expense)
        {
            var split = expense.Name.Split(' ');
            context.ExpenseTravels.Add(new ExpenseTravel()
            {
                Expense = expense,
                Distance = _randomize.Next(50, 1000),
                From = split[0],
                To = split[2]
            });
        }

        private static string GetCity()
        {
            List<string> cities = new List<string>() { 
                "Denver", 
                "Irving", 
                "Seattle",
                "Tampa",
                "Los Angeles",
                "Portland",
                "Oklahoma City",
                "Atlanta",
                "Cleveland",
                "Tulsa",
                "Buffalo",
                "Orlando",
                "Salt Lake City",
                "Ontario",
                "Springfield",
                "Pasadena",
                "Kent"
            };
            int index = _randomize.Next(0, cities.Count);
            return cities[index];
        }

        private static string GetFood()
        {
            List<string> foods = new List<string>() { 
                "Lunch", 
                "Dinner",
                "Coffee",
                "Breakfast"
            };
            int index = _randomize.Next(0, foods.Count);
            return foods[index];
        }

        private static ExpenseStatus GetStatus()
        {
            List<ExpenseStatus> status = new List<ExpenseStatus>() { 
                ExpenseStatus.Pending, 
                ExpenseStatus.Approved, 
                ExpenseStatus.Denied,
            };

            int index = _randomize.Next(0, status.Count);
            return status[index];
        }

        private static ExpenseType GetExpenseType()
        {
            List<ExpenseType> types = new List<ExpenseType>() { 
                ExpenseType.Food, 
                ExpenseType.Accommodation, 
                ExpenseType.Travel,
                ExpenseType.Other
            };

            int index = _randomize.Next(0, types.Count);
            return types[index];
        }

        private static string GetExpenseName(ExpenseType type)
        {
            switch (type)
            {
                case ExpenseType.Travel:
                    return string.Format("{0} to {1}", GetCity(), GetCity());
                case ExpenseType.Food:
                    return GetFood();
                case ExpenseType.Accommodation:
                    return "Hotel";
                case ExpenseType.Other:
                    return "Other";
            }

            return string.Empty;
        }

        private static byte[] GetPicture(string filename)
        {
            string path = new Uri(Assembly.GetAssembly(typeof(MyCompanyContextInitializer)).CodeBase).LocalPath;
            FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(path),
                                                         filename), FileMode.Open, FileAccess.Read);

            using (BinaryReader br = new BinaryReader(fs))
            {
                return br.ReadBytes((int)fs.Length);
            }
        }

        private static byte[] GetExpensePicture()
        {
            string path = new Uri(Assembly.GetAssembly(typeof(MyCompanyContextInitializer)).CodeBase).LocalPath;
            FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(path), GetExpensePicturePath()), FileMode.Open, FileAccess.Read);

            using (BinaryReader br = new BinaryReader(fs))
            {
                return br.ReadBytes((int)fs.Length);
            }
        }

        private static string GetExpensePicturePath()
        {
            List<string> photos = new List<string>() { 
                "FakeExpenses\\expense01.jpg", 
            };
            int index = _randomize.Next(0, photos.Count);
            return photos[index];
        }

        private static string GetPosition(int index)
        {
            List<string> positions = new List<string>() {
                "Development advisor", 
                "Software engineer", 
                "Frontend developer", 
                "Backend developer", 
            };

            return index / 3 < positions.Count ? positions[index / 3] : positions[0];
        }
    }
}
