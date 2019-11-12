using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Index.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PersonDataService
    {
        [OperationContract]
        public PersonData FindPerson(int personID)
        {
            return new PersonRepository().Person.Where(x => x.PersonID == personID).SingleOrDefault();
        }
    }
}
