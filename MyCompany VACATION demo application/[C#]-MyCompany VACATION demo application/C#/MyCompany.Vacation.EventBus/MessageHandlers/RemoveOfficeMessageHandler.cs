namespace MyCompany.Vacation.AzureEventBusPlugin.MessageHandlers
{
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using System;

    /// <summary>
    /// Represent the remove office handler
    /// </summary>
    public class RemoveOfficeMessageHandler
        : MessageHandler
    {
        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.DeleteOffice;
        }

        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var officeRepository = new OfficeRepository(new MyCompanyContext());
            officeRepository.Delete(message.GetBody<int>());
        }
    }
}
