using System.ComponentModel.DataAnnotations;

namespace Youth_Innovation_System.Shared.DTOs.UserRating
{
    public class AddOrUpdateRatingDto
    {
        [Required]
        public string ratedUserId { get; set; }
        [Required]

        public int Rating { get; set; }
        [Required]
        public string Review { get; set; }

    }
}
