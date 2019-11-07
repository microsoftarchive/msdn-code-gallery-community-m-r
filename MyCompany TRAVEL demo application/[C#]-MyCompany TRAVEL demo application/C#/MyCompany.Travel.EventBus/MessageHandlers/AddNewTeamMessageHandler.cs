
namespace MyCompany.Travel.AzureEventBusPlugin.MessageHandlers
{
    using AutoMapper;
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.Data;
    using MyCompany.Travel.Data.Repositories;
    using MyCompany.Travel.Model;
    using System;

    class AddNewTeamMessageHandler
       : MessageHandler
    {
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.AddTeam;
        }

        public override void Handle(BrokeredMessage message)
        {
            var teamRepository = new TeamRepository(new MyCompanyContext());
            var dto = message.GetBody<TeamDTO>();
            var team = Mapper.Map<Team>(dto);

            teamRepository.AddAsync(team).Wait();
        }
    }
}
