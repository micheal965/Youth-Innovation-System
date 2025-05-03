using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.Entities.PostAggregate;
using Youth_Innovation_System.Core.IRepositories;
using Youth_Innovation_System.Core.IServices.OfferServices;
using Youth_Innovation_System.Core.Shared.Enums;
using Youth_Innovation_System.Core.Specifications.OfferSpecifications;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Offer;

namespace Youth_Innovation_System.Service.PostAggregateServices
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public OfferService(IUnitOfWork unitOfWork,
                           UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IEnumerable<OfferToReturnDto>> GetOffersForPostAsync(int postId, string userId)
        {

            var post = await GetPostWithSpec(postId, userId);

            var Offers = post.Offers.ToList();

            var investorIds = Offers.Select(o => o.InvestorId);
            var investors = await _userManager.Users.Where(u => investorIds.Contains(u.Id)).ToListAsync();

            //For better performance
            var investorDict = investors.ToDictionary(i => i.Id);

            var offerDtos = Offers
                .Select(o =>
                {
                    investorDict.TryGetValue(o.InvestorId, out var Investor);

                    return new OfferToReturnDto
                    {
                        investorName = Investor?.firstName,
                        investorImage = Investor?.pictureUrl,
                        Description = o.Description,
                        OfferValue = o.OfferValue,
                        ProfitRate = o.ProfitRate,
                        CreatedOn = o.CreatedAt,
                        Status = o.Status.ToString()
                    };
                });
            return offerDtos;

        }
        public async Task<IEnumerable<OfferToReturnDto>> GetPendingOffersForPostAsync(int postId, string userId)
        {
            var post = await GetPostWithSpec(postId, userId);

            var pendingOffers = post.Offers.Where(o => o.Status == OfferStatus.Pending.ToString()).ToList();

            var investorIds = pendingOffers.Select(o => o.InvestorId);
            var investors = await _userManager.Users.Where(u => investorIds.Contains(u.Id)).ToListAsync();
            var investorDic = investors.ToDictionary(i => i.Id);

            var offerDtos = pendingOffers
                .Select(o =>
                {
                    investorDic.TryGetValue(o.InvestorId, out var Investor);

                    return new OfferToReturnDto
                    {
                        investorName = Investor?.firstName,
                        investorImage = Investor?.pictureUrl,
                        Description = o.Description,
                        OfferValue = o.OfferValue,
                        ProfitRate = o.ProfitRate,
                        CreatedOn = o.CreatedAt,
                        Status = o.Status.ToString()
                    };
                });
            return offerDtos;

        }

        public async Task<ApiResponse> MakeOfferAsync(string investorId, CreateOfferDto offerDto)
        {
            GetPostForEnsuringOwnerCannotMakeOfferForHimSelfSpecifications spec =
                                new GetPostForEnsuringOwnerCannotMakeOfferForHimSelfSpecifications(offerDto.PostId);

            var Post = await _unitOfWork.Repository<Post>().GetWithSpecAsync(spec);
            if (investorId == Post.UserId) return new ApiResponse(StatusCodes.Status400BadRequest, "PostOwner cannot make offer for himself");

            var newOffer = new Offer()
            {
                InvestorId = investorId,
                Description = offerDto.Description,
                PostId = offerDto.PostId,
                OfferValue = offerDto.OfferValue,
                ProfitRate = offerDto.ProfitRate,
            };
            await _unitOfWork.Repository<Offer>().AddAsync(newOffer);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0) return new ApiResponse(StatusCodes.Status200OK, "Offer sent successfully");

            return new ApiResponse(StatusCodes.Status400BadRequest, "Failed to send offer, please try again later.");
        }
        public async Task<ApiResponse> AcceptOfferAsync(string postOwnerId, int offerId)
        {
            var Offer = await GetOfferWithSpec(postOwnerId, offerId);
            if (Offer == null) return new ApiResponse(StatusCodes.Status400BadRequest, "Cannot accept offer");

            Offer.Status = OfferStatus.Accepted.ToString();
            await _unitOfWork.CompleteAsync();
            return new ApiResponse(StatusCodes.Status200OK, "Offer accepted successfully");
        }
        public async Task<ApiResponse> RefuseOfferAsync(string postOwnerId, int offerId)
        {
            var Offer = await GetOfferWithSpec(postOwnerId, offerId);
            if (Offer == null) return new ApiResponse(StatusCodes.Status400BadRequest, "Cannot refuse offer");

            Offer.Status = OfferStatus.Refused.ToString();
            await _unitOfWork.CompleteAsync();
            return new ApiResponse(StatusCodes.Status200OK, "Offer refused successfully");
        }
        public async Task<OfferToReturnDto> GetOfferForPostAsync(int offerId, string userId)
        {
            var offer = await GetOfferWithSpec(userId, offerId);
            if (offer == null) throw new KeyNotFoundException("Offer not found or you are not authorized.");
            var investor = await _userManager.FindByIdAsync(offer.InvestorId);
            var offerDto = new OfferToReturnDto()
            {
                investorName = investor?.firstName,
                investorImage = investor?.pictureUrl,
                CreatedOn = offer.CreatedAt,
                Description = offer.Description,
                OfferValue = offer.OfferValue,
                ProfitRate = offer.ProfitRate,
                Status = offer.Status,
            };
            return offerDto;

        }
        private async Task<Post> GetPostWithSpec(int postId, string userId)
        {
            GetPostForGettingItsOffersSpecifications spec =
                                                        new GetPostForGettingItsOffersSpecifications(postId, userId);

            var post = await _unitOfWork.Repository<Post>().GetWithSpecAsync(spec);
            if (post == null) throw new KeyNotFoundException("Post not found or you are not authorized.");
            return post;

        }
        private async Task<Offer> GetOfferWithSpec(string postOwnerId, int offerId)
        {
            GetOrAcceptOrRefuseOfferSpecifications spec =
                new GetOrAcceptOrRefuseOfferSpecifications(postOwnerId, offerId);
            var Offer = await _unitOfWork.Repository<Offer>().GetWithSpecAsync(spec);
            return Offer;

        }


    }
}
