namespace MyShuttle.API.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Model;

    public interface IClient
    {
        void UpdateEmployeePosition(string employeeId, double latitude, double longitude);

        void UpdateEvents(EventMessage message);
    }

    public class MyShuttleHub : Hub<IClient>
    {
        public void UpdateEmployeePosition(string employeeId, double latitude, double longitude)
        {
            Clients.All.UpdateEmployeePosition(employeeId, latitude, longitude);
        }

        public void UpdateEvents(EventMessage message)
        {
            Clients.All.UpdateEvents(message);
        }
    }
}
