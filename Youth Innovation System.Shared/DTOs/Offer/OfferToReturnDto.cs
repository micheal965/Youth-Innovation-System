namespace Youth_Innovation_System.Shared.DTOs.Offer
{
    public class OfferToReturnDto
    {
        public string investorName { set; get; }
        public string investorImage { set; get; }
        public string Description { get; set; }
        public decimal OfferValue { get; set; }
        public decimal ProfitRate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
    }

}
