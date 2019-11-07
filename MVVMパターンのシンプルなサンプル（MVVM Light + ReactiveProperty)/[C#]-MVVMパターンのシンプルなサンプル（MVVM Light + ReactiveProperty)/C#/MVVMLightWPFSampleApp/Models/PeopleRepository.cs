using System.Collections.Generic;
using System.Linq;

namespace MVVMLightWPFSampleApp.Models
{
    public class PeopleRepository
    {
        private static readonly List<Person> DataStore = new List<Person>
        {
            new Person { ID = 0, Name = "tanaka1", Age = 30 },
            new Person { ID = 1, Name = "tanaka2", Age = 31 },
            new Person { ID = 2, Name = "tanaka3", Age = 32 },
            new Person { ID = 3, Name = "tanaka4", Age = 50 },
        };

        public Person Find(long id)
        {
            var target = DataStore.SingleOrDefault(x => x.ID == id);
            if (target == null)
            {
                return null;
            }

            return new Person
            {
                ID = target.ID,
                Name = target.Name,
                Age = target.Age,
            };
        }

        public IEnumerable<Person> Load()
        {
            return DataStore
                .OrderBy(x => x.ID)
                .Select(x => new Person
                {
                    ID = x.ID,
                    Name = x.Name,
                    Age = x.Age,
                });
        }

        public void Update(Person p)
        {
            var target = DataStore.Single(x => x.ID == p.ID);
            target.Name = p.Name;
            target.Age = p.Age;
        }

        public void Delete(long id)
        {
            DataStore.Remove(DataStore.Single(x => x.ID == id));
        }

        public void Insert(Person p)
        {
            var target = new Person
            {
                ID = GenerateID(),
                Name = p.Name,
                Age = p.Age
            };

            DataStore.Add(target);
            p.ID = target.ID;
        }

        private static long GenerateID()
        {
            if (!DataStore.Any())
            {
                return 0;
            }

            return DataStore.Max(x => x.ID) + 1;
        }
    }
}
