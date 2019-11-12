using System;
using OnlineTradingSystem;

namespace WebApplication2.Account
{
    public partial class Register : System.Web.UI.Page
    {
        Customer customer;
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {

        }

        protected void B_Register_Click(object sender, EventArgs e)
        {
            customer = new Customer()
            {
                Username=TextBoxUserName.Text,
                email=TextBoxEmail.Text,
                password=TextBoxPassword.Text


            };


            dal.Insertnewclient(customer);

            Response.Redirect("Login.aspx");


        }
    }
}