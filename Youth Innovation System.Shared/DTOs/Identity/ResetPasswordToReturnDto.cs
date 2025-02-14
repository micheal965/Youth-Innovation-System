using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth_Innovation_System.Shared.DTOs.Identity
{
    public class ResetPasswordToReturnDto
    {
        public ResetPasswordToReturnDto(int statuscode, string message)
        {
            this.statusCode = statuscode;
            this.message = message;

        }
        public int statusCode { get; set; }
        public string message { get; set; }
    }
}
