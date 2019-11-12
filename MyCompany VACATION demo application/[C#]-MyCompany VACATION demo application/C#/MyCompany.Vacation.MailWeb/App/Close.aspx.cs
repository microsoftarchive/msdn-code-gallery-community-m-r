using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyCompany.Vacation.MailWeb.App
{
    public partial class Close : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["code"] = Request.QueryString["code"];
        }
    }
}