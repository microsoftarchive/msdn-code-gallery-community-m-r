using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OwinSource
{
    class StartUpController
    {
        class RequestHandler
        {
            public string value { get; set; }
        }



        public void Configuration(IAppBuilder builder)
        {

            builder.Use<Middleware<RequestHandler>>();

            // with Controller

            builder.Map("/Step1", config =>
            {

                config.Use((context, next) => context.Response.WriteAsync("Hey from Step1 controller "));

            });

            // Default
            // builder.Use((context, next) => context.Response.WriteAsync("Hellow from shell"));



            // Get Operation will starts from here 

            get(builder, "/Step2", (context, next) => context.Response.WriteAsync("Get Data from API Service"));



            // Post Operations

            builder.Use((context, next) =>
            {
                if (context.Request.Method == "POST")
                {
                    var form = context.Get<RequestHandler>("Body");
                    // return context.Response.WriteAsync("Post Received :" + form.value);
                    return context.Response.WriteAsync("Post Received :");
                }
                return next();
            });

        }


        public static IAppBuilder get(IAppBuilder builder, string path, Func<IOwinContext, Func<Task>, Task> handler)
        {
            return builder.Use((context, next) =>
            {
                if (context.Request.Method == "GET")
                {
                    return handler(context, next);
                }
                return next();
            });

        }

    }
}
