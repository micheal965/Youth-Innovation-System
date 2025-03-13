using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youth_Innovation_System.Shared.DTOs.Comment;
using Youth_Innovation_System.Shared.DTOs.Post;
using Youth_Innovation_System.Shared.Pagination;

namespace Youth_Innovation_System.Core.IServices
{
	public interface ICommentService
	{
		Task<CommentResponseDto> CreateCommentAsync(string userId, int postId, CreateCommentDto createCommentDto);
		Task<bool> DeletePostAsync(int commentId, string userId);
		Task UpdateCommentAsync(string userId, UpdateCommentDto updateCommentDto);
	}
}
