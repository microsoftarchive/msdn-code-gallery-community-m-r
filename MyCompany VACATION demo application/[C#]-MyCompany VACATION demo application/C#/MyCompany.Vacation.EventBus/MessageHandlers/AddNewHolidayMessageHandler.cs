

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
    /// Represent the add new holiday handler
    /// </summary>
    public class AddNewHolidayMessageHandler
        :MessageHandler
    {
        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.AddHoliday;
        }

        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var calendarRepository = new CalendarRepository(new MyCompanyContext());
            var dto = message.GetBody<CalendarHolidayDTO>();
            var calendarHoliday = Mapper.Map<CalendarHolidays>(dto);

            calendarRepository.AddHoliday(calendarHoliday);            
        }
    }
}
