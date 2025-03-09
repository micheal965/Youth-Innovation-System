using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Core.IRepositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}
