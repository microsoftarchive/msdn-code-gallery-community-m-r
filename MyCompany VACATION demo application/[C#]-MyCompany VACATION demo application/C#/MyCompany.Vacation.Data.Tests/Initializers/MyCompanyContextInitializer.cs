namespace MyCompany.Vacation.Data.Test.Initializers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Model;
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
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];

        /// <summary>
        /// Seed
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyCompanyContext context)
        {
            CreateOffices(context);
            CreateMainManager(context);
            CreateHolidays(context);
            CreateTeams(context, 2);
            CreateEmployees(context, 3);
            CreateVacationRequests(context, 2);
            CreateEmployeePictures(context);
        }

        void CreateOffices(MyCompanyContext context)
        {
            context.Calendars.Add(new Calendar() { CalendarId = 1, Vacation = 22 });
            context.Offices.Add(new Office() { OfficeId = 1, CalendarId = 1 });
            context.SaveChanges();
        }

        void CreateHolidays(MyCompanyContext context)
        {
            context.CalendarHolidays.Add(new CalendarHolidays() 
                {
                    CalendarId = 1, 
                    Day = DateTime.UtcNow.AddDays(2), 
                    Name = "Holiday" 
                }
            );

            context.CalendarHolidays.Add(new CalendarHolidays()
                {
                    CalendarId = 1,
                    Day = DateTime.UtcNow.AddDays(12),
                    Name = "Holiday"
                }
            );
            context.SaveChanges();
        }

        void CreateTeams(MyCompanyContext context, int count)
        {
            int managerId = context.Employees.First().EmployeeId;
            for (int i = 0; i < count; i++)
            {
                context.Teams.Add(new Team() { ManagerId = managerId, TeamId = i, OfficeId = 1});
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
                OfficeId = 1,
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
                        OfficeId = 1,
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

        void CreateVacationRequests(MyCompanyContext context, int count)
        {
            foreach (var employee in context.Employees)
            {
                for (int i = 0; i < count; i++)
                {
                    context.VacationRequests.Add(new VacationRequest()
                    {
                        From = DateTime.UtcNow.AddDays(10 + 5 * i),
                        To = DateTime.UtcNow.AddDays(15 + 5 * i),
                        NumDays = 5,
                        Comments = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 
                        CreationDate = DateTime.UtcNow.AddDays(-1),
                        LastModifiedDate = DateTime.UtcNow,
                        Status = GetStatus(),
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
            int index = Randomize.Next(0, cities.Count);
            return cities[index];
        }

        private static VacationRequestStatus GetStatus()
        {
            List<VacationRequestStatus> status = new List<VacationRequestStatus>() { 
                VacationRequestStatus.Pending, 
                VacationRequestStatus.Approved, 
                VacationRequestStatus.Denied,
            };

            int index = Randomize.Next(0, status.Count);
            return status[index];
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
