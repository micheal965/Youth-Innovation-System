using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices.PostServices;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Post;

namespace Youth_Innovation_System.Controllers.PostAggregate
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostReactionController : ControllerBase
    {
        private readonly IPostReactionService _postReactionService;

        public PostReactionController(IPostReactionService postReactionService)
        {
            _postReactionService = postReactionService;
        }

        [Authorize]
        [HttpPost("Add-Reaction")]
        public async Task<IActionResult> AddReaction(AddReactionDto addReactionDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _postReactionService.AddReactionAsync(addReactionDto.postId, userId, addReactionDto.reactionType);
                return Ok(new ApiResponse(StatusCodes.Status200OK, "React added successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [Authorize]
        [HttpPost("Remove-Reaction/{postId}")]
        public async Task<IActionResult> RemoveReaction(int postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _postReactionService.RemoveReactionAsync(postId, userId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("Get-Reactions/{postId}")]
        public async Task<IActionResult> GetReactions(int postId)
        {
            var result = await _postReactionService.GetReactionsAsync(postId);
            return Ok(new { Reacts = result });
        }
        [HttpGet("Reactions-Count/{postId}")]
        public async Task<IActionResult> GetReactionsCount(int postId)
        {
            var result = await _postReactionService.GetReactionsCountAsync(postId);
            return Ok(new { reactionCount = result });
        }
    }
}
