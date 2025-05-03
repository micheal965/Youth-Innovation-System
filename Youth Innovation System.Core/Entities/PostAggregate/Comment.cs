using System.ComponentModel.DataAnnotations.Schema;

namespace Youth_Innovation_System.Core.Entities.PostAggregate
{
    public class Comment : BaseComment
    {
        [ForeignKey("post")]
        public int postId { get; set; }
        //Navigation Properties
        public Post post { get; set; }


        public int? ParentCommentId { get; set; } //  Foreign key to parent Comment (for replies)
        public Comment ParentComment { get; set; } // Navigation to Parent Comment

        public ICollection<Comment> Replies { get; set; } = new List<Comment>(); // Child Comments (replies)
        public ICollection<React> Reactions { get; set; } = new List<React>(); //  Comment Reactions

    }
}
