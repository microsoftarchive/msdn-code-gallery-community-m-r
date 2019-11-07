using System;
using System.IO;
using System.Web.Hosting;

namespace TaskTracker
{
    public class RoleMapElem
    {
        public static string[] Roles = { "Admin", "Observer", "Writer", "Approver" };
        public static string RoleMapXMLFilePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "Roles.xml");

        public string ObjectId {get; set;}
        public string Role { get; set; }
        public string MappingId { get; set; }

        //Parameterless constructor required for xml serialization
        public RoleMapElem(){}

        public RoleMapElem(string objectId, string role, string mappingId)
        {
            this.ObjectId = objectId;
            this.Role = role;
            this.MappingId = mappingId;
        }

    }
}