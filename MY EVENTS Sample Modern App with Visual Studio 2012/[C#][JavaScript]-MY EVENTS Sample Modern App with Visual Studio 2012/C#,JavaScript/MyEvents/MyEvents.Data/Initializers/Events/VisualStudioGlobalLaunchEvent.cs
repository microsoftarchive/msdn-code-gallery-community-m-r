using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MyEvents.Model;

namespace MyEvents.Data.Initializers.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class VisualStudioGlobalLaunchEvent : IEvent
    {
        private DateTime _startTime = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Create(MyEventsContext context)
        {
            _startTime = new System.DateTime(2012, 11, 12, 9, 0, 0);

            var eventDefinition = new EventDefinition()
                                      {                                          
                                          Name = "Visual Studio 2012 Global Launch (Seattle & On-Line)",
                                          Description =
                                              "Microsoft Visual Studio 2012 and the Microsoft .NET Framework 4.5 mark the next generation of developer tools from Microsoft. " +
                                              "Designed to address the latest needs of developers, Visual Studio delivers key innovations to address emerging requirements around Application Lifecycle Management (ALM). With Visual Studio 2012 and the .NET Framework 4.5," +
                                              "Microsoft delivers tooling and framework support for the latest innovations in application architecture, development, and deployment. The .NET Framework 4.5 contains numerous improvements that make it easier to develop powerful " +
                                              "and compelling applications. Attend this webcast to learn how Visual Studio 2012 and the .NET Framework 4.5 provide developers with the tooling support and the platform support needed to create amazing solutions. We also explore the core capabilities of these new technologies.",
                                          Address = "Bell Harbor Conference Center, 2211 Alaskan Way, Seattle, WA",
                                          City = "Seattle",
                                          Tags = "Visual Studio 2012",
                                          TwitterAccount = "@visualstudio",
                                          RoomNumber = 3,
                                          Date = _startTime.Date,
                                          StartTime = _startTime,
                                          EndTime = _startTime.AddHours(8).AddMinutes(30),
                                          TimeZoneOffset = 2,
                                          Logo = CommonInitializer.LoadFile("FakeImages\\usa-seattle.png"),                                          
                                          Latitude = 47.6113f,
                                          Longitude = -122.3489f,
                                          MapImage = CommonInitializer.LoadFile("FakeImages\\map.png"),                                          
                                          Sessions = this.Sessions
                                      };

            //(CDLTLL) Assign the existing default organizer or add it
            var existsOrganizer = context.RegisteredUsers.Any(ru => ru.FacebookId == CommonInitializer.FakeUserFacebookId);
            if (existsOrganizer)
            {
                RegisteredUser existingOrganizer = context.RegisteredUsers.Single(ru => ru.FacebookId == CommonInitializer.FakeUserFacebookId);
                eventDefinition.OrganizerId = existingOrganizer.RegisteredUserId;
            }
            else
            {
                eventDefinition.Organizer = this.Organizer;
            }
            //

            eventDefinition.RoomPoints = CommonInitializer.GetRoomPoints(eventDefinition.RoomNumber);
            context.EventDefinitions.Add(eventDefinition);
            context.SaveChanges();

        }

        
        private RegisteredUser Organizer
        {
            get
            {
                return new RegisteredUser()
                           {
                               FacebookId = CommonInitializer.FakeUserFacebookId,
                               Name = "Orville McDonald",
                               Email = "orville.mcdonald@microsoft.com",
                               City = "Redmond",
                               Bio = "Product Manager - Visual Studio",
                           };
            }
        }
        
        private Collection<Session> Sessions
        {
            get
            {
                return new Collection<Session>()
                           {                                           
                                   new Session()
                                   {
                                       Title = "Keynote Part 1: Visual Studio 2012 and modern app development",
                                       Description = "Embrace the new era of modern apps across connected devices and continuous services with Visual Studio 2012 while increasing your team's velocity with a modern app lifecycle.",
                                       Speaker = "S. Somasegar",
                                       Biography = "CVS, Developer Division",
                                       TwitterAccount = "@visualstudio",
                                       StartTime = _startTime.AddHours(0),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 1,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Keynote Part 2: Building modern apps with visual Studio 2012",
                                       Description = "See visual Studio 2012 in action, building a real modern app that takes advantage of the latest platforms and technologies to turn an idea into software",
                                       Speaker = "Jason Zander",
                                       Biography = "Corporate Vice President, Visual Studio",
                                       TwitterAccount = "@visualstudio",
                                       StartTime = _startTime.AddHours(1),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 0,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Keynote Part 3: Modern app lifecycle",
                                       Description = "Learn how your development team can increase business agility and focus on creating exceptional software while increasing velocity, ensuring quality, and continuosly delivering innovation.",
                                       Speaker = "Brian Harry",
                                       Biography = "Technical Fellow",
                                       TwitterAccount = "@visualstudio",
                                       StartTime = _startTime.AddHours(2),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 1,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Taking your business forward with Modern apps",
                                       Description = "Join us to learn about the changing state of IT and application development and learn about the “New Normal” for our industry. Get a broad-picture of modern application development and how trends like the “Consumerization of IT” and “Bring your own Device” are affecting what we design and build every day. See how the juxtaposition of Modern Apps versus Mission Critical Applications and concepts like “lean Startups” inside organizations fundamentally change the way we need to think about building, deploying and managing applications whether they are to consumer, business to business or internal. But most importantly, come and see the opportunities that the “New Normal” brings for your customers, your business and you.",
                                       Speaker = "Matt Nunn",
                                       Biography = "Product Manager - Dev Tools",
                                       TwitterAccount = "@mattnunn",
                                       StartTime = _startTime.AddHours(4),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 2,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "What is new in WF 4.5?",
                                       Description = "The next version of Windows Workflow Foundation (WF) arrives with many improvements based on key customers asks such as C# expressions, contract-first, designer improvements, side by side versioning of services, and State Machine. Watch this session to learn what is coming in WF4.5 and see some of these features in action.",
                                       Speaker = "Leon Welicki",
                                       Biography = "Senior Program Manager",
                                       TwitterAccount = "@visualstudio",
                                       StartTime = _startTime.AddHours(5),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 2,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Entity Framework 5",
                                       Description = "Modern app development requires a rich foundation as well as back-end services in which data access is critical. Entity Framework is the strategic data technology for Microsoft that helps to develop data-driven applications and domain-driven applications. Take a tour of the new features in Entity Framework 5 that are included in Visual Studio 2012. Learn to build an app that uses Code First, spatial data types, Code First Migrations, and Web API to display local parks on a webpage using Bing Maps.",
                                       Speaker = "Rowan Miller",
                                       Biography = "Program Manager",
                                       TwitterAccount = "@visualstudio",
                                       StartTime = _startTime.AddHours(6),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 2,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Developing Windows Azure Cloud Services using Visual Studio 2012",
                                       Description = "Looking for the best way to evolve your app to take advantage of the cloud?  Visual Studio 2012 and the Windows Azure SDK for .NET provide the solution that you need to create Cloud Services while streamlining the process with modern application lifecycle tools. In this session, you will get an overview of the tools you can use to quickly build and deploy cloud services to Windows Azure.",
                                       Speaker = "Paul Yuknewicz and Mohit Srivastava",
                                       Biography = "Program Manager Leads",
                                       TwitterAccount = "@mohit",
                                       StartTime = _startTime.AddHours(7),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 2,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Authoring XAML Windows Store apps in Visual Studio 2012 and Blend",
                                       Description = "The new Windows Store apps built using the XAML UI form a key pillar in the context of modern app development. You can now leverage your XAML skills (based on experience with WPF & Silverlight) to create new Windows 8 Store client apps!",
                                       Speaker = "Joanna Mason",
                                       Biography = "Principal Group Program Manager",
                                       TwitterAccount = "@JoannaMason",
                                       StartTime = _startTime.AddHours(8),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 2,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Agile development with Visual Studio 2012",
                                       Description = "We all want to “be more agile,” but this can introduce challenges to the way we collaborate as developers. Context switching is expensive; interruptions are distracting; it can be difficult to know how an individual developer ties into the broader development team; even code reuse across the team can lead to code maintenance issues in the future. In this session, attendees will learn about just a few of the ways that Visual Studio 2012 helps users collaborate with teams and focus on what they do best as developers: write high-quality code.",
                                       Speaker = "Brian Keller",
                                       Biography = "Visual Studio Evangelist",
                                       TwitterAccount = "@briankel",
                                       StartTime = _startTime.AddHours(4),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 3,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Taking ALM to the Cloud - A lap around the Team Foundation Service",
                                       Description = "Take a quick tour of the new Team Foundation Service, a cloud-based ALM solution. Learn how you and your team can easily manage your code base, prioritize work, and seamlessly connect through Visual Studio as it now exists in the cloud.",
                                       Speaker = "Aaron Bjork",
                                       Biography = "Principal Program Manager Lead",
                                       TwitterAccount = "@aaronbjork",
                                       StartTime = _startTime.AddHours(5),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 3,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "DevOps with System Center and Visual Studio",
                                       Description = "Developers and Operations engineers are increasingly working closer together to maintain always on services.  See how Visual Studio 2012 with System Center 2012 give developers and operations the tools to work seamlessly together to reduce the mean time to repair for defects in production applications.",
                                       Speaker = "Vitaly Gorbenko, Oji Udezue",
                                       Biography = "Product Manager",
                                       TwitterAccount = "@visualstudio",
                                       StartTime = _startTime.AddHours(6),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 3,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Introduction to software testing with Visual Studio 2012",
                                       Description = "Microsoft Test Manager, a member of the Visual Studio 2012 product family, is a tool, which was built from the ground up to support the unique needs of manual testers. It is a first-class tool for managing as well as tracking test cases and test runs and can help bridge the divide between testers and developers by capturing rich, actionable bugs. It also provides teams with the ability to benefit from lab-managed environments for easily automating build-deploy-test workflows. This video will provide an overview of Microsoft’s software testing tools vision.",
                                       Speaker = "Brian Keller",
                                       Biography = "Visual Studio Evangelist",
                                       TwitterAccount = "@briankel",
                                       StartTime = _startTime.AddHours(7),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 3,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Developer testing with Visual Studio 2012",
                                       Description = "From the ground up, the developer testing experience in Visual Studio 2012 was designed to allow developers to focus on their code and avoid unnecessary distractions. Test Explorer is now easily extended, allowing you to add third-party testing frameworks in addition to those shipped with Visual Studio. Visual Studio also includes the new Fakes framework to let developers create fast running and isolated unit tests. In this session, we will review the new developer testing experience in the context of a typical day-to-day workflow, showing how these features will help you quickly write better quality code.",
                                       Speaker = "Peter Provost",
                                       Biography = "Principal Program Manager Lead",
                                       TwitterAccount = "@pprovost",
                                       StartTime = _startTime.AddHours(8),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 3,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Lunch break!",
                                       Description = "--",
                                       Speaker = "Story Teller",
                                       Biography = "Story Teller",
                                       TwitterAccount = "@visualstudio",
                                       StartTime = _startTime.AddHours(0),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 0,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   }
                           };
            }
        }

        private List<Material> GetMaterials()
        {
            var materials = new List<Material>();

            var material = new Material();
            material.Name = "Presentation";
            material.ContentType = "image/jpeg";
            material.Content = CommonInitializer.LoadFile("FakeImages\\usa-seattle.png");

            materials.Add(material);

            return materials;
        }

    }
}
