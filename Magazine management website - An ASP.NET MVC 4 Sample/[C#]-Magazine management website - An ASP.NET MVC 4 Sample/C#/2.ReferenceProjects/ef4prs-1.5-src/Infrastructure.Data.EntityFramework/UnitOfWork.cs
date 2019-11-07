using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Common;

namespace Infrastructure.Data.EntityFramework
{
    internal class UnitOfWork : IUnitOfWork
    {
        private DbTransaction _transaction;
        private ObjectContext _objectContext;

        public UnitOfWork(ObjectContext context)
        {
            _objectContext = context;
        }

        public bool IsInTransaction
        {
            get { return _transaction != null; }
        }

        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_transaction != null)
            {
                throw new ApplicationException("Cannot begin a new transaction while an existing transaction is still running. " +
                                                "Please commit or rollback the existing transaction before starting a new one.");
            }
            OpenConnection();
            _transaction = _objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public void RollBackTransaction()
        {
            if (_transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }

            if (IsInTransaction)
            {
                _transaction.Rollback();
                ReleaseCurrentTransaction();
            }
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }

            try
            {
                _objectContext.SaveChanges();
                _transaction.Commit();
                ReleaseCurrentTransaction();
            }
            catch
            {
                RollBackTransaction();
                throw;
            }            
        }

        public void SaveChanges()
        {
            if (IsInTransaction)
            {
                throw new ApplicationException("A transaction is running. Call CommitTransaction instead.");
            }
            _objectContext.SaveChanges();
        }

        public void SaveChanges(SaveOptions saveOptions)
        {
            if (IsInTransaction)
            {
                throw new ApplicationException("A transaction is running. Call BeginTransaction instead.");
            }
            _objectContext.SaveChanges(saveOptions);
        }

        /// <summary>
        /// Releases the current transaction
        /// </summary>
        private void ReleaseCurrentTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        private void OpenConnection()
        {
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (_disposed)
                return;

            ReleaseCurrentTransaction();

            _disposed = true;
        }
        private bool _disposed;
        #endregion
    }
}
