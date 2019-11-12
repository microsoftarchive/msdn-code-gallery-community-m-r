using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmMasterDetailApp.Models
{
    public class PersonDetail : ObservableObject
    {
        private readonly Messenger messenger;

        private readonly IPeopleRepository repository;

        private Person editTarget;

        public Person EditTarget
        {
            get { return this.editTarget; }
            private set { this.Set(ref this.editTarget, value); }
        }

        public PersonDetail(Messenger messenger, IPeopleRepository repository)
        {
            this.messenger = messenger;
            this.repository = repository;
        }

        public void SetEditTarget(int id)
        {
            this.EditTarget = this.repository.Find(id);
        }

        public void Update()
        {
            this.repository.Update(this.EditTarget);
            this.messenger.Send(new PersonChangeMessage(ChangeKind.Update, this.EditTarget));
        }

        public void Delete()
        {
            this.repository.Delete(this.EditTarget.Id);
            this.messenger.Send(new PersonChangeMessage(ChangeKind.Delete, this.EditTarget));
        }
    }
}
