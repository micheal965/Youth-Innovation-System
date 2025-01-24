using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth_Innovation_System.Shared.DTOs.Identity
{
	public class ChangePasswordDto
	{
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }

		[Required(ErrorMessage = "New Password is required")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm Password is required")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "New Password and Confirm Password do not match")]
		public string ConfirmPassword { get; set; }
    }
}
