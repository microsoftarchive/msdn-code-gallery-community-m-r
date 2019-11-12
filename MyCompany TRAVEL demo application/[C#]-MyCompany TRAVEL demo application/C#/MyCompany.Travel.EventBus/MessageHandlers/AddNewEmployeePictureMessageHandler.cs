namespace MyCompany.Travel.AzureEventBusPlugin.MessageHandlers
{

    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
    using System;
    using System.Threading;

    class AddNewEmployeePictureMessageHandler
        : MessageHandler
    {
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.AddEmployeePicture;
        }

        public override void Handle(BrokeredMessage message)
        {
            var employeeRepository = new EmployeeRepository(new MyCompanyContext());
            var employeePictureRepository = new EmployeePictureRepository(new MyCompanyContext());
            var dto = message.GetBody<EmployeePictureDTO>();
            var employeePicture = Mapper.Map<EmployeePicture>(dto);

            var employee = employeeRepository.GetAsync(employeePicture.EmployeeId).Result;

            if (employee == null)
            {
                Thread.Sleep(1000);
            }
            employeePictureRepository.AddAsync(employeePicture).Wait();
        }
    }
}
