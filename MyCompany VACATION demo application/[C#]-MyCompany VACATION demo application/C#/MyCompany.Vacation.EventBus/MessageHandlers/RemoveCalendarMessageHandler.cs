namespace MyCompany.Vacation.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Vacation.Model;
    using System;

    /// <summary>
    /// Represent the remove calendar handler
    /// </summary>
    public class RemoveCalendarMessageHandler
        : MessageHandler
    {
        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var calendarRepository = new CalendarRepository(new MyCompanyContext());
            var calendarId = message.GetBody<int>();

            calendarRepository.Delete(calendarId);
        }

        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.DeleteCalendar;
        }
    }
}
