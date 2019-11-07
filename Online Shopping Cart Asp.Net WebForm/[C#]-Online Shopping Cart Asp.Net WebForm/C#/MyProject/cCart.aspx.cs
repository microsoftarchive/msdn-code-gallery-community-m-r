using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProject
{
    public partial class cCart : System.Web.UI.Page
    {
        string url;
        decimal grandTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string s2 = System.Web.HttpContext.Current.User.Identity.Name;
            Response.Cookies["uname"].Value = s2;
            //string s3 = Request.Cookies["price"].Value;
            //Label1.Text = grandTotal.ToString();
            grandTotal = 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "price")) * Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));
                grandTotal = grandTotal + rowTotal;


            }
            Response.Cookies["price"].Value = grandTotal.ToString();
            TextBox2.Text = grandTotal.ToString();
            HyperLink1.ImageUrl = "~/img/str/buttons/checkout.jpg";
            HyperLink1.NavigateUrl = "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=wd.pra_1317033108_biz@gmail.com&image_url=http://server.myspace-shack.com/d20/nf165327.jpg" + "&item_name=Total of Cart&item_number=132&amount=" + grandTotal;
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            Response.Redirect("cCart.aspx");
        }

    }
}