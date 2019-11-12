namespace MyCompany.Vacation.AzureEventBusPlugin
{
    using AutoMapper;
    using MyCompany.Common.CrossCutting;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.AzureEventBusPlugin.MessageHandlers;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Vacation.Model;
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="MyCompany.Common.EventBus.IHandler"/>
    /// </summary>
    public class AzureVacationHandler : IHandler
    {
        private readonly ICollection<MessageHandler> _messageHandlers = new List<MessageHandler>();

        /// <summary>
        /// Create a new instance 
        /// </summary>
        public AzureVacationHandler()
        {
            InitializeMappings();

            _messageHandlers.Add(new AddNewCalendarMessageHandler());
            _messageHandlers.Add(new RemoveCalendarMessageHandler());
            _messageHandlers.Add(new ChangeCalendarMessageHandler());
            _messageHandlers.Add(new AddNewEmployeeMessageHandler());
            _messageHandlers.Add(new RemoveEmployeeMessageHandler());
            _messageHandlers.Add(new ChangeEmployeeMessageHandler());
            _messageHandlers.Add(new AddNewTeamMessageHandler());
            _messageHandlers.Add(new RemoveTeamMessageHandler());
            _messageHandlers.Add(new ChangeTeamMessageHandler());
            _messageHandlers.Add(new AddNewOfficeMessageHandler());
            _messageHandlers.Add(new RemoveOfficeMessageHandler());
            _messageHandlers.Add(new ChangeOfficeMessageHandler());
            _messageHandlers.Add(new AddNewHolidayMessageHandler());
            _messageHandlers.Add(new RemoveHolidayMessageHandler());
            _messageHandlers.Add(new AddNewEmployeePictureMessageHandler());
            _messageHandlers.Add(new ChangeEmployeePictureMessageHandler());
            _messageHandlers.Add(new RemoveEmployeePictureMessageHandler());
        }

        /// <summary>
        /// This event will be called each time a message arrives.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(BrokeredMessageEventArgs message)
        {
            TraceManager.TraceInfo("Vacation Handler Module.OnMessage");

            foreach (var item in _messageHandlers)
            {
                if (item.CanExecute(message.BrokeredMessage))
                {
                    item.Handle(message.BrokeredMessage);
                }
            }
        }

        void InitializeMappings()
        {
            Mapper.CreateMap<TeamDTO, Team>();
            Mapper.CreateMap<CalendarDTO, Calendar>();
            Mapper.CreateMap<OfficeDTO, Office>();
            Mapper.CreateMap<EmployeeDTO, Employee>();
            Mapper.CreateMap<EmployeePictureDTO, EmployeePicture>();
            Mapper.CreateMap<CalendarHolidayDTO, CalendarHolidays>();
        }
    }
}
