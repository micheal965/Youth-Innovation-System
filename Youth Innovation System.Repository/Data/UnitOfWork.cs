using Youth_Innovation_System.Core.IRepositories;

namespace Youth_Innovation_System.Repository.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext DbContext)
        {
            _dbContext = DbContext;
        }



        public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();

    }
}
