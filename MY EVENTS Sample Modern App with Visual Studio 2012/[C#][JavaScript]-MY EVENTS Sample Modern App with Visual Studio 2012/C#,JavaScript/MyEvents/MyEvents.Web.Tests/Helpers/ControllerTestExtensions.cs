using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyEvents.Web.Tests.Helpers
{
    public static class ControllerTestExtensions
    {

        public static void SetFakeContext(this ControllerBase controller)
        {
            var sessionContext = new HttpSessionStateBaseFake();
            var httpContext = new HttpContextBaseFake(sessionContext);
            
            var controllerContext = new ControllerContextFake();
            controllerContext.HttpContext = httpContext;
            controller.ControllerContext = controllerContext;
        }

        public static void AddSessionValue(this ControllerBase controller, string key, object value)
        {
            HttpSessionStateBaseFake sessionContext = controller.ControllerContext.HttpContext.Session as HttpSessionStateBaseFake;
            sessionContext.Add(key, value);
        }

    }
}
