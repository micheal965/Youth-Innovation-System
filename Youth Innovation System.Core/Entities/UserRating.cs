
namespace Youth_Innovation_System.Core.Entities
{
    public class UserRating : BaseEntity
    {

        //The User who receiving rate
        public string ratedUserId { get; set; }
        //The User who gives rate
        public string raterUserId { get; set; }

        public int Rating { get; set; }
        public string Review { get; set; }

        public DateTime createdon { get; set; } = DateTime.UtcNow;
    }
}
