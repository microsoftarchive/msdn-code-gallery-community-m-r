using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using OnlineTradingSystem;

namespace WebApplication2
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();
            List<Store> store = new List<Store>();
            store = dal.ImportStoreName();
            

            ListItem listItem = new ListItem();
            for (int i = 0; store.Count > i; i++)
            {

               
                HyperLink DynLink = new HyperLink()
                {
                    
                    NavigateUrl = "~/About.aspx?StoreId=" +store[i].StoreId,
                    Text = store[i].Getstorename



                };



                listItem = new ListItem() { Text = DynLink.Text, Value = DynLink.NavigateUrl };
                BulletedList1.DisplayMode = BulletedListDisplayMode.HyperLink;

                BulletedList1.Items.Add(listItem);

            }




        }



    }
}
