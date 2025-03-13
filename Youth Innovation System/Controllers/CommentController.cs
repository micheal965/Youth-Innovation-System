using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.Core.Entities;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.Service.PostServices;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Comment;
using Youth_Innovation_System.Shared.DTOs.Post;
using Youth_Innovation_System.Shared.Exceptions;

namespace Youth_Innovation_System.Controllers
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
        [HttpPost("Create Comment")]
		public async Task<IActionResult> CreateComment(int postId,[FromBody]CreateCommentDto createCommentDto)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			try
			{
				var Result = await _commentService.CreateCommentAsync(userId,postId, createCommentDto);
				return Ok(Result);
			}
			catch (NotFoundException ex)
			{
				return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
			}
		}

		[Authorize]
		[HttpDelete("Delete Comment")]
		public async Task<IActionResult> DeleteComment(int commentId)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var result = await _commentService.DeletePostAsync(commentId, userId);
			if (!result)
				return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Can't delete comment"));

			return Ok(new ApiResponse(StatusCodes.Status200OK, "comment deleted successfully"));
		}

		[Authorize]
		[HttpPut("Update Comment")]
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
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
			}
		}
	}
}
