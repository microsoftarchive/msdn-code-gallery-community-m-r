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
    /// Represent the add new calendar message handler
    /// </summary>
    public sealed class AddNewCalendarMessageHandler
        :MessageHandler
    {
        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var calendarRepository = new CalendarRepository(new MyCompanyContext());
            var dto = message.GetBody<CalendarDTO>();
            var calendar = Mapper.Map<Calendar>(dto);

            calendarRepository.Add(calendar);
        }

        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.AddCalendar;
        }
    }
}
