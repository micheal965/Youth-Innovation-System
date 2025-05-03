using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices.PostServices;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Comment;

namespace Youth_Innovation_System.Controllers.PostAggregate
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentReactionController : ControllerBase
    {
        private readonly ICommentReactionService _commentReactionService;

        public CommentReactionController(ICommentReactionService commentReactionService)
        {
            _commentReactionService = commentReactionService;
        }

        [Authorize]
        [HttpPost("Add-Reaction")]
        public async Task<IActionResult> AddReaction(AddReactionToCommentDto addReactionToCommentDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _commentReactionService.AddReactionAsync(addReactionToCommentDto.commentId, userId, addReactionToCommentDto.reactionType);
                return Ok(new ApiResponse(StatusCodes.Status200OK, "React added successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }

        }
        [Authorize]
        [HttpPost("Remove-Reaction/{commentId}")]
        public async Task<IActionResult> RemoveReaction(int commentId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _commentReactionService.RemoveReactionAsync(commentId, userId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("Get-Reactions/{commentId}")]
        public async Task<IActionResult> GetReactions(int commentId)
        {
            var result = await _commentReactionService.GetReactionsAsync(commentId);
            return Ok(new { Reacts = result });
        }
        [HttpGet("Reactions-Count/{commentId}")]
        public async Task<IActionResult> GetReactionsCount(int commentId)
        {
            var result = await _commentReactionService.GetReactionsCountAsync(commentId);
            return Ok(new { reactionCount = result });
        }

    }
}
