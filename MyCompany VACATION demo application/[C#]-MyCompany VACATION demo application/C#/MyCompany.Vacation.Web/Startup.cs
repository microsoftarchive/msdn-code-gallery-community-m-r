using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCompany.Vacation.Web
{
    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureSignalR(app);
        }
    }
}