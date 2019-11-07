namespace MyCompany.Vacation.AzureEventBusPlugin.MessageHandlers
{
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using System;

    /// <summary>
    /// Represent the employee picture handler
    /// </summary>
    public class RemoveEmployeePictureMessageHandler
        : MessageHandler
    {
        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.DeleteEmployeePicture;
        }

        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var employeePictureRepository = new EmployeePictureRepository(new MyCompanyContext());
            employeePictureRepository.Delete(message.GetBody<int>());
        }
    }
}
