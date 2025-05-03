namespace Youth_Innovation_System.Core.Entities.PostAggregate
{
    public class BaseComment : BaseEntity
    {
        public string UserId { get; set; }//User from Youth innovation system.Identity
        public string Content { get; set; }
        public DateTime createOn { get; set; } = DateTime.UtcNow;

    }
}
