using System;
using System.IO;
using System.Web.Hosting;

namespace TaskTracker
{
    public class ACLElem
    {
        public static string ACLXMLFilePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "ACL.xml");

        public string ObjectId { get; set; }
        public string MappingId { get; set; }

        //Parameterless constructor required for xml serialization
        public ACLElem(){}

        public ACLElem(string objectId, string mappingId)
        {
            this.ObjectId = objectId;
            this.MappingId = mappingId;
        }
    }
}