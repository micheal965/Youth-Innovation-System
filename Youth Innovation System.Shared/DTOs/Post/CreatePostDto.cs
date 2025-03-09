using Microsoft.AspNetCore.Http;

namespace Youth_Innovation_System.Shared.DTOs.Post
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
