using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MasterDetailCRUD.Controllers
{
    public class DetailAPIController : ApiController
    {

		OrderManagementEntities objapi = new OrderManagementEntities();

		// to Search Student Details and display the result
		[HttpGet]
		public IEnumerable<USP_OrderDetail_Select_Result> Get(string OrderNO)
		{
			if (OrderNO == null)
				OrderNO = "0";


			return objapi.USP_OrderDetail_Select(OrderNO).AsEnumerable();

		}


		// To Insert new Student Details
		[HttpGet]
		public IEnumerable<string> insertOrderDetail(string Order_No, string Item_Name, string Notes, string QTY, string Price)
		{
			return objapi.USP_OrderDetail_Insert(Order_No, Item_Name, Notes, QTY, Price).AsEnumerable();
		}

		//to Update Student Details
		[HttpGet]
		public IEnumerable<string> updateOrderDetail(int Order_Detail_No, string Order_No, string Item_Name, string Notes, string QTY, string Price)
		{
			return objapi.USP_OrderDetail_Update(Order_Detail_No, Order_No, Item_Name, Notes, QTY, Price).AsEnumerable();
		}


		//to Delete Student Details
		[HttpGet]
		public IEnumerable<string> deleteOrderDetail(int Order_Detail_No)
		{
			return objapi.USP_OrderDetail_Delete(Order_Detail_No).AsEnumerable();
		}
	}
}
