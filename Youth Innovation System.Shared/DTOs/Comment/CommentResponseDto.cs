using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth_Innovation_System.Shared.DTOs.Comment
{
	public class CommentResponseDto
	{
		public string Content { get; set; }
		public DateTime createdOn { get; set; }
		public int postId { get; set; }
	}
}