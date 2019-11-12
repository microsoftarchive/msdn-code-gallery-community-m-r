namespace MyCompany.Vacation.AzureEventBusPlugin.MessageHandlers
{
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using System;


    /// <summary>
    /// Represent the remove team message handler
    /// </summary>
    public class RemoveTeamMessageHandler
         : MessageHandler
    {
        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.DeleteTeam;
        }

        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var teamRepository = new TeamRepository(new MyCompanyContext());
            teamRepository.Delete(message.GetBody<int>());
        }
    }
}
