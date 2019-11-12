
namespace MyShuttle.API
{
    using Microsoft.AspNet.Identity;
    using Model;
    using System;
    using Microsoft.Framework.DependencyInjection;
    using System.Security.Principal;
    using System.Threading.Tasks;



    /// <summary>
    /// This is a fake class up to integrate WAAD security
    /// </summary>
    public class MyShuttleSecurityContext
    {
        IServiceProvider _serviceProvider = null;

        // Default values for demos without authentication
        private static readonly int _defaultCustomerId = 1;
        private static readonly int _defaultEmployeeId = 1;
        private static readonly int _defaultCarrierId = 1;
        private static readonly string _defaultPassword = "Shuttle@1";

        public MyShuttleSecurityContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public int EmployeeId
        {
            get
            {
                return _defaultEmployeeId;
            }
        }

        public int CustomerId
        {
            get
            {
                return _defaultCustomerId;
            }
        }

        public string DefaultPassword
        {
            get
            {
                return _defaultPassword;
            }
        }

        public async Task<int> GetCarrierId(IIdentity identity)
        {
            if (identity.IsAuthenticated)
            {
                var userManager = _serviceProvider.GetService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByNameAsync(identity.Name);
                return user.CarrierId;
            }

            return _defaultCarrierId;
        }
    }
}