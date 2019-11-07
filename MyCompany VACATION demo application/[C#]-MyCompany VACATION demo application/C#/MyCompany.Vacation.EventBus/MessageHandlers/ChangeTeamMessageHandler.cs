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
    /// Represent the change team handler
    /// </summary>
    public class ChangeTeamMessageHandler
         : MessageHandler
    {
        ///<inheritdoc/>
        public override bool CanExecute(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return message.ContentType == StaffActions.UpdateTeam;
        }

        ///<inheritdoc/>
        public override void Handle(BrokeredMessage message)
        {
            var teamRepository = new TeamRepository(new MyCompanyContext());
            var dto = message.GetBody<TeamDTO>();
            var team = Mapper.Map<Team>(dto);

            teamRepository.Update(team);
        }
    }
}
