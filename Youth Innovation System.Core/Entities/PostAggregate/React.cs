using System.ComponentModel.DataAnnotations.Schema;

namespace Youth_Innovation_System.Core.Entities.PostAggregate
{
    public class React : BaseReact
    {

        [ForeignKey("post")]
        public int? PostId { get; set; }
        [ForeignKey("comment")]
        public int? CommentId { get; set; }
        //Navigation Property
        public Post post { get; set; }
        public Comment comment { get; set; }
    }
}
