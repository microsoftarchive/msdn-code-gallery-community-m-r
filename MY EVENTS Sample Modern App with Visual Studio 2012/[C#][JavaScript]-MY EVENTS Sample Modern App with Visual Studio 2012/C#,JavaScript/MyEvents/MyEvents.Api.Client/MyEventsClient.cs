using System;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Client entry point class
    /// </summary>
    public class MyEventsClient : IMyEventsClient
    {
        private IEventDefinitionService _eventDefinitionService = null;
        private ISessionService _sessionService = null;
        private IRegisteredUserService _registeredUserService = null;
        private IReportService _reportService = null;
        private IAuthenticationService _authenticationService = null;
        private IMaterialService _materialService = null;
        private ICommentService _commentService = null;
        private IRoomPointService _roomPointService = null;

        private string _urlPrefix = string.Empty;
        private string _authenticationToken = string.Empty;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        public MyEventsClient(string urlPrefix)
        {
            if (String.IsNullOrEmpty(urlPrefix))
                throw new ArgumentNullException("urlPrefix");

            _urlPrefix = urlPrefix;
        }

        /// <summary>
        /// Set Access Token
        /// </summary>
        /// <param name="token"></param>
        public void SetAccessToken(string token)
        {
            _authenticationToken = token;
        }

        /// <summary>
        /// Authentication Token
        /// </summary>
        public string AuthenticationToken
        {
            get
            {
                return _authenticationToken;
            }
            set
            {
                _authenticationToken = value;
            }
        }

        /// <summary>
        /// Property to access to the Event Definition Service client
        /// </summary>
        public IEventDefinitionService EventDefinitionService 
        {
            get
            {
                if (_eventDefinitionService == null)
                    _eventDefinitionService = new EventDefinitionService(_urlPrefix, _authenticationToken);

                return _eventDefinitionService;
            }
        }

        /// <summary>
        /// Property to access to the Session Service client
        /// </summary>
        public ISessionService SessionService
        {
            get
            {
                if (_sessionService == null)
                    _sessionService = new SessionService(_urlPrefix, _authenticationToken);

                return _sessionService;
            }
        }

        /// <summary>
        /// Property to access to the Registered User Service client
        /// </summary>
        public IRegisteredUserService RegisteredUserService
        {
            get
            {
                if (_registeredUserService == null)
                    _registeredUserService = new RegisteredUserService(_urlPrefix, _authenticationToken);

                return _registeredUserService;
            }
        }

        /// <summary>
        /// Property to access to the Material Service client
        /// </summary>
        public IMaterialService MaterialService
        {
            get
            {
                if (_materialService == null)
                    _materialService = new MaterialService(_urlPrefix, _authenticationToken);

                return _materialService;
            }
        }

        /// <summary>
        /// Property to access to the Comment Service client
        /// </summary>
        public ICommentService CommentService
        {
            get
            {
                if (_commentService == null)
                    _commentService = new CommentService(_urlPrefix, _authenticationToken);

                return _commentService;
            }
        }

        /// <summary>
        /// Property to access to the Report Service client
        /// </summary>
        public IReportService ReportService
        {
            get
            {
                if (_reportService == null)
                    _reportService = new ReportService(_urlPrefix, _authenticationToken);

                return _reportService;
            }
        }


        /// <summary>
        /// Property to access to the Authentication Service client
        /// </summary>
        public IAuthenticationService AuthenticationService
        {
            get
            {
                if (_authenticationService == null)
                    _authenticationService = new AuthenticationService(_urlPrefix);

                return _authenticationService;
            }
        }

        /// <summary>
        /// Property to access to the RoomPoint Service client
        /// </summary>
        public IRoomPointService RoomPointService
        {
            get
            {
                if (_roomPointService == null)
                    _roomPointService = new RoomPointService(_urlPrefix, _authenticationToken);

                return _roomPointService;
            }
        }

    }
}
