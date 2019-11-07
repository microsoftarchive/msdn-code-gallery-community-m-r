using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace shanuMVCAngularJS_Chart.Controllers
{
    public class BarChartController : Controller
    {
        // GET: BarChart
        public ActionResult Index()
        {
            return View();
        }


		[HttpPost]
		public ActionResult ShanuSaveImage(string fileData)
		{
			string dataWithoutJpegMarker = fileData.Replace("data:image/jpeg;base64,", String.Empty);
			byte[] filebytes = Convert.FromBase64String(dataWithoutJpegMarker);
			string writePath = Path.Combine(Server.MapPath("~/images"), "shanuChart.jpg");
			using (FileStream fs = new FileStream(writePath,
											FileMode.OpenOrCreate,
											FileAccess.Write,
											FileShare.None))
			{
				fs.Write(filebytes, 0, filebytes.Length);
			}
			return new EmptyResult();
		}

	}
}