using System;
using MVVMSample.PersonDataService;

namespace MVVMSample.ServiceAgent
{
    public interface IPersonService
    {
        void FindPerson(int personID,EventHandler<FindPersonCompletedEventArgs> callBack);
    }
}
