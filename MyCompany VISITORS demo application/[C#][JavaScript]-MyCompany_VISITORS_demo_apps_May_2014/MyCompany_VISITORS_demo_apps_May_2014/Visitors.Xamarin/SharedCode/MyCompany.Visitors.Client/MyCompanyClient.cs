namespace MyCompany.Visitors.Client
{
    using System;

    /// <summary>
    /// Client entry point class
    /// </summary>
    public class MyCompanyClient : IMyCompanyClient
    {
        IEmployeeService _employeeService = null;
        IVisitorService _visitorService = null;
        IVisitService _visitService = null;
        ISecurityService _securityService = null;
        IVisitorPictureService _visitorPictureService = null;

        private readonly string _urlPrefix = string.Empty;
        private string _securityToken = string.Empty;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public MyCompanyClient(string urlPrefix, string securityToken)
        {
            if (String.IsNullOrEmpty(urlPrefix))
                throw new ArgumentNullException("urlPrefix");

            _urlPrefix = urlPrefix;
            _securityToken = securityToken;
        }

        /// <summary>
        /// Property to access to the Employee Service client
        /// </summary>
        public IEmployeeService EmployeeService
        {
            get
            {
                if (_employeeService == null)
                    _employeeService = new EmployeeService(_urlPrefix, _securityToken);

                return _employeeService;
            }
        }

        /// <summary>
        /// Property to access to the Visit Service client
        /// </summary>
        public IVisitService VisitService
        {
            get
            {
                if (_visitService == null)
                    _visitService = new VisitService(_urlPrefix, _securityToken);

                return _visitService;
            }
        }

        /// <summary>
        /// Property to access to the Visitor Service client
        /// </summary>
        public IVisitorService VisitorService
        {
            get
            {
                if (_visitorService == null)
                    _visitorService = new VisitorService(_urlPrefix, _securityToken);

                return _visitorService;
            }
        }

        /// <summary>
        /// Property to access to the security service client.
        /// </summary>
        public ISecurityService SecurityService
        {
            get
            {
                if (_securityService == null)
                    _securityService = new SecurityService(_urlPrefix, _securityToken);

                return _securityService;
            }
        }

        /// <summary>
        /// Property to access to the visitor picture service client.
        /// </summary>
        public IVisitorPictureService VisitorPictureService
        {
            get
            {
                if (_visitorPictureService == null)
                    _visitorPictureService = new VisitorPictureService(_urlPrefix, _securityToken);

                return _visitorPictureService;
            }
        }

        /// <summary>
        /// Refreshes the security token.
        /// </summary>
        /// <param name="securityToken"></param>
        public void RefreshToken(string securityToken)
        {
            _securityToken = securityToken;

            if (_employeeService != null)
                _employeeService.RefreshToken(_securityToken);
            if (_visitorService != null)
                _visitorService.RefreshToken(_securityToken);
            if (_visitService != null)
                _visitService.RefreshToken(_securityToken);
            if (_securityService != null)
                _securityService.RefreshToken(_securityToken);
            if (_visitorPictureService != null)
                _visitorPictureService.RefreshToken(_securityToken);
            
        }


     
    }
}
