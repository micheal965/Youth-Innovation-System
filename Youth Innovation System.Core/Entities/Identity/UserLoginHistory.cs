using System.ComponentModel.DataAnnotations.Schema;

namespace Youth_Innovation_System.Core.Entities.Identity
{
    public class UserLoginHistory
    {
        public int Id { get; set; }
        public string ipAddress { get; set; }
        public DateTime LoginTime { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
