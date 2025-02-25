using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Youth_Innovation_System.Core.Entities.Identity
{
    [Owned]
    public class RefreshToken
    {
        public int Id { get; set; }
        public string token { get; set; }
        public DateTime expiryDate { get; set; }
        public bool isExpired => expiryDate <= DateTime.UtcNow;
        public DateTime createdOn { get; set; }
        public DateTime? revokedOn { get; set; }
        public bool isActive => revokedOn == null && !isExpired;



        //mapping 1 user : many refreshtoken

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
