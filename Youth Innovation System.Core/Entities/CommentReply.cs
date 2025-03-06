using System.ComponentModel.DataAnnotations.Schema;

namespace Youth_Innovation_System.Core.Entities
{
    public class CommentReply : BaseEntity
    {

        public string UserId { get; set; }
        [ForeignKey("comment")]
        public int commentId { get; set; }
        public string Content { get; set; }

        //Navigation property
        public Comment comment { get; set; }
    }
}
