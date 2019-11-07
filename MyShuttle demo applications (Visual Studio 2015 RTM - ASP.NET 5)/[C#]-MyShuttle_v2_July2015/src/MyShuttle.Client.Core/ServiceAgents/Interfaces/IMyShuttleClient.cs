namespace MyShuttle.Client.Core.ServiceAgents.Interfaces
{
    public interface IMyShuttleClient
    {
        IAnalyticsService AnalyticsService { get; }

        ICustomersService CustomersService { get; }

        IEmployeesService EmployeesService { get; }

        ICarriersService CarriersService { get; }

        IDriversService DriversService { get; }

        IVehiclesService VehiclesService { get; }

        IRidesService RidesService { get; }

        INotificationsService NotificationsService { get; }

        void Refresh();
    }
}
