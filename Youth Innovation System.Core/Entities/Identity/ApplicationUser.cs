using Microsoft.AspNetCore.Identity;

namespace Youth_Innovation_System.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? pictureUrl { get; set; }
        public bool status { get; set; } = false;

    }
}
