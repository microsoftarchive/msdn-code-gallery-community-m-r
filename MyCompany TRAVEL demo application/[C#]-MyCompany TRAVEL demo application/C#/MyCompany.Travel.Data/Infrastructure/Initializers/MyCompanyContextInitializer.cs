
namespace MyCompany.Travel.Data.Infrastructure.Initializers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using MyCompany.Travel.Model;
    using System.Configuration;
    using System.Threading.Tasks;

    /// <summary>
    /// The default initializer for testing . You can
    /// learn more about initializers in 
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
            CreateTravels(context);

            CreateQuestions(context);
        }

        private void CreateQuestions(MyCompanyContext context)
        {
            context.Questions.Add(new Question()
            {
                Text = "Why are software updates necessary?",
                Answer = "Microsoft is committed to providing its customers with software that has been tested for safety and security. Although no system is completely secure, we use processes, technology, and several specially focused teams to investigate, fix, and learn from security issues to help us meet this goal and to provide guidance to customers on how to help protect their PCs."
            });

            context.Questions.Add(new Question()
            {
                Text = "How can I keep my software up to date?",
                Answer = "Microsoft offers a range of online services to help you keep your computer up to date. Windows Update finds updates that you might not even be aware of and provides you with the simplest way to install updates that help prevent or fix problems, improve how your computer works, or enhance your computing experience. Visit Windows Update to learn more."
            });

            context.Questions.Add(new Question()
            {
                Text = "How do I find worldwide downloads?",
                Answer = "Microsoft delivers downloads in more than 118 languages worldwide. The Download Center now combines all English downloads into a single English Download Center. We no longer offer separate downloads for U.S. English, U.K. English, Australian English, or Canadian English."
            });

            context.Questions.Add(new Question()
            {
                Text = "How do I install downloaded software?",
                Answer = "Before you can use any software that you download, you must install it. For example, if you download a security update but do not install it, the update will not provide any protection for your computer."
            });

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
                    JobTitle = "Team Lead"
                });

                context.Teams.Add(new Team() { TeamId = id, ManagerId = id });
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
                    TeamId = i > (_employeeEmails.Count / 2) ? teamOne.TeamId : teamTwo.TeamId
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

        private void CreateTravels(MyCompanyContext context)
        {
            foreach (var employee in context.Employees)
            {
                int travelsCount = _randomize.Next(1, 10);

                if (employee.Email.Equals(_employeeEmails[0]) // Andrew Davis
                    || employee.Email.Equals(_employeeEmails[8])) // Carold Poland
                    travelsCount = 5;

                for (int i = 0; i < travelsCount; i++)
                {
                    string from = GetCity();
                    string to = GetCity();

                    var travel = new TravelRequest()
                        {
                            Name = string.Format("Travel from {0} to {1}", from, to),
                            Description = string.Format("Travel from {0} to {1}", from, to),
                            From = from,
                            To = to,
                            Depart = DateTime.UtcNow.AddDays(-7 + i * 3).Date,
                            Return = DateTime.UtcNow.AddDays(-5 + i * 3).Date,
                            CreationDate = DateTime.UtcNow.AddDays(-i * 3),
                            LastModifiedDate = DateTime.UtcNow,
                            AccommodationNeed = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                            TransportationNeed = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                            Comments = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                            RelatedProject = "MyCompany",
                            Status = GetStatus(i),
                            TravelType = TravelType.Roundtrip,
                            EmployeeId = employee.EmployeeId,
                        };

                    CreateAttachment(context, travel);
                    context.TravelRequests.Add(travel);
                }
            }

            context.SaveChanges();
        }

        private void CreateAttachment(MyCompanyContext context, TravelRequest travel)
        {
            context.TravelAttachments.Add(new TravelAttachment()
                {
                    Name = "Boarding Pass",
                    FileName = "BoardingPass.pdf",
                    Content = GetAttachment(),
                    TravelRequest = travel,
                }
            );
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

        private static TravelRequestStatus GetStatus(int currentIndex)
        {
            List<TravelRequestStatus> status = new List<TravelRequestStatus>() { 
                TravelRequestStatus.Pending, 
                TravelRequestStatus.Approved, 
                TravelRequestStatus.Completed,
                TravelRequestStatus.Denied,
            };

            if (currentIndex < 2) // To be sure that always are pending travel request
                return TravelRequestStatus.Pending;

            int index = _randomize.Next(0, status.Count);
            return status[index];
        }

        private static byte[] GetAttachment()
        {
            string path = new Uri(Assembly.GetAssembly(typeof(MyCompanyContextInitializer)).CodeBase).LocalPath;
            FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(path), GetAttachmentName()), FileMode.Open, FileAccess.Read);

            using (BinaryReader br = new BinaryReader(fs))
            {
                return br.ReadBytes((int)fs.Length);
            }
        }

        private static string GetAttachmentName()
        {
            List<string> emails = new List<string>() { 
                "FakeAttachments\\boardingpass.pdf", 
            };
            int index = _randomize.Next(0, emails.Count);
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