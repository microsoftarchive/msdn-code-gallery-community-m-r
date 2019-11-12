using OwinSignalR.Data.Models;

using StructureMap;

namespace OwinSignalR.Data.DataAccessors
{
    public class DataAccessorBase
    {
        protected IDataContext DataContext 
        {
            get 
            {
                return ObjectFactory.GetInstance<IDataContext>();
            }
        }

        protected IOwinSignalrDbContext OwinSignalrDbContext 
        {
            get 
            {
                return DataContext.OwinSignalrDbContext;
            }
        }
    }
}
