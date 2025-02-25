using Youth_Innovation_System.Core.Entities.Identity;
namespace Youth_Innovation_System.Core.IServices
{
    public interface IUserService
    {
        Task SaveLoginAttempt(string email);
        Task<IReadOnlyList<UserLoginHistory>> GetLoginHistory(string userId);
    }
}
