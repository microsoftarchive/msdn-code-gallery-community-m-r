namespace MyCompany.Travel.AzureEventBusPlugin.MessageHandlers
{
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using System;

    class RemoveTeamMessageHandler
     : MessageHandler
    {
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.DeleteTeam;
        }

        public override void Handle(BrokeredMessage message)
        {
            var teamRepository = new TeamRepository(new MyCompanyContext());
            var teamId = message.GetBody<int>();

            teamRepository.DeleteAsync(teamId).Wait();
        }
    }
}
