using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


//using System.Web.Mail;
using System.Net.Mail;
namespace MyProject.reg
{
    public partial class Password_recover : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string email = TextBox1.Text;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["shopingConnectionString1"].ConnectionString);
            string command = "select UserId,Pwd,Email from UDetail ";
            SqlCommand sqlcmd = new SqlCommand(command, con);
            //sqlcmd.Parameters["@Email"].Value = email;
            //sqlcmd.Parameters.Add("@Email", email);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                SqlDataReader dtr = sqlcmd.ExecuteReader();
                
                while (dtr.Read())
                {
                    if (dtr[2].ToString().Equals(TextBox1.Text))
                    {
                        MailMessage mail = new MailMessage();
                        mail.To.Add(dtr[2].ToString());
                        mail.From = new MailAddress("xyz@gmail.com");
                        mail.Subject = "Your userId and Password";
                        mail.Body = "Your<br/> UserId:<b>" + dtr[0].ToString() + "</b><br/>" + "Password:<b>" + dtr[1].ToString()+"</b>";
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Credentials = new System.Net.NetworkCredential("your id", "your password");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        Label1.Text = "check your mailbox for user iD and Password";
                        
                        string javaScript =  "<script language=JavaScript>\n" +  "alert('User Id and password send to Your mail box');\n" +  "</script>";
                        RegisterStartupScript("xyz", javaScript);
                        break;
                    }
                    else
                    {
                        Label1.Text = "Email Id not valid";
                    }
                   
                }
            }
        }
    }
}