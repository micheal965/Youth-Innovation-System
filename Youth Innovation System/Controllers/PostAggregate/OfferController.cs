using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices.OfferServices;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Offer;

namespace Youth_Innovation_System.Controllers.PostAggregate
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }
        [HttpPost("Accept/{offerId}")]
        [Authorize]
        public async Task<IActionResult> AcceptOffer(int offerId)
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _offerService.AcceptOfferAsync(ownerId, offerId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Refuse/{offerId}")]
        [Authorize]
        public async Task<IActionResult> RefuseOffer(int offerId)
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _offerService.RefuseOfferAsync(ownerId, offerId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Make-Offer")]
        [Authorize]
        public async Task<IActionResult> MakeOffer(CreateOfferDto proposalDto)
        {
            var InvestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _offerService.MakeOfferAsync(InvestorId, proposalDto);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("Get-Offers/{postId}")]
        [Authorize]
        public async Task<IActionResult> GetOffersForPost(int postId)
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var Offers = await _offerService.GetOffersForPostAsync(postId, ownerId);
                return Ok(Offers);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
        }
        [HttpGet("Get-Offer/{offerId}")]
        [Authorize]
        public async Task<IActionResult> GetOfferForPost(int offerId)
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var Offers = await _offerService.GetOfferForPostAsync(offerId, ownerId);
                return Ok(Offers);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
        }
        [HttpGet("Get-Pending-Offers/{postId}")]
        [Authorize]
        public async Task<IActionResult> GetPendingOffersForPost(int postId)
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var pendingOffers = await _offerService.GetPendingOffersForPostAsync(postId, ownerId);
                return Ok(pendingOffers);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
        }
    }
}
