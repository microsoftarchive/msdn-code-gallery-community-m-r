using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
namespace MyProject
{
    public partial class productDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{       
          
        //    int i=int.Parse(TextBox1.Text);

        //    int p = int.Parse((string)ViewState["price"]);
        //    string img = ViewState["image"].ToString();
        //    string brand = ViewState["brand"].ToString();
        //    string s2=System.Web.HttpContext.Current.User.Identity.Name;
        //    string s1 = Request.QueryString["pName"];

        //    Label1.Text = img + " brand:" + brand + "  price:" + p;


        //    SqlConnection a = new SqlConnection(ConfigurationManager.ConnectionStrings["shopingConnectionString1"].ConnectionString);

        //    a.Open();
        //    string cartCmd = "insert into completeCart(pName,quantity,uName,brand,img,price) values('" + s1 + "','" + i + "','" + s2 + "','" + brand + "','" + img + "','" + p + "') ";
        //    SqlCommand cmd = new SqlCommand(cartCmd, a);
        //    cmd.ExecuteNonQuery();
        //    a.Close();

        //    Response.Redirect("cCart.aspx?user=" + s2);

           
        //}

        protected void use(object sender, DataListItemEventArgs e)
        {
            Label prd = (Label)e.Item.FindControl("productNameLabel");
            ViewState["poductName"] = prd.Text;
            Label brd = (Label)e.Item.FindControl("brandLabel");
            ViewState["brand"] = brd.Text;
            Image img = (Image)e.Item.FindControl("Image1");
            ViewState["image"] = img.ImageUrl.ToString();
            Label prc = (Label)e.Item.FindControl("priceLabel");
            ViewState["price"] = prc.Text;
           /* Label img = (Label)e.Item.FindControl("Image1");
            Label1.Text += img.Text;
                
        */
            
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            int i = int.Parse(TextBox1.Text);

            int p = int.Parse((string)ViewState["price"]);
            string img = ViewState["image"].ToString();
            string brand = ViewState["brand"].ToString();
            string s2 = System.Web.HttpContext.Current.User.Identity.Name;
            string s1 = Request.QueryString["pName"];

            


            SqlConnection a = new SqlConnection(ConfigurationManager.ConnectionStrings["shopingConnectionString1"].ConnectionString);

            a.Open();
            string cartCmd = "insert into completeCart(pName,quantity,uName,brand,img,price) values('" + s1 + "','" + i + "','" + s2 + "','" + brand + "','" + img + "','" + p + "') ";
            SqlCommand cmd = new SqlCommand(cartCmd, a);
            cmd.ExecuteNonQuery();
            a.Close();

            Response.Redirect("cCart.aspx?user=" + s2);

        }

        

    }
}