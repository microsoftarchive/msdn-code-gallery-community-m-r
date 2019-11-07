using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using System;
namespace MyShuttle.Client.Core.ServiceAgents
{
    public interface INotificationsService : IUpdatableUrl
    {
        System.Threading.Tasks.Task RequestVehicleAsync(string employeeId, int driverId, double latitude, double longitude);
    }
}
