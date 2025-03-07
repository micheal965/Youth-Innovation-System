using Microsoft.AspNetCore.Identity;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Identity;
namespace Youth_Innovation_System.Core.IServices
{
    public interface IUserService
    {
        Task SaveLoginAttemptAsync(string email);
        Task<IReadOnlyList<UserLoginHistory>> GetLoginHistoryAsync(string userId);
        Task<UserToReturnDto?> GetUserByIdAsync(string userId);
        Task<IEnumerable<UserToReturnDto?>> GetAllUsersAsync();
        Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserDto userDto);
        Task<ApiResponse> DeleteUserAsync(string userId);
        //UpdateUserPicture

    }
}
