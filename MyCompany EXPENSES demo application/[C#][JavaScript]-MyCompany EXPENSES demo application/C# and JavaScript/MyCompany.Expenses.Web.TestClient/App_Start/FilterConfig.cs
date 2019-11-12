using System.Web;
using System.Web.Mvc;

namespace MyCompany.Expenses.Web.TestClient
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
