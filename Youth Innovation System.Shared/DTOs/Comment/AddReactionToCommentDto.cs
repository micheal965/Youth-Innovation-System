using System.ComponentModel.DataAnnotations;

namespace Youth_Innovation_System.Shared.DTOs.Comment
{
    public class AddReactionToCommentDto
    {
        [Required]
        public int commentId { get; set; }
        [Required]
        public string reactionType { get; set; }
    }
}
