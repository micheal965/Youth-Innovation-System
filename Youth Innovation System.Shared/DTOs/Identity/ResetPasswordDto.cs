using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth_Innovation_System.Shared.DTOs.Identity
{
    public class ResetPasswordDto
    {
        public string token { get; set; }
        public string email { get; set; }
        public string newPassword { get; set; }
    }
}
