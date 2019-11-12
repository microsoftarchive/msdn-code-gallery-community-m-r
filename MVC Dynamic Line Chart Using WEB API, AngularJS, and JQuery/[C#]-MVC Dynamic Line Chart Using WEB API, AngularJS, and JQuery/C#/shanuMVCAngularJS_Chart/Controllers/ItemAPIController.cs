using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace shanuMVCAngularJS_Chart.Controllers
{
    public class ItemAPIController : ApiController
    {
		ItemsDBEntities objapi = new ItemsDBEntities();

		// To get all Item chart detaiuls
		[HttpGet]
		public IEnumerable<USP_Item_Select_Result> getItemDetails(string ItemName)
		{
			if (ItemName == null)
				ItemName = "";
			return objapi.USP_Item_Select(ItemName).AsEnumerable();
		}

		// To get maximum and Minimum value
		[HttpGet]
		public IEnumerable<USP_ItemMaxMin_Select_Result> getItemMaxMinDetails(string ItemNM)
		{
			if (ItemNM == null)
				ItemNM = "";
			return objapi.USP_ItemMaxMin_Select(ItemNM).AsEnumerable();
		}

		// To Insert/Update Item Details
		[HttpGet]
		public IEnumerable<string> insertItem(string itemName, string SaleCount)
		{
			return objapi.USP_Item_Edit(itemName,SaleCount).AsEnumerable();
		}


	}
}
