using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Offer;

namespace Youth_Innovation_System.Core.IServices.OfferServices
{
    public interface IOfferService
    {
        Task<ApiResponse> MakeOfferAsync(string investorId, CreateOfferDto offerDto);
        Task<IEnumerable<OfferToReturnDto>> GetOffersForPostAsync(int postId, string userId);
        Task<IEnumerable<OfferToReturnDto>> GetPendingOffersForPostAsync(int postId, string userId);
        Task<OfferToReturnDto> GetOfferForPostAsync(int offerId, string userId);

        Task<ApiResponse> AcceptOfferAsync(string postOwnerId, int offerId);
        Task<ApiResponse> RefuseOfferAsync(string postOwnerId, int offerId);
    }
}
