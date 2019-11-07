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
    /// Represent the add new office handler
    /// </summary>
    public class AddNewOfficeMessageHandler
        :MessageHandler
    {
        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.AddOffice;
        }

        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var officeRepository = new OfficeRepository(new MyCompanyContext());
            var dto = message.GetBody<OfficeDTO>();

            var office = Mapper.Map<Office>(dto);
            officeRepository.Add(office);
        }
    }
}
