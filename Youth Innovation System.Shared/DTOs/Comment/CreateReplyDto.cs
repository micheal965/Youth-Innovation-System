using System.ComponentModel.DataAnnotations;

namespace Youth_Innovation_System.Shared.DTOs.Comment
{
    public class CreateReplyDto
    {
        [Required]
        public int parentCommentId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
