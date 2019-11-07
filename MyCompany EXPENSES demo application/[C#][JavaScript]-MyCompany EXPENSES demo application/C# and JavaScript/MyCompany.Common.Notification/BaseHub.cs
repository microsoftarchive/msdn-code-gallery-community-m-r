namespace MyCompany.Common.Notification
{
    using Microsoft.AspNet.SignalR;
    using MyCompany.Common.Security.Web;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    internal class User
    {
        public string Id { get; set; }
        public bool IsRRHH { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }

    /// <summary>
    /// Base hub for SignalR notifications.
    /// To scaleout with SQL Server see:
    /// http://www.asp.net/signalr/overview/performance-and-scaling/scaleout-with-sql-server
    /// </summary>
    public class BaseHub : Hub
    {
        internal static readonly ConcurrentDictionary<string, User> Users
            = new ConcurrentDictionary<string, User>(StringComparer.InvariantCultureIgnoreCase);

        private ISecurityHelper _securityHelper;

        /// <summary>
        /// Creates a new instance of BaseHub
        /// </summary>
        public BaseHub()
        {
            _securityHelper = new SecurityHelper();
        }

        /// <summary>
        /// <see cref="Microsoft.AspNet.SignalR.Hub"/>
        /// </summary>
        /// <returns><see cref="Microsoft.AspNet.SignalR.Hub"/></returns>
        public override Task OnConnected()
        {
            ParseUserFromAuthorizationHeader();

            string userId = _securityHelper.GetUser();
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userId, _ => new User
            {
                Id = userId,
                IsRRHH = IsRRHH(),
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
            }

            return base.OnConnected();
        }

        /// <summary>
        /// <see cref="Microsoft.AspNet.SignalR.Hub"/>
        /// </summary>
        /// <returns><see cref="Microsoft.AspNet.SignalR.Hub"/></returns>
        public override Task OnDisconnected()
        {
            ParseUserFromAuthorizationHeader();

            string userId = _securityHelper.GetUser();
            string connectionId = Context.ConnectionId;

            User user;
            Users.TryGetValue(userId, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                    if (!user.ConnectionIds.Any())
                    {
                        User removedUser;
                        Users.TryRemove(userId, out removedUser);
                    }
                }
            }

            return base.OnDisconnected();
        }

        /// <summary>
        /// Get user connection ids
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>an enumerable of connection ids</returns>
        protected static IEnumerable<string> GetUserConnections(string userId)
        {
            User user;
            Users.TryGetValue(userId, out user);

            if (user != null)
                return user.ConnectionIds;

            return new List<string>();
        }

        private bool IsRRHH()
        {
            return SecurityHelper.RequestIsNoAuthRoute() ? true : Thread.CurrentPrincipal.IsInRole("RRHH");
        }

        private void ParseUserFromAuthorizationHeader()
        {
            string token;
            if (Context.User != null && !Context.User.Identity.IsAuthenticated)
            {
                if (SecurityHelper.TryRetrieveToken(Context.Headers["Authorization"], out token))
                {
                    Thread.CurrentPrincipal = SecurityHelper.GetIdentityFromToken(token);
                }
                else
                {
                    //throw new System.UnauthorizedAccessException();
                    Trace.TraceInformation("Request has no Authorization header");
                }
            }
        }
    }
}