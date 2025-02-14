using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth_Innovation_System.Shared.DTOs.Identity
{
    public class VerifyOtpRequestDto
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
