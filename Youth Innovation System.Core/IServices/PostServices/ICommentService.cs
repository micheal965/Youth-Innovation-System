using Youth_Innovation_System.Shared.DTOs.Comment;

namespace Youth_Innovation_System.Core.IServices.PostServices
{
    public interface ICommentService
    {

        //Comment
        Task<CommentResponseDto> CreateCommentAsync(string userId, CreateCommentDto createCommentDto);

        //CommentReply
        Task<CommentResponseDto> CreateReplyAsync(string userId, CreateReplyDto dto);

        //For both
        Task<bool> DeleteCommentAsync(int commentId, string userId);
        Task<CommentResponseDto> UpdateCommentAsync(string userId, UpdateCommentDto updateCommentDto);
        Task<CommentResponseDto> GetCommentAsync(int CommentId);
        Task<List<CommentResponseDto>> GetAllCommentsWithRepliesAsync(int postId);
        Task<int> GetCommentsCountAsync(int postId);

    }
}
