using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Author      : Shanu
/// Create date : 2014-11-27
/// Description : Shanu Detail Grid Bind List Class
/// Latest
/// Modifier    : Shanu
/// Modify date : 2014-11-27
/// </summary>

namespace ShanuNestedDataGridView.DataClass
{
    class OrderDetailBindClass
    {
        
        public static List<OrderDetailBindClass> objDetailDGVBind = new List<OrderDetailBindClass>();
        public string Order_Detail_No { get; set; }
        public string Order_No { get; set; }
        public string Item_Name { get; set; }
        public string Notes { get; set; }
        public int Price { get; set; }
        public int QTY { get; set; }

        public OrderDetailBindClass(string order_detail_no, string order_no, string item_name, string notes ,int price,int qty)
        {
            Order_Detail_No = order_detail_no;
            Order_No = order_no;
            Item_Name = item_name;
            Notes = notes;
            Price = price;
            QTY = qty;
       
        }
    }
}
