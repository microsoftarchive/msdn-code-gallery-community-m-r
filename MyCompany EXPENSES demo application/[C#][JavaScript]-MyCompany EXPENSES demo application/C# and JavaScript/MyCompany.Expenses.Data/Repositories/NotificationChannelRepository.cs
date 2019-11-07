namespace MyCompany.Expenses.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Expenses.Model;
    using System.Data.Entity;
    using System.Threading.Tasks;

    /// <summary>
    /// Notification channel repository
    /// </summary>
    public class NotificationChannelRepository : INotificationChannelRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Creates a new instance of TeamRepository class
        /// </summary>
        /// <param name="context"></param>
        public NotificationChannelRepository(MyCompanyContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/>
        /// </summary>
        /// <param name="userIdentity"><see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/></param>
        /// <returns>
        /// <see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/>
        /// </returns>
        public async Task<IEnumerable<NotificationChannel>> GetUserChannelsAsync(string userIdentity)
        {
            return await _context.NotificationChannels
                    .Where(nc => nc.Employee.Email == userIdentity)
                    .ToListAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/>
        /// </summary>
        /// <param name="userIdentity"><see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/></param>
        /// <param name="channelUri"><see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/></param>
        /// <param name="notificationType"><see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/></param>
        public async Task AddUserChannelAsync(string userIdentity, string channelUri, NotificationType notificationType)
        {
            var channel =
                _context.NotificationChannels.FirstOrDefault(
                    nc => nc.Employee.Email == userIdentity && nc.NotificationType == notificationType);

            var employee = _context.Employees.FirstOrDefault(e => e.Email == userIdentity);
            if (employee == null)
                return;

            if (channel == null)
            {
                _context.NotificationChannels.Add(new NotificationChannel
                    {
                        ChannelUri = channelUri,
                        EmployeeId = employee.EmployeeId,
                        NotificationType = notificationType
                    });
            }
            else
            {
                channel.ChannelUri = channelUri;

                _context.Entry<NotificationChannel>(channel)
                  .CurrentValues
                  .SetValues(channel);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/></returns>
        public async Task<IEnumerable<NotificationChannel>> GetManagersChannelsAsync()
        {
            return
                await _context.NotificationChannels.Where(
                    nc => nc.Employee.ManagedTeams.Any()).ToListAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/>
        /// </summary>
        /// <param name="userIdentity"><see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/></param>
        /// <param name="channelUri"><see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/></param>
        /// <param name="notificationType"><see cref="MyCompany.Expenses.Data.Repositories.INotificationChannelRepository"/></param>
        public async Task RemoveUserChannelAsync(string userIdentity, string channelUri, NotificationType notificationType)
        {
            var channel =
                _context.NotificationChannels.FirstOrDefault(
                    nc => nc.Employee.Email == userIdentity && nc.NotificationType == notificationType && nc.ChannelUri == channelUri);

            _context.NotificationChannels.Remove(channel);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose all resource
        /// </summary>
        /// <param name="disposing">Dispose managed resources check</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
