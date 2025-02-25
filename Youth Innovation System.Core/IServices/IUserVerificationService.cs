using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Shared.ApiResponses;

namespace Youth_Innovation_System.Core.IServices
{
    public interface IUserVerificationService
    {
        Task RequestVerificationEmailAsync(string Email);
        Task<ApiResponse> RequestPasswordResetAsync(string email);
    }
}
