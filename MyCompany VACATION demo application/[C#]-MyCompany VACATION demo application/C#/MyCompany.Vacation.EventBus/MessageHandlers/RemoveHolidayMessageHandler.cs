
namespace MyCompany.Vacation.AzureEventBusPlugin.MessageHandlers
{
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using System;

    /// <summary>
    /// Represent the remove holiday handler
    /// </summary>
    public class RemoveHolidayMessageHandler
        :MessageHandler
    {
        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if(message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.DeleteHoliday;
        }

        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var calendarRepository = new CalendarRepository(new MyCompanyContext());
            calendarRepository.DeleteHoliday(message.GetBody<int>());
        }
    }
}
