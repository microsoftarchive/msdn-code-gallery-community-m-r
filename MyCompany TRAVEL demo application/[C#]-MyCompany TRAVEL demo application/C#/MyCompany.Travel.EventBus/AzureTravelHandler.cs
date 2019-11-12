namespace MyCompany.Travel.AzureEventBusPlugin
{
    using MyCompany.Common.CrossCutting;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.AzureEventBusPlugin.MessageHandlers;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="MyCompany.Common.EventBus.IHandler"/>
    /// </summary>
    public class AzureTravelHandler : IHandler
    {
        private readonly ICollection<MessageHandler> _messageHandlers = new List<MessageHandler>();

        /// <summary>
        /// Constructor
        /// </summary>
        public AzureTravelHandler()
        {
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
            TraceManager.TraceInfo("Travel Handler Module.OnMessage");

            foreach (var item in _messageHandlers)
            {
                if (item.CanExecute(message.BrokeredMessage))
                {
                    item.Handle(message.BrokeredMessage);
                }
            }
            
        }
    }
}
