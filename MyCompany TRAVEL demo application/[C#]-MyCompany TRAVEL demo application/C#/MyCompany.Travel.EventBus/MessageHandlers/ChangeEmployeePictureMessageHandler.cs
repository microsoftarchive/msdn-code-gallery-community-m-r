namespace MyCompany.Travel.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
    using System;

    class ChangeEmployeePictureMessageHandler
        : MessageHandler
    {
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.UpdateEmployeePicture;
        }

        public override void Handle(BrokeredMessage message)
        {
            var employeePictureRepository = new EmployeePictureRepository(new MyCompanyContext());
            var dto = message.GetBody<EmployeePictureDTO>();
            var employeePicture = Mapper.Map<EmployeePicture>(dto);

            employeePictureRepository.UpdateAsync(employeePicture).Wait();
        }
    }
}
