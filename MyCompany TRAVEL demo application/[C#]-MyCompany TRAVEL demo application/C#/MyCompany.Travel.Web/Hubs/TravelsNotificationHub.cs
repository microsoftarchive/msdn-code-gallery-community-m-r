namespace MyCompany.Travel.Web.Hubs
{
    using Microsoft.AspNet.SignalR;
    using MyCompany.Travel.Model;
    using MyCompany.Travel.Web.Security;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Web.Configuration;
    using System.Threading;
    using System.Diagnostics;

    internal class User
    {
        public string Id { get; set; }
        public bool IsRRHH { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }

    /// <summary>
    /// Hub for SignalR notifications
    /// To scaleout with SQL Server see:
    /// http://www.asp.net/signalr/overview/performance-and-scaling/scaleout-with-sql-server
    /// </summary>
    /// [Authorize] // Authorize attribute is disabled in order to support the NoAuth scenario
    public class TravelsNotificationHub : Hub 
    {
        internal static readonly ConcurrentDictionary<string, User> Users
            = new ConcurrentDictionary<string, User>(StringComparer.InvariantCultureIgnoreCase);

        private ISecurityHelper _securityHelper;
        private IADGraphApi _ADGrapAPI;

        /// <summary>
        /// Constructor
        /// </summary>
        public TravelsNotificationHub()
        {
            _securityHelper = new SecurityHelper();
            _ADGrapAPI = new ADGraphApi();
        }

        /// <summary>
        /// <see cref="Microsoft.AspNet.SignalR.Hub"/>
        /// </summary>
        /// <returns><see cref="Microsoft.AspNet.SignalR.Hub"/></returns>
        public override Task OnConnected()
        {
            ParseUserFromAuthorizationHeader();

            bool isNoAuth;
            bool.TryParse(Context.QueryString["isNoAuth"], out isNoAuth);

            string userId = _securityHelper.GetUser(isNoAuth);
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userId, _ => new User
            {
                Id = userId,
                IsRRHH = isNoAuth || IsRRHH(userId),
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
        /// Send a notification when a new TravelRequest is created
        /// </summary>
        /// <param name="travelRequest">the TravelRequest entity</param>
        /// <param name="to">destinatary of the notification</param>
        public static void NotifyNew(TravelRequest travelRequest, string to)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<TravelsNotificationHub>();

            IEnumerable<string> receivers = GetUserConnections(to);
            foreach (var cid in receivers)
            {
                context.Clients.Client(cid).notifyNew(travelRequest);
            }
        }

        /// <summary>
        /// Send a notification when a TravelRequest has been approved
        /// </summary>
        /// <param name="travelRequest">the TravelRequest entity</param>
        public static void NotifyApproved(TravelRequest travelRequest)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<TravelsNotificationHub>();

            IEnumerable<string> receivers = GetRRHHUsersConnections();
            foreach (var cid in receivers)
            {
                context.Clients.Client(cid).notifyApproved(travelRequest);
            }
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

        /// <summary>
        /// Get RRHH Users Connections
        /// </summary>
        /// <returns>an enumerable of connection ids</returns>
        protected static IEnumerable<string> GetRRHHUsersConnections()
        {
            var rrhhUsers = Users.Where(u => u.Value.IsRRHH);

            if (rrhhUsers.Any())
            {
                var list = rrhhUsers.Select(r => r.Value.ConnectionIds);

                HashSet<string> connectionIds = new HashSet<string>();
                foreach (var item in list)
                {
                    foreach (var connectionId in item)
                    {
                        connectionIds.Add(connectionId);
                    }
                }
                return connectionIds;
            }
            return new HashSet<string>();
        }

        private bool IsRRHH(string name)
        {
            if (SecurityHelper.RequestIsNoAuthRoute()
                 || IsDemoUser(name))
                return true;

            string groupName = WebConfigurationManager.AppSettings["HHRRGroupName"];
            return _ADGrapAPI.IsInGroup(name, groupName);
        }

        bool IsDemoUser(string userPrincipalName)
        {
            // Only needed for demos
            return userPrincipalName.Equals(WebConfigurationManager.AppSettings["rrhhIdentity"], StringComparison.InvariantCultureIgnoreCase);
        }

        private void ParseUserFromAuthorizationHeader()
        {
            string token;
            if (Context.User != null && Context.User.Identity != null && !Context.User.Identity.IsAuthenticated)
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