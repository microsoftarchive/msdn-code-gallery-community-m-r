using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmMasterDetailApp.Models
{
    public interface IPeopleRepository
    {
        IEnumerable<Person> Load();

        Person Find(int id);

        void Insert(Person p);

        void Update(Person p);

        void Delete(int id);
    }
}
