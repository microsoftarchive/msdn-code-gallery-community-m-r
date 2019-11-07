using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MyProject
{
    public partial class AdminEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {   string s =@"~\img\product\"+FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(Server.MapPath(s));
            SqlConnection a=new SqlConnection(ConfigurationManager.ConnectionStrings["shopingConnectionString1"].ConnectionString);
            String cmd = "insert into product(productName,brand,image,catagory,price) values('"+nameText.Text+"','"+brandText.Text+"','"+s+"','"+catText.Text+"','"+priceText.Text+"')";

            SqlCommand b = new SqlCommand(cmd, a);
          /*  b.Parameters.Add("@pn",nameText.Text);
            b.Parameters.Add("@br",brandText.Text);
            b.Parameters.Add("@img",s);
            b.Parameters.Add("@cat","lappy");
            b.Parameters.Add("@pr",priceText.Text);
            */
            a.Open();
            b.ExecuteNonQuery();
            a.Close();

        } 
    }
}