namespace MyCompany.Vacation.Data.Infraestructure.Initializers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using MyCompany.Vacation.Model;
    using System.Configuration;
    using MyCompany.Vacation.Data.Services;
    using MyCompany.Vacation.Data.Repositories;

    /// <summary>
    /// The default initializer for testing . You can learn more about initializers in 
    /// http://msdn.microsoft.com/en-us/library/gg696323(v=VS.103).aspx
    /// </summary>
    public class MyCompanyContextInitializer
        : DropCreateDatabaseIfModelChanges<MyCompanyContext>
    {
        private static readonly Random _randomize = new Random();
        private static string tenant = string.IsNullOrEmpty(ConfigurationManager.AppSettings["ida:Tenant"])
         ? ConfigurationManager.AppSettings["Tenant"]
         : ConfigurationManager.AppSettings["ida:Tenant"];
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

        List<string> comments = new List<string>()
            {
                "Easter week",
                "Family trip",
                "Summer vacation to go Spain!",
                "I will go to Bilbao few days",
                "Guggenheim Museum!",
                "They were not planned but I need to get them if possible.",
                "If there is any problem with this dates I could change to go on vacation another week.",
                "The work will be finished before going on vacation",
                "Christmas",
                "I come back to my country!! Yeah!",
                "Easter week",
                "Family trip",
                "Summer vacation to go Spain!",
                "I will go to Bilbao few days",
                "Guggenheim Museum!",
                "They were not planned but I need to get them if possible.",
                "If there is any problem with this dates I could change to go on vacation another week.",
                "The work will be finished before going on vacation",
                "Christmas",
                "I come back to my country!! Yeah!",
            };


        /// <summary>
        /// Seed
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyCompanyContext context)
        {
            CreateCalendars(context);
            CreateOffices(context);
            CreateTeamManagers(context);
            CreateEmployees(context);
            CreateEmployeePictures(context);
            CreateVacationRequests(context);
        }

        void CreateCalendars(MyCompanyContext context)
        {
            context.Calendars.Add(new Calendar() { CalendarId = 1, Vacation = 13 });

            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 1, 1),
                Name = "New Year's Day"
            });
            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 1, 21),
                Name = "Martin Luther King Day"
            });
            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 2, 18),
                Name = "Presidents' Day"
            });
            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 5, 27),
                Name = "Memorial Day"
            });
            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 7, 4),
                Name = "Independence Day"
            });
            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 9, 2),
                Name = "Labor Day"
            });
            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 10, 14),
                Name = "Columbus Day"
            });
            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 11, 11),
                Name = "Veterans Day"
            });
            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 11, 28),
                Name = "Thanksgiving Day"
            });
            context.CalendarHolidays.Add(new CalendarHolidays()
            {
                CalendarId = 1,
                Day = new DateTime(DateTime.UtcNow.Year, 12, 25),
                Name = "Christmas Day"
            });

            context.SaveChanges();
        }

        void CreateOffices(MyCompanyContext context)
        {
            context.Offices.Add(new Office() { OfficeId = 1, CalendarId = 1 });

            context.SaveChanges();
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
                    OfficeId = 1
                });

                context.Teams.Add(new Team() { TeamId = id, ManagerId = id, OfficeId = 1 });
            }

            context.SaveChanges();
        }

        private void CreateEmployees(MyCompanyContext context)
        {
            int initialId = context.Employees.Count() + 1;
            int employeesCount = _employeeEmails.Count + 1;

            var teamOne = context.Teams.OrderBy(t => t.TeamId).First();
            var teamTwo = context.Teams.OrderByDescending(t => t.TeamId).First();

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
                    TeamId = i > (_employeeEmails.Count / 2) ? teamOne.TeamId : teamTwo.TeamId,
                    OfficeId = i > (_employeeEmails.Count / 2) ? teamOne.OfficeId : teamTwo.OfficeId,
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

        void CreateVacationRequests(MyCompanyContext context)
        {
            var workingDaysCalculator = new WorkingDaysCalculator(new CalendarRepository(context));

            var now = DateTime.UtcNow.Date;
            int total = 0;
            foreach (var employee in context.Employees)
            {
                int requests = 2;
                for (int i = 0; i < requests; i++)
                {
                    int days = _randomize.Next(2,4);
                    var from = now.AddDays(10 + 5 * i);
                    var to = now.AddDays(14 + 5 * i);

                    int numDays = workingDaysCalculator.GetWorkingDays(employee.OfficeId, from, to);
                    var request = new VacationRequest()
                    {
                        From = from,
                        To = to,
                        NumDays = numDays,
                        Comments = GetComment(total),
                        CreationDate = DateTime.UtcNow.AddDays(-1),
                        LastModifiedDate = DateTime.UtcNow,
                        Status = (VacationRequestStatus)i + 1,
                        EmployeeId = employee.EmployeeId,
                    };
                    total++;
                    context.VacationRequests.Add(request);
                }
            }

            context.SaveChanges();
        }

        string GetComment(int index)
        {
            if (index < comments.Count())
                return comments[index];

            return string.Empty;
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
