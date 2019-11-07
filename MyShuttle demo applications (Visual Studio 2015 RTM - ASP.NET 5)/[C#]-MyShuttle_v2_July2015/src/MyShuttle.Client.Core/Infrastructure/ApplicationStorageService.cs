using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using System;

namespace MyShuttle.Client.Core.Infrastructure
{
    public class ApplicationStorageService : IApplicationStorageService
    {
        // Services
        private readonly IApplicationDataRepository _applicationDataRepository;
        
        // Fields
        private const string SecurityTokenKey = "SecurityTokenKey";
        private readonly string _defaultSecurityTokenValue = String.Empty;
        private string _securityToken;
        
        public string SecurityToken
        {
            get
            {
                if (string.IsNullOrEmpty(_securityToken))
                {
                    _securityToken = _applicationDataRepository.GetStringFromApplicationData(SecurityTokenKey, _defaultSecurityTokenValue);
                }
                return _securityToken;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
       
        public ApplicationStorageService(IApplicationDataRepository applicationDataRepository)
        {
            if (applicationDataRepository == null)
            {
                throw new ArgumentNullException("applicationDataRepository");
            }

            _applicationDataRepository = applicationDataRepository;
        }
        
        public void Refresh()
        {
            _securityToken = null;
        }
    }
}
