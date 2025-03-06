namespace Youth_Innovation_System.Core.IRepositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CompleteAsync();
    }
}
