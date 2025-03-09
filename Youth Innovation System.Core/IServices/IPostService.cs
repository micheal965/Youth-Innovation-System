using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youth_Innovation_System.Core.Entities;
using Youth_Innovation_System.Shared.DTOs.Post;

namespace Youth_Innovation_System.Core.IServices
{
    public interface IPostService
    {
        Task<PostResponseDto> CreatePostAsync(string userId, CreatePostDto createPostDto);
        Task<bool> DeletePostAsync(int postId);
        Task<List<Post>> GetAllPostsAsync(int pageNumber, int pageSize);

    }
}
