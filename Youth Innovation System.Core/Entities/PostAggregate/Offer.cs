using System.ComponentModel.DataAnnotations.Schema;
using Youth_Innovation_System.Core.Shared.Enums;

namespace Youth_Innovation_System.Core.Entities.PostAggregate
{
    public class Offer : BaseEntity
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public decimal OfferValue { get; set; }
        public decimal ProfitRate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = OfferStatus.Pending.ToString();
        public string InvestorId { get; set; }//From identity Db
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
