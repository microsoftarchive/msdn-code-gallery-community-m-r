using System;
using MVVMSample.PersonDataService;

namespace MVVMSample.ServiceAgent
{
    public class PersonService:IPersonService
    {
        private PersonDataServiceClient client = new PersonDataServiceClient();

        public void FindPerson(int personID, EventHandler<FindPersonCompletedEventArgs> callBack)
        {
            client.FindPersonCompleted += callBack;
            client.FindPersonAsync(personID);
        }
    }
}
