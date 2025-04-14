using Microsoft.EntityFrameworkCore.Storage;
using Youth_Innovation_System.Core;
using Youth_Innovation_System.Core.Entities;
using Youth_Innovation_System.Core.IRepositories;

namespace Youth_Innovation_System.Repository.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Dictionary<string, object> Repositories;
        public UnitOfWork(ApplicationDbContext DbContext)
        {
            _dbContext = DbContext;
            Repositories = new Dictionary<string, object>();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            if (!Repositories.ContainsKey(type))
            {
                var RepositoryInstance = new GenericRepository<T>(_dbContext);
                Repositories.Add(type, RepositoryInstance);
            }
            return (IGenericRepository<T>)Repositories[type];
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();
        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await _dbContext.Database.BeginTransactionAsync();
    }

}
