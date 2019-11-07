using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MyProject
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   string usr=Request.Cookies["uname"].Value;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["shopingConnectionString1"].ConnectionString);
            string cmd="select * from UDetail ";
            SqlCommand command=new SqlCommand(cmd,con);
            con.Open();
            if(con.State==ConnectionState.Open)
            {
                SqlDataReader dtr=command.ExecuteReader();
                while(dtr.Read())
                {
                    if(dtr[2].ToString().Equals(usr))
                    {
                       TextBox1.Text= dtr[0].ToString();
                       TextBox2.Text=  dtr[1].ToString();
                        TextBox7.Text=  dtr[2].ToString();
                       TextBox4.Text= dtr[3].ToString();
                        TextBox5.Text= dtr[4].ToString();
                       TextBox6.Text=  dtr[5].ToString();
                        TextBox3.Text= dtr[6].ToString();
                                        
                    }
                    else continue;
                }
            }


        }

       
        
    }
}