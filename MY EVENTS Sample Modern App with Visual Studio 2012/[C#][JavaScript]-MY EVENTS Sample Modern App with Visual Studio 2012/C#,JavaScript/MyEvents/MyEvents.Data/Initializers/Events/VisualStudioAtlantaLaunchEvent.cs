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
    public class VisualStudioAtlantaLaunchEvent : IEvent
    {
        private DateTime _startTime = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Create(MyEventsContext context)
        {
            _startTime = new System.DateTime(2012, 12, 14, 9, 0, 0);

            var eventDefinition = new EventDefinition()
            {
                Name = "Visual Studio 2012 - Atlanta Launch Event",
                Description =
                    "Discover how Visual Studio 2012 allows you to collaborate better and be more agile. See how it helps you turn big ideas into more compelling apps. Experience how it integrates best practices that accelerate development and deployment.  You’ll enjoy several sessions which will take Visual Studio, Team Foundation Server, and Test Professional through their paces to show off what’s possible with this incredible release!",
                Address = "1125 Sanctuary Pkwy., Alpharetta, GA 30009",
                City = "Atlanta",
                Tags = "Visual Studio 2012, SharePoint",
                TwitterAccount = "@visualstudio",
                RoomNumber = 2,
                Date = _startTime.Date,
                StartTime = _startTime,
                EndTime = _startTime.AddHours(8).AddMinutes(30),
                TimeZoneOffset = 2,
                Logo = CommonInitializer.LoadFile("FakeImages\\usa-atlanta.png"),                     
                Latitude = 34.0478f,
                Longitude = -84.3120f,
                MapImage = CommonInitializer.LoadFile("FakeImages\\map.png"),                
                Sessions = this.Sessions
            };

            //Assign the existing default organizer or add it
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
                                       Title = "Keynote: Modern development",
                                       Description = "In this keynote session we’ll show you the broad-picture of the modern development concept. Basically, we will introduce an overview of the whole event, mentioning terms like `Modern Apps` versus ‘Mission Critical Applications’. Then, introducing the possibilities for Native Apps development, Web Development, Continuous Services and finally ALM (Application Lifetime Management) providing Continuous value without barriers",
                                       Speaker = "Jason Zander",
                                       Biography = "Vice President Visual Studio",
                                       TwitterAccount = "@jasonz",
                                       StartTime = _startTime.AddHours(0),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 1,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Web & Cloud development",
                                       Description = "Take a journey through the features of the most significant Visual Studio release for web developers yet. You will explore the new ASP.NET MVC4, HTML5, CSS3 and JavaScript editors, mobile-friendly templates, a focus on responsive design as well as dedicated templates that leverage jQuery and jQuery mobile. You will also discover new tools inside Visual Studio, like Page-Inspector, highlighting both the big and the small features that increase both productivity and developer happiness. We will cover ASP.NET MVC4 web-sites, working with data using Entity-Framework 5, and building modern apps with Web-API Services consumed by HTML5 clients (JQuery & JavaScript). We will also focus on application deployment possibilities offered by Windows Azure.",
                                       Speaker = "Orville MacDonald",
                                       Biography = "Product Manager",
                                       TwitterAccount = "@orvillem",
                                       StartTime = _startTime.AddHours(1),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 1,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Building Windows Applications",
                                       Description = "The new Windows Store apps form a key pillar for the Modern App Development context. Those new Windows 8 apps are immersive apps with many new concepts in Windows. You can build these Windows Store apps using many different ways and technologies: XAML-C#/VB, HTML5-WinJS, C++.",
                                       Speaker = "Orville MacDonald",
                                       Biography = "Product Manager",
                                       TwitterAccount = "@orvillem",
                                       StartTime = _startTime.AddHours(2),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 1,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Tools for Teamwork",
                                       Description = "We all want to “be more agile,” but this can introduce challenges in the way we collaborate as developers. Context switching is expensive; interruptions are distracting; it can be difficult to know how an individual developer ties into the broader development team; even code reuse across the team can lead to code maintenance issues in the future. This session focuses on ALM areas but from a practical and developer point of view, presenting scenarios following a “Problem-Solution-Demo” approach. It covers topics like Product Backlog / Task Board, My Work, Local workspaces, Code Review, Code Comparison, Rollback, etc., all of them supported by Team Foundation Server and Visual Studio 2012.",
                                       Speaker = "Orville MacDonald",
                                       Biography = "Product Manager",
                                       TwitterAccount = "@orvillem",
                                       StartTime = _startTime.AddHours(3),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 1,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Developing for SharePoint 2010 with Visual Studio 2012",
                                       Description = "Modern App Development is tightly related with end-users work and collaborative applications, where SharePoint collaborative applications are key. In this session, you will see how the new SharePoint 2010 and SharePoint 2013 developer tools in Visual Studio 2012 help build SharePoint solutions more easily and efficiently. We will focus on news about productivity and development power when using Visual Studio 2012 for SharePoint 2010 application development.",
                                       Speaker = "Orville MacDonald",
                                       Biography = "Product Manager",
                                       TwitterAccount = "@orvillem",
                                       StartTime = _startTime.AddHours(4),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 1,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "C++ in Visual Studio 2012",
                                       Description = "This release of Visual Studio is big news for C++ developers. User interfaces for modern apps are getting increasingly graphically intensive and more common place. Whether you are creating an interactive UI for a Windows Store app or a 3D game, having the right tools will make your job easier. Take a tour of the tools and technologies in Visual Studio 2012 for applications and games that depend on DirectX. Learn how to create DirectX apps including writing shaders, working with graphics assets, and debugging your app. The target of this session will be around developing high performing and modern applications with C++ and Visual Studio 2012. In this talk, we will focus on three of the most important themes for C++ in the latest version of Visual Studio: 1.Windows 8 and Metro-style apps, 2.Productivity, 3.Performance.",
                                       Speaker = "Orville MacDonald",
                                       Biography = "Product Manager",
                                       TwitterAccount = "@orvillem",
                                       StartTime = _startTime.AddHours(5),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 1,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Application Quality enablement",
                                       Description = "With the rise of modern apps and the modern data center, the landscape for building applications is changing fast.  This new world requires a modern lifecycle approach that supports the need to increase velocity, deliver continuous value and manage change while enabling quality. This session will show you how you can ensure Application Quality using the Visual Studio 2012 platform. We will focus on topics like Backlog, Storyboarding, Task board, Exploratory Testing, Feedback, Unit Testing & Fakes, again, we’ll be following a practical “Problem-Solution-Demo” approach.",
                                       Speaker = "Orville MacDonald",
                                       Biography = "Product Manager",
                                       TwitterAccount = "@orvillem",
                                       StartTime = _startTime.AddHours(6),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 2,
                                       Duration = 60,
                                       Materials = GetMaterials()
                                   },
                                   new Session()
                                   {
                                       Title = "Development & Operations interaction",
                                       Description = "As software developers, our job isn’t done the moment we hand off a release to operations. When things go bump in the night, we should expect a phone call. But diagnosing issues in production is different from diagnosing issues in dev or test environments. You usually don’t have access to the same sets of tools, and issues can be difficult to reproduce. In this session, you will learn about a few of the ways Visual Studio 2012 is helping to ease this challenge and how to improve the interaction between the Development & Operations areas. We will focus on traditional conflictive areas between Devs & Ops which can be highly improved through topics like: TFS Work Items, SCOM Monitoring, AVI Code Bug and IntelliTrace in Production.",
                                       Speaker = "Orville MacDonald",
                                       Biography = "Product Manager",
                                       TwitterAccount = "@orvillem",
                                       StartTime = _startTime.AddHours(7),
                                       TimeZoneOffset = 2,
                                       RoomNumber = 2,
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
            material.Content = CommonInitializer.LoadFile("FakeImages\\visualstudio.png");

            materials.Add(material);

            return materials;
        }
        
    }
}
