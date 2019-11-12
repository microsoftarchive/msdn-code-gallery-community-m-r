
namespace MyCompany.Travel.Web
{
    using Microsoft.Owin.Security.WindowsAzure;
    using Owin;
    using System.Configuration;

	/// <summary>
	/// Configure OAuth Security
	/// </summary>
    public partial class Startup
    {
		/// <summary>
        /// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		/// </summary>
		/// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseWindowsAzureBearerToken(
                new WindowsAzureJwtBearerAuthenticationOptions
                {
                    Audience = ConfigurationManager.AppSettings["ida:Audience"],
                    Tenant = ConfigurationManager.AppSettings["ida:Tenant"]
                });
        }
    }
}