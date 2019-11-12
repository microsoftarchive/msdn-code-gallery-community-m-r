using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnlineTradingSystem;


namespace WebApplication2
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        LoginStatus sts;
        Label user;
        Customer cus = new Customer();
        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {

               
                sts = (LoginStatus)Page.Master.FindControl("LoginStatus1");
                sts.LoggedOut += new EventHandler(sts_LoggedOut);
                
            
             
            
            
            
            }


            else if (Session["login"] != null)
            {

                sts = (LoginStatus)Page.Master.FindControl("LoginStatus1");
               
                sts.LogoutText = "Log Out";

                

                cus = (Customer)Session["login"];
                 
                user = (Label)Page.Master.FindControl("UserName");

                user.Text ="Welcome! " +cus.Username.ToUpper();
            
            
            }
        


          
        }

        void sts_LoggedOut(object sender, EventArgs e)
        {
            sts = sender as LoginStatus;
            sts.LogoutText = "Log In";
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
           
        }
    }
}
