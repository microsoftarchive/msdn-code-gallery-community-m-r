namespace MyCompany.Expenses.AzureEventBusPlugin
{
    using System.Threading.Tasks;
    using AutoMapper;
    using MyCompany.Common.CrossCutting;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.Model;
    using System.Collections.Generic;
    using MyCompany.Expenses.AzureEventBusPlugin.MessageHandlers;
    using MyCompany.Expenses.Data.Repositories;
    using MyCompany.Expenses.Data;

    /// <summary>
    /// <see cref="MyCompany.Common.EventBus.IHandler"/>
    /// </summary>
    public class AzureExpensesHandler : IHandler
    {
        private readonly ICollection<MessageHandler> _messageHandlers = new List<MessageHandler>();
        /// <summary>
        /// Constructor
        /// </summary>
        public AzureExpensesHandler()
        {
            InitializeMappings();

            _messageHandlers.Add(new AddNewEmployeeMessageHandler());
            _messageHandlers.Add(new AddNewEmployeePictureMessageHandler());
            _messageHandlers.Add(new AddNewTeamMessageHandler());
            _messageHandlers.Add(new ChangeEmployeeMessageHandler());
            _messageHandlers.Add(new ChangeEmployeePictureMessageHandler());
            _messageHandlers.Add(new ChangeTeamMessageHandler());
            _messageHandlers.Add(new RemoveEmployeeMessageHandler());
            _messageHandlers.Add(new RemoveEmployeePictureMessageHandler());
            _messageHandlers.Add(new RemoveTeamMessageHandler());
        }      

        /// <summary>
        /// This event will be called each time a message arrives.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(BrokeredMessageEventArgs message)
        {
            TraceManager.TraceInfo("Expense Handler Module.OnMessage");

            foreach (var item in _messageHandlers)
            {
                if (item.CanExecute(message.BrokeredMessage))
                {
                    item.Handle(message.BrokeredMessage);
                }
            }
        }

        private void InitializeMappings()
        {
            Mapper.CreateMap<EmployeeDTO, Employee>();
            Mapper.CreateMap<EmployeePictureDTO, EmployeePicture>();
            Mapper.CreateMap<TeamDTO, Team>();
        }
    }
}