using System.ComponentModel.DataAnnotations.Schema;

namespace Youth_Innovation_System.Core.Entities
{
    public class React : BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey("post")]
        public int postId { get; set; }
        public string reactionType { get; set; }

        //Navigation Property
        public Post post { get; set; }
    }
}
