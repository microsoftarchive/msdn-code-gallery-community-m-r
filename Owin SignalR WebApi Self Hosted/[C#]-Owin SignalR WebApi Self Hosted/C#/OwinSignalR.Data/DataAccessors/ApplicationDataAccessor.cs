using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using OwinSignalR.Data.Models;

namespace OwinSignalR.Data.DataAccessors
{
    public interface IApplicationDataAccessor 
    {
        Application FetchApplication(string apiToken);
    }

    public class ApplicationDataAccessor
        : DataAccessorBase, IApplicationDataAccessor
    {
        public Application FetchApplication(
            string apiToken)
        {
            var query = (from a in OwinSignalrDbContext.Applications
                         where a.ApiToken == apiToken
                         select a).Include("ApplicationReferralUrls");

            return query.FirstOrDefault();
        }
    }
}
