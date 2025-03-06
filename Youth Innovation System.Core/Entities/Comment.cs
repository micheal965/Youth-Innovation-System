using System.ComponentModel.DataAnnotations.Schema;

namespace Youth_Innovation_System.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; }//User from Youth innovation system.Identity
        [ForeignKey("post")]
        public int postId { get; set; }
        public string Content { get; set; }
        public DateTime createOn { get; set; }
        //Navigation Properties
        public Post post { get; set; }
        public List<CommentReaction> CommentReactions { get; set; } = new();
        public List<CommentReply> CommentReplies { get; set; } = new();

    }
}
