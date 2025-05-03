using System.ComponentModel.DataAnnotations.Schema;

namespace Youth_Innovation_System.Core.Entities.PostAggregate
{
    public class PostImage : BaseEntity
    {
        public string imageUrl { get; set; }
        public string imagePublicId { get; set; }

        [ForeignKey("post")]
        public int PostId { get; set; }
        //Navigation properties
        public Post post { get; set; }
    }
}
