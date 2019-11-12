using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace MyProject.reg
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
                        MailMessage mail = new MailMessage();
                        mail.To.Add("wd.prakash@gmail.com");
                        
                        mail.From = new MailAddress("pramuk97@gmail.com");
                        mail.Subject = "Email using Gmail";
                        string Body = "Hi, this mail is to test sending mail"+
                        "using Gmail in ASP.NET";
                        mail.Body = Body;
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                        smtp.Credentials = new System.Net.NetworkCredential
                        ("pramuk97@gmail.com","mukund1375");
                        //Or your Smtp Email ID and Password
                        smtp.EnableSsl = true;
                        smtp.Send(mail);

        }
    }
}