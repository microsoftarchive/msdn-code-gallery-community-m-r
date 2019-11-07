
namespace MyCompany.Expenses.WebApiRole.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public class NotifiersProvider : INotifiersProvider
    {
        private List<IPushNotificationService> _notificators = new List<IPushNotificationService>();

        /// <summary>
        /// Notificators repository constructor.
        /// </summary>
        public NotifiersProvider()
        {
            LoadNotificatorsFromAssembly();
        }

        /// <summary>
        /// Gets the notificators.
        /// </summary>
        public List<IPushNotificationService> GetNotificators()
        {
            return _notificators;
        }

        private void LoadNotificatorsFromAssembly()
        {
            var type = typeof(IPushNotificationService);
            var types = type.Assembly
             .GetTypes()
             .Where(p => type.IsAssignableFrom(p)
                         && p.IsClass);

            foreach (var notificatorType in types)
            {
                _notificators.Add((IPushNotificationService)Activator.CreateInstance(notificatorType));
            }
        }
    }
}