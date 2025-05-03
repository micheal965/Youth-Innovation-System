using AutoMapper;
using Youth_Innovation_System.Core.Entities.PostAggregate;
using Youth_Innovation_System.Core.IRepositories;
using Youth_Innovation_System.Core.IServices.PostServices;
using Youth_Innovation_System.Core.Specifications.CommentSpecifications;
using Youth_Innovation_System.Shared.DTOs.Comment;
using Youth_Innovation_System.Shared.Exceptions;

namespace Youth_Innovation_System.Service.PostAggregateServices
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentResponseDto> CreateCommentAsync(string userId, CreateCommentDto createCommentDto)
        {
            var post = await _unitOfWork.Repository<Post>().GetAsync(createCommentDto.postId);
            if (post == null) throw new NotFoundException("Post not found");

            var commentRepo = _unitOfWork.Repository<Comment>();
            var comment = new Comment()
            {
                UserId = userId,
                postId = createCommentDto.postId,
                Content = createCommentDto.Content
            };

            await commentRepo.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CommentResponseDto>(comment);
        }
        public async Task<CommentResponseDto> CreateReplyAsync(string userId, CreateReplyDto dto)
        {
            var parentComment = await _unitOfWork.Repository<Comment>().GetAsync(dto.parentCommentId);
            if (parentComment == null) throw new NotFoundException("Parent comment not found.");

            var reply = new Comment
            {
                UserId = userId,
                Content = dto.Content,
                postId = parentComment.postId,
                ParentCommentId = parentComment.Id,
            };

            await _unitOfWork.Repository<Comment>().AddAsync(reply);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CommentResponseDto>(reply);

        }
        public async Task<bool> DeleteCommentAsync(int commentId, string userId)
        {
            var commentRepo = _unitOfWork.Repository<Comment>();
            DeleteCommentSpecifications spec = new DeleteCommentSpecifications(commentId, userId);
            var comment = await commentRepo.GetWithSpecAsync(spec);
            //if comment not exist or if its not belong to this user so he can't delete it
            if (comment == null) throw new KeyNotFoundException("comment not found");

            commentRepo.Delete(comment);
            //Delete Replies and Reactions manually
            commentRepo.DeleteRange(comment.Replies);
            _unitOfWork.Repository<React>().DeleteRange(comment.Reactions);
            return await _unitOfWork.CompleteAsync() > 0;

        }

        public async Task<List<CommentResponseDto>> GetAllCommentsWithRepliesAsync(int postId)
        {
            var comments = await GetCommentsWithSpec(postId);
            return _mapper.Map<List<CommentResponseDto>>(comments);
        }
        public async Task<int> GetCommentsCountAsync(int postId)
        {
            var comments = await GetCommentsWithSpec(postId);
            return comments.Count;
        }
        public async Task<List<Comment>> GetCommentsWithSpec(int postId)
        {
            var post = await _unitOfWork.Repository<Post>().GetAsync(postId);
            if (post == null) throw new NotFoundException("Post not found");

            GetAllCommentsSpecifications spec = new GetAllCommentsSpecifications(postId);
            var comments = await _unitOfWork.Repository<Comment>().GetAllWithSpecAsync(spec);
            return comments.ToList();
        }
        public async Task<CommentResponseDto> GetCommentAsync(int CommentId)
        {
            GetCommentSpecifications spec = new GetCommentSpecifications(CommentId);

            var comment = await _unitOfWork.Repository<Comment>().GetWithSpecAsync(spec);
            if (comment == null) throw new NotFoundException("comment not found");

            return _mapper.Map<CommentResponseDto>(comment);
        }

        public async Task<CommentResponseDto> UpdateCommentAsync(string userId, UpdateCommentDto updateCommentDto)
        {
            var commentRepo = _unitOfWork.Repository<Comment>();
            UpdateCommentSpecifications spec = new UpdateCommentSpecifications(updateCommentDto.Id, userId);

            var comment = await commentRepo.GetWithSpecAsync(spec);
            if (comment == null) throw new NotFoundException("comment not found");

            comment.Content = updateCommentDto.Content;
            comment.createOn = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CommentResponseDto>(comment);

        }
    }
}
