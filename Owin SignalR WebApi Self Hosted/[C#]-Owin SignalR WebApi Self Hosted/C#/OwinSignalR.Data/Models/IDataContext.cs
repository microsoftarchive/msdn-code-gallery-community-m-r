using System;

namespace OwinSignalR.Data.Models
{
    public interface IDataContext
    {
        IOwinSignalrDbContext OwinSignalrDbContext { get; }
        void Initialize(IOwinSignalrDbContext owinSignalrDbContext);
    }

    public class DataContext 
        : IDataContext
    {
        #region Private Members
        private IOwinSignalrDbContext _owinSignalrDbContext;
        #endregion

        public IOwinSignalrDbContext OwinSignalrDbContext
        {
            get
            {
                return _owinSignalrDbContext;
            }            
        }

        public void Initialize(
            IOwinSignalrDbContext owinSignalrDbContext)
        {
            _owinSignalrDbContext = owinSignalrDbContext;
        }
    }
}
