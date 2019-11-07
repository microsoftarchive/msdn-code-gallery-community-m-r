using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Payment_Example
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

 
      protected void btnOrder_Click(object sender, EventArgs e)
{
    try
    {
        if (!Page.IsValid) return;

        // Create the order in your DB and get the ID
        string amount;
        string orderId = "Test1";
        string name = "Putchase a laptop, Order #" + orderId;
        string description = "Laptop";

        amount = txtamoutt.Text;
        string site = "http://www.ourcode.co.za";
        string merchant_id = "";
        string merchant_key = "";

        // Check if we are using the test or live system
        string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

        if (paymentMode == "test")
        {
            site = "https://sandbox.payfast.co.za/eng/process?";
            merchant_id = "";
            merchant_key = "";
        }
       if( paymentMode == "live" )
        {
            site = "https://www.payfast.co.za/eng/process?";
            merchant_id = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantID"];
            merchant_key = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantKey"];
        }
        else
        {
            throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
        }

        // Build the query string for payment site

        StringBuilder str = new StringBuilder();
        str.Append( "merchant_id=" + HttpUtility.UrlEncode( merchant_id ) );
        str.Append( "&merchant_key=" + HttpUtility.UrlEncode( merchant_key ) );
        //str.Append( "&return_url=" + HttpUtility.UrlEncode( System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"] ) );
        //str.Append( "&cancel_url=" + HttpUtility.UrlEncode( System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"] ) );
        //str.Append( "¬ify_url=" + HttpUtility.UrlEncode( System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"] ) );

        str.Append( "&m_payment_id=" + HttpUtility.UrlEncode( orderId ) );
        str.Append( "&amount=" + HttpUtility.UrlEncode( amount ) );
        str.Append( "&item_name=" + HttpUtility.UrlEncode( name ) );
        str.Append( "&item_description=" + HttpUtility.UrlEncode( description ) );

        // Redirect to PayFast
        Response.Redirect(site + str.ToString());
    }
    catch (Exception ex)
    {
       // Handle your errors here (log them and tell the user that there was an error)
    }
      }

    }
}