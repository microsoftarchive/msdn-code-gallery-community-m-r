using System.Collections.Generic;

namespace Index.Web
{
    public class PersonRepository
    {
        public IList<PersonData> Person{get;set;}

        public PersonRepository()
        {
            GeneratePersonList();
        }

        private void GeneratePersonList()
        {
            Person = new List<PersonData>() 
            {
                new PersonData() { PersonID = 1, FirstName = "Erandika",    LastName = "Sandaruwan", Age=25,     Address = "Delgoda"    },
                new PersonData() { PersonID = 2, FirstName = "Niluka",      LastName = "Dilani",     Age = 30,   Address = "Kandy"      },
                new PersonData() { PersonID = 3, FirstName = "Chathura",    LastName = "Achini",     Age = 27,   Address = "Colombo"    },
                new PersonData() { PersonID = 4, FirstName = "Florina",     LastName = "Breban",     Age = 25,   Address = "Romania"    },
            };
        }
    }
}