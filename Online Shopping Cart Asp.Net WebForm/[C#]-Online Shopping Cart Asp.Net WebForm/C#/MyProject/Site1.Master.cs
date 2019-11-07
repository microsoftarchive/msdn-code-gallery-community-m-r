using System;
using System.Web.UI;

namespace MyProject
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                ImageButton1.Visible = false;
                if (Request.Cookies["role"].Value == "admin")
                {
                    ImageButton2.Visible = true;
                    ImageButton3.Visible = true;
                    
                    
                }
                else
                {
                    
                    ImageButton2.Visible = false;
                    ImageButton3.Visible = false;
                    
                }

            }
            else
            {
                ImageButton1.Visible = true;
                ImageButton2.Visible = false;
            }
            

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        }

       
    }
}