using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MasterDetailCRUD.Controllers
{
    public class OrderAPIController : ApiController
    {
		OrderManagementEntities objapi = new OrderManagementEntities();

		// to Search Student Details and display the result
		[HttpGet]
		public IEnumerable<USP_OrderMaster_Select_Result> Get(string OrderNO, string TableID)
		{
			if (OrderNO == null)
				OrderNO = "";
			if (TableID == null)
				TableID = "";

			return objapi.USP_OrderMaster_Select(OrderNO, TableID).AsEnumerable();

		}


		// To Insert new Student Details
		[HttpGet]
		public IEnumerable<string> insertOrderMaster(string Table_ID,string Description,string Waiter_Name)
		{
			return objapi.USP_OrderMaster_Insert( Table_ID, Description, Waiter_Name).AsEnumerable();
		}

		//to Update Student Details
		[HttpGet]
		public IEnumerable<string> updateOrderMaster(int OrderNo, string Table_ID, string Description, string Waiter_Name)
		{
			return objapi.USP_OrderMaster_Update(OrderNo, Table_ID, Description, Waiter_Name).AsEnumerable();
		}


		//to Delete Student Details
		[HttpGet]
		public IEnumerable<string> deleteOrderMaster(int OrderNo)
		{
			return objapi.USP_OrderMaster_Delete(OrderNo).AsEnumerable();
		}



		

	}
}
