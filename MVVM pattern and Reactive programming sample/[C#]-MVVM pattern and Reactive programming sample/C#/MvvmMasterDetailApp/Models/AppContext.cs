using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmMasterDetailApp.Models
{
    public class AppContext : ObservableObject
    {
        private Messenger modelMessenger = new Messenger();

        public PeopleMaster Master { get; private set; }

        public PersonDetail Detail { get; private set; }

        public AppContext(IPeopleRepository repository)
        {
            this.Master = new PeopleMaster(this.modelMessenger, repository);
            this.Detail = new PersonDetail(this.modelMessenger, repository);
        }
    }
}
