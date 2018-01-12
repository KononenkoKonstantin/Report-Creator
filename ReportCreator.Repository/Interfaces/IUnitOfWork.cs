using System;

namespace ReportCreator.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
