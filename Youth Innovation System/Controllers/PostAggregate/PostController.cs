using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices.PostServices;
using Youth_Innovation_System.Core.Shared.Enums;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Post;
using Youth_Innovation_System.Shared.Exceptions;

namespace Youth_Innovation_System.Controllers.PostAggregate
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost("Create-Post")]
        public async Task<IActionResult> CreatePost(CreatePostDto createPostDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var Result = await _postService.CreatePostAsync(userId, createPostDto);
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
        [HttpDelete("Delete-Post/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _postService.DeletePostAsync(postId, userId);
            if (!result)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Can't delete post"));

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Post deleted successfully"));
        }
        [HttpGet("Get-Post/{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            try
            {
                return Ok(await _postService.GetPostAsync(postId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }

        }
        [Authorize]
        [HttpPut("Update-Post")]
        public async Task<IActionResult> UpdatePost(UpdatePostDto updatePostDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _postService.UpdatePostAsync(userId, updatePostDto);
                return Ok(new ApiResponse(StatusCodes.Status200OK, "Post updated successfully"));
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
        [HttpGet("Get-User-Posts")]
        public async Task<IActionResult> GetUserPosts(GetUserPostsDto getUserPostsDto)
        {
            try
            {
                var pagedPosts = await _postService.GetAllUserPostsAsync(getUserPostsDto.userId,
                                                                       getUserPostsDto.pageNumber,
                                                                       getUserPostsDto.pageSize);
                return Ok(pagedPosts);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [Authorize(Roles = nameof(UserRoles.Admin))]
        [HttpGet("Get-All-Posts")]
        public async Task<IActionResult> GetAllPosts(GetAllPostsDto getAllPostsDto)
        {
            try
            {
                //With Pagination
                var pagedPosts = await _postService.GetAllPostsAsync(getAllPostsDto.pageNumber, getAllPostsDto.pageSize);
                return Ok(pagedPosts);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
        }

    }
}
