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
    /// Represent the change employee picture handler
    /// </summary>
    public class ChangeEmployeePictureMessageHandler
        : MessageHandler
    {
        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.UpdateEmployeePicture;
        }

        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var employeeRepository = new EmployeeRepository(new MyCompanyContext());
            var employeePictureRepository = new EmployeePictureRepository(new MyCompanyContext());
            
            var dto = message.GetBody<EmployeePictureDTO>();
            var employeePicture = Mapper.Map<EmployeePicture>(dto);

            if (employeeRepository.Get(employeePicture.EmployeeId) != null)
            {
                employeePictureRepository.Update(employeePicture);
            }
        }
    }
}
