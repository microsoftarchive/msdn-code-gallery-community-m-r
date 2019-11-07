using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmMasterDetailApp.Models
{
    public class PeopleMaster : ObservableObject
    {
        private readonly Messenger messenger;

        private readonly IPeopleRepository repository;

        public ObservableCollection<Person> People { get; private set; }

        public PeopleMaster(Messenger messenger, IPeopleRepository repository)
        {
            this.messenger = messenger;
            this.repository = repository;
            this.People = new ObservableCollection<Person>();

            this.messenger.Register<PersonChangeMessage>(this, this.PersonChangedReceived);
        }

        public void Load()
        {
            this.People.Clear();
            var results = this.repository
                .Load()
                .ToArray();

            foreach (var p in results)
            {
                this.People.Add(p);
            }
        }

        public void AddPerson()
        {
            var p = new Person { Name = "New person" };
            this.repository.Insert(p);
            this.People.Add(p);
        }

        private void PersonChangedReceived(PersonChangeMessage message)
        {
            switch (message.ChangeKind)
            {
                case ChangeKind.Delete:
                    this.People.Remove(this.People.First(x => x.Id == message.Content.Id));
                    break;
                case ChangeKind.Update:
                    var p = this.People.First(x => x.Id == message.Content.Id);
                    p.Name = message.Content.Name;
                    p.Age = message.Content.Age;
                    break;
            }
        }


    }
}
