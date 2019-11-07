using System;

namespace TableToViewMigrationDemo
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
