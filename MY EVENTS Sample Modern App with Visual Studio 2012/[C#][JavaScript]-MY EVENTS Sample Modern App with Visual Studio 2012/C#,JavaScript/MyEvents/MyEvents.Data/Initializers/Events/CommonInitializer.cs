using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MyEvents.Model;

namespace MyEvents.Data.Initializers.Events
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommonInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        public static string FakeUserFacebookId = "100004295251408";
                                                   

        /// <summary>
        /// 
        /// </summary>
        public static string FakeUserName = "Orville McDonald";

        /// <summary>
        /// 
        /// </summary>
        public const string DefaultOrganizerBio = "Visual Studio Product Manager";

        /// <summary>
        /// 
        /// </summary>
        public const string DefaultSessionDescription = "Microsoft Visual Studio 2012 and the Microsoft .NET Framework 4.5 mark the next generation of developer tools from Microsoft. " +
           "Designed to address the latest needs of developers, Visual Studio delivers key innovations to address emerging requirements around Application Lifecycle Management (ALM). With Visual Studio 2012 and the .NET Framework 4.5," +
           "Microsoft delivers tooling and framework support for the latest innovations in application architecture, development, and deployment. The .NET Framework 4.5 contains numerous improvements that make it easier to develop powerful " +
           "and compelling applications. Attend this webcast to learn how Visual Studio 2012 and the .NET Framework 4.5 provide developers with the tooling support and the platform support needed to create amazing solutions. We also explore the core capabilities of these new technologies.";

        /// <summary>
        /// 
        /// </summary>
        public const string DefaultSpeakerBio = "He is a Product Manager of the Visual Studio";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static RegisteredUser GetOrganizer(MyEventsContext context, string name)
        {
            return context.RegisteredUsers.Single(r => r.Name == name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] LoadFile(string file)
        {
            string path = new Uri(Assembly.GetAssembly(typeof(MyEventsContextInitializer)).CodeBase).LocalPath;
            FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(path), file), FileMode.Open, FileAccess.Read);

            using (BinaryReader br = new BinaryReader(fs))
            {
                return br.ReadBytes((int)fs.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<RoomPoint> GetRoomPoints(int roomNumber)
        {
            var points = new List<RoomPoint>();

            if (roomNumber >= 1)
            {
                var room = new RoomPoint {PointX = 35, PointY = 14, RoomNumber = 1};
                points.Add(room);

                room = new RoomPoint {PointX = 34, PointY = 83, RoomNumber = 1};
                points.Add(room);

                room = new RoomPoint {PointX = 15, PointY = 84, RoomNumber = 1};
                points.Add(room);

                room = new RoomPoint {PointX = 13, PointY = 135, RoomNumber = 1};
                points.Add(room);

                room = new RoomPoint {PointX = 34, PointY = 136, RoomNumber = 1};
                points.Add(room);

                room = new RoomPoint {PointX = 34, PointY = 245, RoomNumber = 1};
                points.Add(room);

                room = new RoomPoint {PointX = 336, PointY = 242, RoomNumber = 1};
                points.Add(room);

                room = new RoomPoint {PointX = 336, PointY = 137, RoomNumber = 1};
                points.Add(room);

                room = new RoomPoint {PointX = 376, PointY = 136, RoomNumber = 1};
                points.Add(room);

                room = new RoomPoint {PointX = 142, PointY = 14, RoomNumber = 1};
                points.Add(room);
            }

            if (roomNumber >= 2)
            {
                var room = new RoomPoint {PointX = 445, PointY = 126, RoomNumber = 2};
                points.Add(room);

                room = new RoomPoint {PointX = 389, PointY = 241, RoomNumber = 2};
                points.Add(room);

                room = new RoomPoint {PointX = 763, PointY = 241, RoomNumber = 2};
                points.Add(room);

                room = new RoomPoint {PointX = 571, PointY = 139, RoomNumber = 2};
                points.Add(room);

                room = new RoomPoint {PointX = 559, PointY = 147, RoomNumber = 2};
                points.Add(room);

                room = new RoomPoint {PointX = 516, PointY = 128, RoomNumber = 2};
                points.Add(room);

                room = new RoomPoint {PointX = 487, PointY = 148, RoomNumber = 2};
                points.Add(room);
            }

            if (roomNumber >= 3)
            {
                var room = new RoomPoint {PointX = 460, PointY = 299, RoomNumber = 3};
                points.Add(room);

                room = new RoomPoint {PointX = 458, PointY = 455, RoomNumber = 3};
                points.Add(room);

                room = new RoomPoint {PointX = 675, PointY = 455, RoomNumber = 3};
                points.Add(room);

                room = new RoomPoint {PointX = 673, PointY = 351, RoomNumber = 3};
                points.Add(room);

                room = new RoomPoint {PointX = 551, PointY = 342, RoomNumber = 3};
                points.Add(room);
            }

            return points;
        }
    }
}
