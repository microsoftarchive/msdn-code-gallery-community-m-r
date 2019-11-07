using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ShanuMenuCreation_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/GetMenuDetails/")]
        List<MenuDataContract.MenuDetailDataContract> GetMenuDetails(); 
        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    public class MenuDataContract
    {
        [DataContract]
        public class MenuMasterDataContract
        {
            [DataMember]
            public string Menu_ID { get; set; }

            [DataMember]
            public string Menu_RootID { get; set; }

            [DataMember]
            public string Menu_ChildID { get; set; }

            [DataMember]
            public string UserID { get; set; }

        }

        [DataContract]
        public class MenuDetailDataContract
        {
            [DataMember]
            public string MDetail_ID { get; set; }

            [DataMember]
            public string Menu_RootID { get; set; }

            [DataMember]
            public string Menu_ChildID { get; set; }

            [DataMember]
            public string MenuName { get; set; }

            [DataMember]
            public string MenuDisplayTxt { get; set; }

            [DataMember]
            public string MenuFileName { get; set; }

            [DataMember]
            public string MenuURL { get; set; }

            [DataMember]
            public string UserID { get; set; }
        }

    }
}
