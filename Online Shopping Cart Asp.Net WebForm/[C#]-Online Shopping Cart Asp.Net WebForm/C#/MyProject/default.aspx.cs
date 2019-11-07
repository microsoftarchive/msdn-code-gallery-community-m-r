using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProject
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //ImageButton ib = (ImageButton)Page.Master.FindControl("ImageButton2");
            //ib.Visible = true;
           
        }

        protected void sd(object source, DataListCommandEventArgs e)
        {
            
        }

        protected void onclick(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
           

            Response.Redirect("productDetail.aspx?pName=" + lb.Text);
        }

        protected void sd(object sender, DataListItemEventArgs e)
        {
            
        }

       

       
    }
}