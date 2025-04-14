namespace Youth_Innovation_System.Shared.DTOs.Comment
{
    public class CommentResponseDto
    {
        public string Content { get; set; }
        public DateTime createdOn { get; set; }
        public ICollection<CommentReplyToReturnDto?> Replies { get; set; }
        public ICollection<CommentReactionToReturnDto?> Reactions { get; set; }
    }
}
