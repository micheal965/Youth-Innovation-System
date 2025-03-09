namespace Youth_Innovation_System.Core.Entities
{
    public class Post : BaseEntity
    {

        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime createdOn { get; set; } = DateTime.UtcNow;
        public int reactsCount { get; set; } = 0;

        //Navigation properties
        public List<React> Reacts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<PostImage> postImages { get; set; } = new();
    }
}
