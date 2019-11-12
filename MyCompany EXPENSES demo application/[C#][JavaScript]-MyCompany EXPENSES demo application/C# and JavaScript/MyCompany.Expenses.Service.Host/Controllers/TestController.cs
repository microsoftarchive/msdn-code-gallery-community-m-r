
namespace MyCompany.Expenses.Service.Host.Controllers
{
    using System;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Test Controller to validate that WebAPI works
    /// </summary>
    [Authorize]
    public class TestController : ApiController
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("Hello from OWIN!")
            };
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            string msg = String.Format("Hello from OWIN (id = {0})", id);
            return new HttpResponseMessage()
            {
                Content = new StringContent(msg)
            };
        }
    }

}
