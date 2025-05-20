using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Youth_Innovation_System.Core.IServices.PostServices;
using Youth_Innovation_System.Core.Shared.Enums;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Post;
using Youth_Innovation_System.Shared.Pagination;

namespace Youth_Innovation_System.Controllers.PostAggregate
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("ip-policy")]
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
            var Result = await _postService.CreatePostAsync(userId, createPostDto);
            return Ok(new Response<PostResponseDto>(Result, StatusCodes.Status200OK, "Post created successfully"));


        }
        [Authorize]
        [HttpDelete("Delete-Post/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _postService.DeletePostAsync(postId, userId);
            if (!result)
                return BadRequest(new Response<bool>(result, StatusCodes.Status400BadRequest, "Can't delete post"));

            return Ok(new Response<bool>(result, StatusCodes.Status200OK, "Post deleted successfully"));
        }
        [HttpGet("Get-Post/{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        => Ok(new Response<PostResponseDto>(await _postService.GetPostAsync(postId), StatusCodes.Status200OK));


        [Authorize]
        [HttpPut("Update-Post")]
        public async Task<IActionResult> UpdatePost(UpdatePostDto updatePostDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _postService.UpdatePostAsync(userId, updatePostDto);
            return Ok(new Response<string>(null, StatusCodes.Status200OK, "Post updated successfully"));
        }
        [HttpGet("Get-User-Posts")]
        public async Task<IActionResult> GetUserPosts(GetUserPostsDto getUserPostsDto)
        {
            var pagedPosts = await _postService.GetAllUserPostsAsync(getUserPostsDto.userId,
                                                                     getUserPostsDto.pageNumber,
                                                                     getUserPostsDto.pageSize);

            return Ok(new Response<PagedResult<PostResponseDto>>(pagedPosts, StatusCodes.Status200OK));
        }

        [Authorize(Roles = nameof(UserRoles.Admin))]
        [HttpGet("Get-All-Posts")]
        public async Task<IActionResult> GetAllPosts(GetAllPostsDto getAllPostsDto)
        {
            //With Pagination
            var pagedPosts = await _postService.GetAllPostsAsync(getAllPostsDto.pageNumber, getAllPostsDto.pageSize);
            return Ok(new Response<PagedResult<PostResponseDto>>(pagedPosts, StatusCodes.Status200OK));

        }

    }
}
