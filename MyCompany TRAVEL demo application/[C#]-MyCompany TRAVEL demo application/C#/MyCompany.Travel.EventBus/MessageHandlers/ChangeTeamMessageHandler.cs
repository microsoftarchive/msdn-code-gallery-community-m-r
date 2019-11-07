namespace MyCompany.Travel.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
    using System;

    class ChangeTeamMessageHandler
   : MessageHandler
    {
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.UpdateTeam;
        }

        public override void Handle(BrokeredMessage message)
        {
            var teamRepository = new TeamRepository(new MyCompanyContext());
            var dto = message.GetBody<TeamDTO>();
            var team = Mapper.Map<Team>(dto);

            teamRepository.UpdateAsync(team).Wait();
        }
    }
}
