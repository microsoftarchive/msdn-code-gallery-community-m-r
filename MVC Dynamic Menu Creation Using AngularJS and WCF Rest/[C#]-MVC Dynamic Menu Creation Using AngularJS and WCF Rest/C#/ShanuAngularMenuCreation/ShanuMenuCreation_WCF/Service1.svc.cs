using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ShanuMenuCreation_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        ShanuMenuCreation_WCF.MenuDBEntities OME;

        public Service1()
        {
            OME = new ShanuMenuCreation_WCF.MenuDBEntities();
        }

        public List<MenuDataContract.MenuDetailDataContract> GetMenuDetails()
        {
            ////var query = (from a in OME.MenuDetails
            ////             select a).Distinct();
             var query = (from A in OME.MenuMaster   
                         join B in OME.MenuDetails on A.Menu_ChildID equals B.Menu_ChildID 
                         select new   
                         {   
                            A.Menu_RootID,
                            B.MDetail_ID,
                            B.Menu_ChildID,
                            B.MenuName,
                            B.MenuDisplayTxt,
                            B.MenuFileName,
                            B.MenuURL ,
                            B.UserID
                         }).ToList().OrderBy(q => q.MDetail_ID);  

            List<MenuDataContract.MenuDetailDataContract> MenuList = new List<MenuDataContract.MenuDetailDataContract>();

            query.ToList().ForEach(rec =>
            {
                MenuList.Add(new MenuDataContract.MenuDetailDataContract
                {
                    MDetail_ID = Convert.ToString(rec.MDetail_ID),
                    Menu_RootID = rec.Menu_RootID,
                    Menu_ChildID = rec.Menu_ChildID,
                    MenuName = rec.MenuName,
                    MenuDisplayTxt = rec.MenuDisplayTxt,
                    MenuFileName = rec.MenuFileName,
                    MenuURL = rec.MenuURL,
                    UserID = rec.UserID,
                });
            });
            return MenuList;
        }      
    }
}
