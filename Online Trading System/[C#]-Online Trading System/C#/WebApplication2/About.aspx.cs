using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using OnlineTradingSystem;




namespace WebApplication2
{
    public partial class About : System.Web.UI.Page
    {
        List<Product> Prod = new List<Product>();
        List<Goods_bought> goods = new List<Goods_bought>();

        Customer cus = new Customer();

        Store str = new Store();
      
        List<Panel> p = new List<Panel>();

        List<CheckBox> buy = new List<CheckBox>();

        List<AsyncPostBackTrigger> trigger = new List<AsyncPostBackTrigger>();

        DAL dalsql = new DAL();

           String Name = "ProductName";
           String Price = "ProductPrice";
           String Count = "ProductCount";
           String Discript = "ProductDiscript";
           String Category = "ProductCategory";
           String DateUpdate = "ProductDateUpdate";
           String Instock = "ProductInstock";


           List<String> arrValue = new List<String>();
           List<String> arrPrice = new List<String>();
           List<String> arrCount = new List<String>();
           List<String> arrDiscript = new List<String>();
           List<String> arrCategory = new List<String>();
           List<String> arrDateUpdate = new List<String>();
           List<String> arrInstock = new List<String>();

      

        protected void Page_PreInit(object sender, EventArgs e)
        {
            
   
            try
            {
                Array.ForEach(Directory.GetFiles(@"C:\Documents and Settings\user\My Documents\Visual Studio 2010\Projects\OnlineTradingSystem\WebApplication2\Images\", "imageTest*.jpg"),
                              delegate(string path) { File.Delete(path); });
            }
            catch
            {

                return;

            }


        }

        protected void Page_Init(object sender, EventArgs e)
        {

          
            // ask what the user chouse
            int storeId = Convert.ToInt16( Request.QueryString["StoreId"]);
           
            Store regis = new Store { StoreId = storeId };
            regis=dalsql.ImportStoreDataById(regis);
            storenamelabel.Text = regis.Getstorename;
            //Ask for the selected number of product 
            Prod= dalsql.ImportSqlData(regis);

            ViewState["Store"] = regis;
           

            // Define the array name and values.
        //    String arrName = "MyArray";
        //    List<String> arrValue = new List<String>(){
        //    "\""+Prod[1].ProductName.ToString()+"\""
        //};

        //    arrValue.Add("\""+Prod[2].ProductName+"\"");
        //    string arrValue1 = string.Join(",", arrValue.ToArray());
         
             
       

            // Get a ClientScriptManager reference from the Page class.
            //ClientScriptManager cs = Page.ClientScript;

            // Register the array with the Page class.
            //cs.RegisterArrayDeclaration(arrName, arrValue1);
           
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            BindProducts();



               ClientScriptManager cs=Page.ClientScript; 

      
           




            for (int i = 0; Prod.Count > i; i++)
            {
               
                p.Add(new Panel()
                {
                    ID = "panelnum" + Prod[i].ID,
                    ToolTip = "hy",
                    CssClass = "front"



                });
               

                Label label = new Label()
                {
                    ID = "label" + Prod[i].ID,
                    Text = Prod[i].ProductName,
                    ForeColor = System.Drawing.Color.Black,
                    CssClass = "Label"
                };

              
                    
                try
                {
                    //check if image exist
                    if ((Prod[i].img != null) && (Prod[i].img.Length > 200))
                    {
                        MemoryStream ms = new MemoryStream(Prod[i].img);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                        image.Save(@"C:\Documents and Settings\user\My Documents\Visual Studio 2010\Projects\OnlineTradingSystem\WebApplication2\Images\imageTest" + Convert.ToString(i) + ".jpg");
                    }
                }

                catch
                {
                
                }

                Image img = new Image()
                {

                    ID = "imagnum" + Prod[i].ID,
                    Height = 171,
                    Width = 150,
                    BorderColor = System.Drawing.Color.Gray,
                    ImageUrl = "~/Images/imageTest" + Convert.ToString(i) + ".jpg",
                    CssClass = Prod[i].url
                };


                /*
                Context.Response.ContentType = "image/jpeg";
                Context.Response.OutputStream.Write(Prod[i].img, 0, Prod[i].img.Length);
              */



                Label price = new Label()
                {
                    ID = "Price" + Prod[i].ID,
                    Text = "Price :" + Prod[i].Price + "$",
                    ForeColor = System.Drawing.Color.LightGreen,
                    CssClass = "price"

                };
                

                buy.Add(new CheckBox()
                {
                    ID = "Buy" + Prod[i].ID,
                    Text = "BUY",
                    ForeColor = System.Drawing.Color.Gold,
                    BorderColor = System.Drawing.Color.Black


                });
                buy[i].AutoPostBack = true;

                int b = i;

                trigger.Add(new AsyncPostBackTrigger()
                {
                    ControlID = buy[i].ID,
                    EventName = "CheckedChanged",
                   
                    
                    


                });

             
                
                buy[b].CheckedChanged += delegate(object s, EventArgs ep) {buy_CheckedChanged(s, ep,Prod[b]); };

                updatepanel1.Triggers.Add(trigger[b]);
                 
                Button butt_info = new Button() {
                   ID =i.ToString(),
                   OnClientClick="return false",
                   Text="Info"
                   
                   
                };
                
               
               
                int z=i+1;

                 

                arrValue.Add("\"" + Prod[i].ProductName + "\"");
                arrCategory.Add("\"" + Prod[i].Category + "\"");
                arrDateUpdate.Add("\"" + Convert.ToString(Prod[i].DateUpdate) + "\"");
                arrDiscript.Add("\"" + Prod[i].Description + "\"");
                arrPrice.Add("\"" + Convert.ToString( Prod[i].Price) + "\"");
                arrInstock.Add("\"" + Convert.ToString( Prod[i].Instock) + "\"");
                arrCount.Add("\"" + Convert.ToString(Prod[i].Count) + "\"");
              
                
               // butt_info.Click += new EventHandler(butt_info_Click);

                p[i].Controls.Add(label);
                p[i].Controls.Add(img);
                p[i].Controls.Add(price);
                p[i].Controls.Add(buy[i]);
                p[i].Controls.Add(butt_info);
             
                Panel2.Controls.Add(p[i]);
               
                Panel2.Wrap = true;







            }


            // export all the data to JAvaScript
            String NameProd = String.Join(",", arrValue.ToArray());
            String CategoryProd = String.Join(",", arrCategory.ToArray());
            String CountProd = String.Join(",", arrCount.ToArray());
            String DateUpdateProd = String.Join(",", arrDateUpdate.ToArray());
            String DiscriptProd = String.Join(",", arrDiscript.ToArray());
            String InstockProd = String.Join(",", arrInstock.ToArray());
            String PriceProd = String.Join(",", arrPrice.ToArray());


            // Get a ClientScriptManager reference from the Page class.


            // Register the array with the Page class.
            cs.RegisterArrayDeclaration(Name, NameProd);
            cs.RegisterArrayDeclaration(Category, CategoryProd);
            cs.RegisterArrayDeclaration(Count, CountProd);
            cs.RegisterArrayDeclaration(DateUpdate, DateUpdateProd);
            cs.RegisterArrayDeclaration(Discript, DiscriptProd);
            cs.RegisterArrayDeclaration(Instock, InstockProd);
            cs.RegisterArrayDeclaration(Price, PriceProd);
             
            
        }



       
    
        


   



        private void BindProducts()
        {
            if (ViewState["Products"] != null)
            {
                GridView1.DataSource = (List<Product>)ViewState["Products"];
                GridView1.DataBind();
               
            }
            else
            {
                List<Product> Products = new List<Product>();
                ViewState["Products"] = Products;

                List<Goods_bought> goods = new List<Goods_bought>();
                ViewState["Goods"] = goods;
              
            }
        }


 
      
       
   
        
       

       void buy_CheckedChanged(object sender, EventArgs e,Product prod)
        {



            CheckBox buy = sender as CheckBox;
            Store shop = new Store();
            if (buy.Checked == true)
            {
             
               
                ((List<Product>)ViewState["Products"]).Add(prod);
            
                BindProducts();

                
                 
                GridView1.DataBind();


               cus = (Customer)Session["login"];
               shop = (Store)ViewState["Store"];

               if ((shop != null) && (cus != null))
               {
                   goods.Add ( new Goods_bought
                   {

                       ProductID = prod.ID,
                       CustomerId = cus.CustomerId,
                       StoreId = shop.StoreId,
                       OrderDate=DateTime.Now

                   });


                   ((List<Goods_bought>)ViewState["Goods"]).Add(goods[0]);

               }

            }

            else if (buy.Checked == false)
            {

                 ((List<Product>)ViewState["Products"]).RemoveRange(1,1) ;

                BindProducts();
                GridView1.DataBind();
            
            
            
            
            }


 



        }
       string myScriptValue;
        protected void buy_all_product(object sender, EventArgs e)
        {

            goods.AddRange(((List<Goods_bought>)ViewState["Goods"]));
          
            if (goods.Count > 0)
            {


                dalsql.ExportCustomerGoods(goods);
                ((List<Goods_bought>)ViewState["Goods"]).Clear();
                ((List<Product>)ViewState["Products"]).Clear(); ;

                BindProducts();

                GridView1.DataBind();
              
              

            }

            else
            { 
            myScriptValue = "alert('You have to login first!')";
 
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScriptName", myScriptValue, true);
             
            
            
            }

          

        
        
        }


    
       protected void mygrid_RowDataBound(object sender, GridViewRowEventArgs e)
       {
           if (e.Row.Cells.Count > 1)


           {
                   e.Row.Cells[4].Visible = false;
                   e.Row.Cells[5].Visible = false;
                   e.Row.Cells[6].Visible = false;
                   e.Row.Cells[7].Visible = false;
                   e.Row.Cells[8].Visible = false;
                   
               
           }
       }    

       

   
    }
}
