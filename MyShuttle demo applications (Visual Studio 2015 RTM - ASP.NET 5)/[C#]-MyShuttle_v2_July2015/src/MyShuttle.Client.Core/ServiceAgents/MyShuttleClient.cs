using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.Messages;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using System;

namespace MyShuttle.Client.Core.ServiceAgents
{
    public class MyShuttleClient : IMyShuttleClient
    {
        private readonly IApplicationSettingServiceSingleton _applicationSettingService;
        private readonly IApplicationStorageService _applicationStorageService;
        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _token;

        IAnalyticsService _analyticsService;
        ICustomersService _customersService;
        IEmployeesService _employeesService;
        ICarriersService _carriersService;
        IVehiclesService _vehiclesService;
        IDriversService _driversService;
        IRidesService _ridesService;
        INotificationsService _notificationsService;
        
        public IAnalyticsService AnalyticsService
        {
            get
            {
                return _analyticsService ?? (_analyticsService = new AnalyticsService(_applicationSettingService.UrlPrefix, _applicationStorageService.SecurityToken));
            }
        }

        public ICustomersService CustomersService
        {
            get
            {
                return _customersService ?? (_customersService = new CustomersService(_applicationSettingService.UrlPrefix, _applicationStorageService.SecurityToken));
            }
        }

        public IEmployeesService EmployeesService
        {
            get 
            {
                return _employeesService ?? (_employeesService = new EmployeesService(_applicationSettingService.UrlPrefix, _applicationStorageService.SecurityToken));
            }
        }

        public ICarriersService CarriersService
        {
            get 
            {
                return _carriersService ?? (_carriersService = new CarriersService(_applicationSettingService.UrlPrefix, _applicationStorageService.SecurityToken));
            }
        }

        public IDriversService DriversService
        {
            get
            {
                return _driversService ?? (_driversService = new DriversService(_applicationSettingService.UrlPrefix, _applicationStorageService.SecurityToken));
            }
        }

        public IVehiclesService VehiclesService
        {
            get
            {
                return _vehiclesService ?? (_vehiclesService = new VehiclesService(_applicationSettingService.UrlPrefix, _applicationStorageService.SecurityToken));
            }
        }

        public IRidesService RidesService
        {
            get
            {
                return _ridesService ?? (_ridesService = new RidesService(_applicationSettingService.UrlPrefix, _applicationStorageService.SecurityToken));
            }
        }

        public INotificationsService NotificationsService
        {
            get
            {
                return _notificationsService ?? (_notificationsService = new NotificationsService(_applicationSettingService.UrlPrefix, _applicationStorageService.SecurityToken));
            }
        }

        public MyShuttleClient(
            IApplicationSettingServiceSingleton applicationSettingService, 
            IApplicationStorageService applicationStorageService,
            IMvxMessenger messenger)
        {
            if (applicationSettingService == null)
            {
                throw new ArgumentNullException("applicationSettingService");
            }

            if (applicationStorageService == null)
            {
                throw new ArgumentNullException("applicationStorageService");
            }

            if (messenger == null)
            {
                throw new ArgumentNullException("messenger");
            }

            _applicationSettingService = applicationSettingService;
            _applicationStorageService = applicationStorageService;
            _messenger = messenger;

            _token = _messenger.Subscribe<ReloadDataMessage>(_ => Refresh());
        }

        // NOTE: In order to notify "child" _*Service on UrlPrefix
        // change, make 2 things:
        // 1) Add IUpdatableUrlPrefix to its base interface, and
        // 2) Add 'if' below code for such service
        public void Refresh()
        {
            _applicationSettingService.Refresh();
            _applicationStorageService.Refresh();

            this.UpdateUrlPrefix(_analyticsService);
            this.UpdateUrlPrefix(_customersService);
            this.UpdateUrlPrefix(_employeesService);
            this.UpdateUrlPrefix(_carriersService);
            this.UpdateUrlPrefix(_vehiclesService);
            this.UpdateUrlPrefix(_driversService);
            this.UpdateUrlPrefix(_ridesService);
            this.UpdateUrlPrefix(_notificationsService);
        }

        private void UpdateUrlPrefix(IUpdatableUrl service)
        {
            if (service != null)
            {
                service.UrlPrefix = _applicationSettingService.UrlPrefix;
            }
        }
    }
}
