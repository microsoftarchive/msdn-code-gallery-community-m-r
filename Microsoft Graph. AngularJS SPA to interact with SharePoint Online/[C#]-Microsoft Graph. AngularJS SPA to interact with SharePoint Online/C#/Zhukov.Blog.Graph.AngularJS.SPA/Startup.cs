using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(Zhukov.Blog.Graph.AngularJS.SPA.Startup))]

namespace Zhukov.Blog.Graph.AngularJS.SPA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("default.html");

            // Файл по умолчанию
            app.UseDefaultFiles(options);

            // Статические файлы
            app.UseStaticFiles();
        }
    }
}
