namespace MyCompany.Travel.AzureEventBusPlugin.MessageHandlers
{
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using System;


    class RemoveEmployeePictureMessageHandler
    : MessageHandler
    {
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.DeleteEmployeePicture;
        }

        public override void Handle(BrokeredMessage message)
        {
            var employeePictureRepository = new EmployeePictureRepository(new MyCompanyContext());
            var employeePictureId = message.GetBody<int>();

            employeePictureRepository.DeleteAsync(employeePictureId).Wait();
        }
    }
}
