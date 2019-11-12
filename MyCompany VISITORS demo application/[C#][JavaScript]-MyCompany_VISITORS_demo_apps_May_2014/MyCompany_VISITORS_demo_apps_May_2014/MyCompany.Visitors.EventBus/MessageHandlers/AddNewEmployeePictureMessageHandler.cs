namespace MyCompany.Visitors.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Visitors.Data;
    using MyCompany.Visitors.Data.Repositories;
    using MyCompany.Visitors.Model;
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
            var employeePictureRepository = new EmployeePictureRepository(new MyCompanyContext());
            var employeeRepository = new EmployeeRepository(new MyCompanyContext());
            var dto = message.GetBody<EmployeePictureDTO>();
            var employeePicture = Mapper.Map<EmployeePicture>(dto);
            var employee = employeeRepository.GetAsync(employeePicture.EmployeeId).Result;

            if(null == employeePicture)
            {
                Thread.Sleep(1000);
            }
            employeePictureRepository.AddAsync(employeePicture).Wait();
        }
    }
}
