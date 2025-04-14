using System.ComponentModel.DataAnnotations;

namespace Youth_Innovation_System.Shared.DTOs.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
