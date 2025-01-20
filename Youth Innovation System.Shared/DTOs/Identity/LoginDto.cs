using System.ComponentModel.DataAnnotations;

namespace Youth_Innovation_System.DTOs.Identity
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
