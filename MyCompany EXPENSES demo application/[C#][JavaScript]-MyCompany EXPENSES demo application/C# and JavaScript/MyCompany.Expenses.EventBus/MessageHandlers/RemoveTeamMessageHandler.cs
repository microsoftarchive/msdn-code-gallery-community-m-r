
namespace MyCompany.Expenses.AzureEventBusPlugin.MessageHandlers
{
    using Microsoft.ServiceBus.Messaging;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


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
