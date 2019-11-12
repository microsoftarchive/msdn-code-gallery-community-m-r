
namespace MyCompany.Visitors.Data.Test.Initializers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using MyCompany.Visitors.Data;
    using MyCompany.Visitors.Model;
    using System.Configuration;

    /// <summary>
    /// The default initializer for testing . Yoy can
    /// learn more about initializers in 
    /// http://msdn.microsoft.com/en-us/library/gg696323(v=VS.103).aspx
    /// </summary>
    public class MyCompanyContextInitializer
        : DropCreateDatabaseAlways<MyCompanyContext>  //** DropCreateDatabaseAlways / DropCreateDatabaseIfModelChanges / CreateDatabaseIfNotExists **
    {
        private static readonly Random Randomize = new Random();
        private static string tenant = ConfigurationManager.AppSettings["ida:Domain"];

        /// <summary>
        /// Seed
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyCompanyContext context)
        {
            CreateMainManager(context);
            CreateTeams(context, 2);
            CreateEmployees(context, 4);
            CreateVisitors(context, 2);
            CreateVisits(context);
            CreateEmployeePictures(context);
            CreateVisitorPictures(context);
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

        void CreateVisitors(MyCompanyContext context, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var name = GetName();
                context.Visitors.Add(new Visitor()
                    {
                        FirstName = name.Split(' ')[0],
                        LastName = name.Split(' ')[1], 
                        Company = Guid.NewGuid().ToString(),
                        Email = GetEmail(), 
                        CreatedDateTime = DateTime.UtcNow, 
                        LastModifiedDateTime = DateTime.UtcNow,
                    }
                );
            }

            context.SaveChanges();
        }

        void CreateVisitorPictures(MyCompanyContext context)
        {
            foreach (var visitor in context.Visitors)
            {
                string pictureName = GetPictureName();
                context.VisitorPictures.Add(new VisitorPicture()
                {
                    VisitorId = visitor.VisitorId,
                    PictureType = PictureType.Small,
                    Content = GetPicture(pictureName)
                });

                context.VisitorPictures.Add(new VisitorPicture()
                {
                    VisitorId = visitor.VisitorId,
                    PictureType = PictureType.Big,
                    Content = GetPicture(pictureName.Insert(pictureName.IndexOf("."), "_big"))
                });

            }

            context.SaveChanges();
        }

        void CreateVisits(MyCompanyContext context)
        {
            foreach (var employee in context.Employees)
            {
                foreach (var visitor in context.Visitors)
                {
                    context.Visits.Add(new Visit()
                    {
                        CreatedDateTime = DateTime.UtcNow,
                        VisitDateTime = DateTime.UtcNow.AddDays(2),
                        Comments = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        EmployeeId = employee.EmployeeId,
                        HasCar = true,
                        Plate = "AAA-BBBB",
                        VisitorId = visitor.VisitorId,
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
            int index = Randomize.Next(0, cities.Count);
            return cities[index];
        }

        private static string GetEmail()
        {
            List<string> emails = new List<string>() { 
                String.Format("dalexander@{0}", tenant),
                String.Format("tandersen@{0}", tenant),
                String.Format("adavis@{0}", tenant)
            };
            int index = Randomize.Next(0, emails.Count);
            return emails[index];
        }

        private static byte[] GetPicture(string fileName)
        {
            string path = new Uri(Assembly.GetAssembly(typeof(MyCompanyContextInitializer)).CodeBase).LocalPath;
            FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(path),
                                                         fileName), FileMode.Open, FileAccess.Read);

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
            int index = Randomize.Next(0, names.Count);
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
            int index = Randomize.Next(0, emails.Count);
            return emails[index];
        }
    }
}