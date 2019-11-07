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
    /// Represent hte change employee message handler
    /// </summary>
    public class ChangeEmployeeMessageHandler
        : MessageHandler
    {
        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var employeeRepository = new EmployeeRepository(new MyCompanyContext());
            var dto = message.GetBody<EmployeeDTO>();
            var employee = Mapper.Map<Employee>(dto);

            employeeRepository.Update(employee);
        }

        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.UpdateEmployee;
        }
    }
}
