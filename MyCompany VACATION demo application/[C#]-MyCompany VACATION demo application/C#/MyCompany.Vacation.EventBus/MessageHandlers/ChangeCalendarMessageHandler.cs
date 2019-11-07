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
    /// Change calendar message handler
    /// </summary>
    public class ChangeCalendarMessageHandler
        :MessageHandler
    {
        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var calendarRepository = new CalendarRepository(new MyCompanyContext());
            var dto = message.GetBody<CalendarDTO>();
            var calendar = Mapper.Map<Calendar>(dto);
            calendarRepository.Update(calendar);
        }


        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.UpdateCalendar;
        }
    }
}
