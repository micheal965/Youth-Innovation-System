using Youth_Innovation_System.Shared.ApiResponses;

namespace Youth_Innovation_System.Core.Specifications
{
    public interface IGenericReactionService<T>
    {
        Task AddReactionAsync(int id, string userId, string reactionType);
        Task<ApiResponse> RemoveReactionAsync(int id, string userId);
        Task<IEnumerable<T>> GetReactionsAsync(int id);
        Task<int> GetReactionsCountAsync(int id);
    }
}
