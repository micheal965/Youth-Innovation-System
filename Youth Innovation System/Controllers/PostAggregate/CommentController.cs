using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices.PostServices;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Comment;
using Youth_Innovation_System.Shared.Exceptions;

namespace Youth_Innovation_System.Controllers.PostAggregate
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [Authorize]
        [HttpPost("Create-Comment")]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {

                var Result = await _commentService.CreateCommentAsync(userId, createCommentDto);
                return Ok(Result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }

        }
        [Authorize]
        [HttpPost("Create-Reply")]
        public async Task<IActionResult> CreateReply(CreateReplyDto createReplyDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var Result = await _commentService.CreateReplyAsync(userId, createReplyDto);
                return Ok(Result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }

        }

        [Authorize]
        [HttpDelete("Delete-Comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var result = await _commentService.DeleteCommentAsync(commentId, userId);
                if (!result)
                    return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Can't delete comment"));

                return Ok(new ApiResponse(StatusCodes.Status200OK, "comment deleted successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
        }

        [Authorize]
        [HttpPut("Update-Comment")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _commentService.UpdateCommentAsync(userId, updateCommentDto);
                return Ok(new ApiResponse(StatusCodes.Status200OK, "Comment updated successfully"));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
        }

        [HttpGet("Get-Comment-By-Id/{commentId}")]
        public async Task<IActionResult> GetComment(int commentId)
        {
            try
            {
                return Ok(new { Commment = await _commentService.GetCommentAsync(commentId) });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [HttpGet("Get-All-Comments/{postId}")]
        public async Task<IActionResult> GetAllComments(int postId)
        {
            try
            {
                return Ok(new { Comments = await _commentService.GetAllCommentsWithRepliesAsync(postId) });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [HttpGet("Get-Comments-Count/{postId}")]
        public async Task<IActionResult> GetCommentsCount(int postId)
        => Ok(new { commentCount = await _commentService.GetCommentsCountAsync(postId) });
    }
}
