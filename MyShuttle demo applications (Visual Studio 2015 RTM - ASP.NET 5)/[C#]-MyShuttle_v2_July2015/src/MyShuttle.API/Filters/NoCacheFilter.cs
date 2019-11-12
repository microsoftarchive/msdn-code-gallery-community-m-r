namespace MyShuttle.API
{
    using Microsoft.AspNet.Mvc;
    using System;

    internal class NoCacheFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.HttpContext.Response.StatusCode == 200 &&
                actionExecutedContext.HttpContext.Response.Headers["CacheControl"] == null)
            {
                actionExecutedContext.HttpContext.Response.Headers["CacheControl"] = "no-cache";
                actionExecutedContext.HttpContext.Response.Headers["Pragma"] = "no-cache";

                if (actionExecutedContext.HttpContext.Response.Headers["Expires"] == null)
                {
                    actionExecutedContext.HttpContext.Response.Headers["Expires"] = DateTimeOffset.UtcNow.AddDays(-1).ToString();
                }
            }
        }
    }
}