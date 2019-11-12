using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineTradingSystem
{
  public  class Uregistration
    {

        
        #region DAta Member
        //DAta Member
        private string password;
        private string username;
        private string marketname;
        private string storename;
   

        #endregion

        #region constructor
        //defulte c'tor
        public   Uregistration()
        {
 
        }

        //1.oveloded constructor
        public Uregistration(string Password, string Usernamem, string Marketname, string Tabelname)
        {
            password = Password;
            username = Usernamem;
            marketname = Marketname;
            storename = Tabelname;
        }

        //2.oveloded constructor
        public Uregistration(string Password, string Username)
        {
            password = Password;
            username = Username;

        }
        
        #endregion

        #region Properties
        //Propertise Method
        public string Getpassword
        {
            get { return password; }
            set { password = value; }

        }

        public string Getusername
        {

            get { return username; }
            set { username = value; }

        }

        public string Getmarketname
        {
            get { return marketname; }
            set { marketname = value; }

        }

        public string Getstorename
        {
            get { return storename; }
            set { storename = value; }

        } 
        #endregion

     
    }




    [Serializable]
     class Product
    {
        public Int32 ID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public bool Instock { get; set; }
        public int Count { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Description { get; set; }
        public byte[] img { get; set; }
        public string url { get; set; }



        #region Method
        /* find the next id number that is not listed in the
         * data base and  not in the data gridview
         */
        public int FindNextNum(List<Int32> NumberId)
        {
           
            
            int Next = 1;

            for (int j = 0; NumberId.Count > j; j++)
            {
                for (int i = 0; NumberId.Count > i; i++)
                {


                    if (NumberId[i] == Next)
                    {
                       
                        Next = Next + 1;
                        continue;

                    }




                }
                


            }
            return Next;
        }
        // add product to the List
        public List<Product> AddList(List<Product> products, List<Int32> NumbersId)

        {
            List<Int32> Newnumberid = new List<Int32>();
            for (Int32 i = 0; products.Count > i; i++)
            {
                Newnumberid.Add(products[i].ID);
            
            }


             
            NumbersId = (NumbersId.Union(Newnumberid)).ToList();


                products.Add(new Product()
                {
                    ID = FindNextNum(NumbersId),

                    DateUpdate = DateTime.Now

                });



            return products;
        
        
        }
        #endregion

    }

    [Serializable]
     class Goods_bought
     {
        public Int32 OrderID { get; set; }
        public Int32 CustomerId { get; set; }
        
        public  int StoreId { get; set; }
        public Int32 ProductID { get; set; }
        public DateTime OrderDate { get; set; }
     
     }

    [Serializable]
     class Customer
     {
        private object p;

        public Customer(object p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        public Customer()
        {
            // TODO: Complete member initialization
        }
         public Int32 CustomerId { get; set; }
         public string Username { get; set; }
         public string email { get; set; }
         public string password { get; set; }
         public DateTime RegisterDate { get; set; }
     
     }


    public class Store : Uregistration
     {
       


         public Store()
         { }

        public Store(string Password, string Username)
        {
            // TODO: Complete member initialization
            Getpassword = Password;
            Getusername = Username;
        }



         public Int32 StoreId { get; set; }
         public DateTime RegisterDate { get; set; }

         
       
      
         
       
     
     
     
     }

      
        
       

      


}
