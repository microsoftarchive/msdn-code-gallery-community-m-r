
namespace MyCompany.Expenses.Data.Test.Initializers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Model;
    using System.Configuration;

     /// <summary>
    /// The default initializer for testing . Yoy can
    /// learn more about initializers in 
    /// http://msdn.microsoft.com/en-us/library/gg696323(v=VS.103).aspx
    /// </summary>
    public class MyCompanyContextInitializer
        : DropCreateDatabaseAlways<MyCompanyContext>  //** DropCreateDatabaseAlways / DropCreateDatabaseIfModelChanges / CreateDatabaseIfNotExists **
    {
        private static readonly Random randomize = new Random();
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];

        /// <summary>
        /// Seed
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyCompanyContext context)
        {
            CreateMainManager(context);
            CreateTeams(context, 2);
            CreateEmployees(context, 5);
            CreateExpenses(context, 2);
            CreateEmployeePictures(context);
            CreateNotificationChannels(context,2);
        }

        void CreateTeams(MyCompanyContext context, int count)
        {
            int managerId = context.Employees.First().EmployeeId;
            for (int i = 0; i < count; i++)
            {
                context.Teams.Add(new Team() { ManagerId = managerId, TeamId = i });
            }

            context.SaveChanges();
        }

        void CreateMainManager(MyCompanyContext context)
        {
            context.Employees.Add(new Employee()
                {
                    EmployeeId = 1,
                    FirstName = "Andrew",
                    LastName = "Davis",
                    Email = String.Format("Andrew.Davis@{0}", tenant),
                    JobTitle = "Team Lead",
                }
            );

            context.SaveChanges();
        }

        void CreateEmployees(MyCompanyContext context, int count)
        {
            int employeeId = 2;
            foreach (var team in context.Teams)
            {
                for (int i = 0; i < count; i++)
                {
                    var name = GetName();
                    context.Employees.Add(new Employee()
                        {
                            EmployeeId = employeeId,
                            FirstName = name.Split(' ')[0],
                            LastName = name.Split(' ')[1],
                            Email = GetEmail(),
                            TeamId = team.TeamId,
                            JobTitle = "Developer",
                        }
                    );

                    employeeId++;
                }
            }

            context.SaveChanges();
        }

        void CreateEmployeePictures(MyCompanyContext context)
        {
            int employeePictureId = 1;
            foreach (var employee in context.Employees)
            {
                string pictureName = GetPictureName();
                context.EmployeePictures.Add(new EmployeePicture()
                {
                    EmployeePictureId = employeePictureId,
                    EmployeeId = employee.EmployeeId,
                    PictureType = PictureType.Small,
                    Content = GetPicture(pictureName)
                });
                employeePictureId++;

                context.EmployeePictures.Add(new EmployeePicture()
                {
                    EmployeePictureId = employeePictureId,
                    EmployeeId = employee.EmployeeId,
                    PictureType = PictureType.Big,
                    Content = GetPicture(pictureName.Insert(pictureName.IndexOf("."), "_big"))
                });

                employeePictureId++;
            }

            context.SaveChanges();
        }

        void CreateNotificationChannels(MyCompanyContext context, int count)
        {
            context.Employees.Add(new Employee()
            {
                EmployeeId = 500,
                FirstName = "john",
                LastName = "Doe",
                Email = String.Format("johndoe@{0}", tenant),
                TeamId = 1,
                JobTitle = "Developer",
            }
            );

            context.Employees.Add(new Employee()
            {
                EmployeeId = 501,
                FirstName = "john",
                LastName = "Doe",
                Email = String.Format("johndoe1@{0}", tenant),
                TeamId = 1,
                JobTitle = "Developer",
            }
            );

            context.Employees.Add(new Employee()
            {
                EmployeeId = 502,
                FirstName = "john",
                LastName = "Doe2",
                Email = String.Format("johndoe2@{0}", tenant),
                TeamId = 1,
                JobTitle = "Developer",
            }
            );

            context.SaveChanges();

            var employees = context.Employees.OrderBy(e=>e.EmployeeId).ToList();
            for (int employeeIndex = 0; employeeIndex < employees.Count - 1; employeeIndex++)
            {
                for (int i = 0; i < count; i++)
                {
                    context.NotificationChannels.Add(new NotificationChannel
                    {
                        ChannelUri = "http://www.microsoft.com",
                        EmployeeId = employees[employeeIndex].EmployeeId,
                        NotificationType =
                            i == 0
                                ? NotificationType.WindowsStoreNotification
                                : NotificationType.WindowsPhoneNotification,
                    });
                }
            }

            context.SaveChanges();
        }

        void CreateExpenses(MyCompanyContext context, int count)
        {
            foreach (var employee in context.Employees)
            {
                for (int i = 0; i < count; i++)
                {
                    context.Expenses.Add(new Expense()
                        {
                            Name = "Bussiness",
                            Description = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Nam cursus. Morbi ut mi.",
                            CreationDate = DateTime.UtcNow.AddDays(-1),
                            LastModifiedDate = DateTime.UtcNow,
                            Status = GetStatus(), 
                            Amount = 270, 
                            Contact = "Jeff Phillips",
                            Picture = GetExpensePicture(),
                            ExpenseType = GetExpenseType(), 
                            RelatedProject = "MyCompany", 
                            EmployeeId = employee.EmployeeId,
                        }
                    );
                }
            }

            context.SaveChanges();
        }

        private static string GetCity()
        {
            List<string> cities = new List<string>() { 
                "Madrid", 
                "Irving", 
                "Seattle",
                "Tampa",
                "Los Angeles",
                "Irvine",
            };
            int index = randomize.Next(0, cities.Count);
            return cities[index];
        }

        private static ExpenseStatus GetStatus()
        {
            List<ExpenseStatus> status = new List<ExpenseStatus>() { 
                ExpenseStatus.Pending, 
                ExpenseStatus.Approved, 
                ExpenseStatus.Denied,
            };

            int index = randomize.Next(0, status.Count);
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

            int index = randomize.Next(0, types.Count);
            return types[index];
        }

        private static string GetEmail()
        {
            List<string> emails = new List<string>() { 
                String.Format("dalexander@{0}", tenant),
                String.Format("tandersen@{0}", tenant),
                String.Format("adavis@{0}", tenant)
            };
            int index = randomize.Next(0, emails.Count);
            return emails[index];
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
            FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(path),
                                                         GetExpenseName()), FileMode.Open, FileAccess.Read);

            using (BinaryReader br = new BinaryReader(fs))
            {
                return br.ReadBytes((int)fs.Length);
            }
        }

        private static string GetName()
        {
            List<string> names = new List<string>() { 
                "David Alexander", 
                "Thomas Andersen", 
                "Christen Anderson", 
                "Josh Bailey", 
                "Adam Barr",
                "Robin Counts",
                "Andrew Davis",
                "Garth Fort",
                "Christa Geller",
                "David Hamilton",
            };
            int index = randomize.Next(0, names.Count);
            return names[index];
        }

        private static string GetPictureName()
        {
            List<string> emails = new List<string>() { 
                "FakeImages\\user1.png", 
                "FakeImages\\user2.png", 
                "FakeImages\\user3.png", 
                "FakeImages\\user4.png", 
                "FakeImages\\user5.png", 
                "FakeImages\\user6.png", 
                "FakeImages\\user7.png", 
            };
            int index = randomize.Next(0, emails.Count);
            return emails[index];
        }

        private static string GetExpenseName()
        {
            List<string> emails = new List<string>() { 
                "FakeExpenses\\expense01.png", 
            };
            int index = randomize.Next(0, emails.Count);
            return emails[index];
        }
    }
}
