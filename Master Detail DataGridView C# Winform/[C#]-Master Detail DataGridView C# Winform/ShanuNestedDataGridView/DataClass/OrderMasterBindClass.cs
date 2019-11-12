using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Author      : Shanu
/// Create date : 2014-11-27
/// Description : Shanu Master grid Bind List Class
/// Latest
/// Modifier    : Shanu
/// Modify date : 2014-11-27
/// </summary>
namespace ShanuNestedDataGridView.DataClass
{

    class OrderMasterBindClass
    {
        public static List<OrderMasterBindClass> objMasterDGVBind = new List<OrderMasterBindClass>();
        public string ImgCol { get; set; }
        public string Order_No { get; set; }
        public string Table_ID { get; set; }
        public string Description { get; set; }
        public DateTime Order_DATE { get; set; }
        public string Waiter_ID { get; set; }

        public OrderMasterBindClass(string imgcol, string order_no, string table_id, string description, DateTime order_date, string waiter_id)
        {
            ImgCol = imgcol;
            Order_No = order_no;
            Table_ID = table_id;
            Description = description;
            Order_DATE = order_date;
            Waiter_ID = waiter_id;
        }   
    }
}
