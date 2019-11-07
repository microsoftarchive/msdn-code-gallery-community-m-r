namespace MyCompany.Travel.Client
{
    using System;

    /// <summary>
    /// Client entry point class
    /// </summary>
    public class MyCompanyClient : IMyCompanyClient
    {
        IEmployeeService _employeeService = null;
        ITravelRequestService _travelRequestService = null;
        ITravelAttachmentService _travelAttachmentService = null;
        ISecurityService _accountService = null;

        private readonly string _urlPrefix = string.Empty;
        private readonly  string _securityToken = string.Empty;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public MyCompanyClient(string urlPrefix, string securityToken)
        {
            if (String.IsNullOrEmpty(urlPrefix))
                throw new ArgumentNullException("urlPrefix");

            if (String.IsNullOrEmpty(securityToken))
                throw new ArgumentNullException("securityToken");

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
        /// Property to access to the Travel Request Service client
        /// </summary>
        public ITravelRequestService TravelRequestService
        {
            get
            {
                if (_travelRequestService == null)
                    _travelRequestService = new TravelRequestService(_urlPrefix, _securityToken);

                return _travelRequestService;
            }
        }

        /// <summary>
        /// Property to access to the Travel Attachment Service client
        /// </summary>
        public ITravelAttachmentService TravelAttachmentService
        {
            get
            {
                if (_travelAttachmentService == null)
                    _travelAttachmentService = new TravelAttachmentService(_urlPrefix, _securityToken);

                return _travelAttachmentService;
            }
        }

        /// <summary>
        /// Property to access to the Account Service client
        /// </summary>
        public ISecurityService AccountService
        {
            get
            {
                if (_accountService == null)
                    _accountService = new SecurityService(_urlPrefix, _securityToken);

                return _accountService;
            }
        }
    }
}
