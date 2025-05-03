using System.ComponentModel.DataAnnotations;

namespace Youth_Innovation_System.Shared.DTOs.Post
{
    public class AddReactionDto
    {
        [Required]
        public int postId { get; set; }
        [Required]
        public string reactionType { get; set; }
    }
}
