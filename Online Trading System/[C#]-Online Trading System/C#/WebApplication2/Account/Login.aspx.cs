using System;
using OnlineTradingSystem;

namespace WebApplication2.Account
{
    public partial class Login : System.Web.UI.Page
    {
        Customer customer = new Customer();
       DAL dal=new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
         

        }

        protected void B_Log_in_Click(object sender, EventArgs e)
        {

            customer.Username = TextBoxUserName.Text;
            customer.password = TextBoxPassword.Text;

            customer=dal.ValidateUsernamePasswordCompatible(customer);

            if (customer.email == null)
            {

                Response.Write("Log in Faile");


            }
            else if (customer.email !=null)

            {

                Session["login"] = customer;

                Session["loginid"] = customer.CustomerId;
                Response.Redirect("~/Default.aspx");
              

               

            
            
            
            
            }


        }
    }
}