

namespace Youth_Innovation_System.Shared.DTOs.Post
{
    public class PostResponseDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime createdOn { get; set; }
        public int reactsCount { get; set; }
        public List<string>? imagesUrls { get; set; }

    }
}
