using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmMasterDetailApp.Models
{
    public class OnMemoryPeopleRepository : IPeopleRepository
    {
        private static readonly List<Person> DataStore = new List<Person>();

        public IEnumerable<Person> Load()
        {
            return DataStore
                .Select(x => new Person 
                { 
                    Id = x.Id, 
                    Name = x.Name, 
                    Age = x.Age 
                });
        }

        public Person Find(int id)
        {
            var target = DataStore.First(x => x.Id == id);
            return new Person
            {
                Id = target.Id,
                Name = target.Name,
                Age = target.Age
            };
        }

        public void Insert(Person p)
        {
            var newPerson = new Person
            {
                Id = GenerateId(),
                Name = p.Name,
                Age = p.Age
            };
            DataStore.Add(newPerson);
            p.Id = newPerson.Id;
        }

        public void Update(Person p)
        {
            var target = DataStore.First(x => x.Id == p.Id);
            target.Name = p.Name;
            target.Age = p.Age;
        }

        public void Delete(int id)
        {
            DataStore.Remove(DataStore.First(x => x.Id == id));
        }

        private static int GenerateId()
        {
            if (!DataStore.Any())
            {
                return 0;
            }

            return DataStore.Max(x => x.Id) + 1;
        }
    }
}
