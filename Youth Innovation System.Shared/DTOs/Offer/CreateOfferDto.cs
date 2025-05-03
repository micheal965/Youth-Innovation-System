using System.ComponentModel.DataAnnotations;

namespace Youth_Innovation_System.Shared.DTOs.Offer
{
    public class CreateOfferDto
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public decimal OfferValue { get; set; }
        [Required]
        public decimal ProfitRate { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
