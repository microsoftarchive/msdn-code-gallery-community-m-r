using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmMasterDetailApp.Models
{
    public class PersonChangeMessage : GenericMessage<Person>
    {
        public ChangeKind ChangeKind { get; private set; }

        public PersonChangeMessage(ChangeKind kind, Person content) : base(content)
        {
            this.ChangeKind = kind;
        }
    }

    public enum ChangeKind
    {
        Update,
        Delete
    }
}
