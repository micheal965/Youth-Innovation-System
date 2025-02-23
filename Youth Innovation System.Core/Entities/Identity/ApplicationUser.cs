using Microsoft.AspNetCore.Identity;

namespace Youth_Innovation_System.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? pictureUrl { get; set; }
        public bool status { get; set; } = false;
        public string ipAddress { get; set; }


        //for tracking ip address for each login
        public List<UserLoginHistory> userLoginsHistory { get; set; }
        public List<RefreshToken> refreshTokens { get; set; }
    }
}
