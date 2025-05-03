using Microsoft.AspNetCore.Http;
using Youth_Innovation_System.Core.Entities.PostAggregate;
using Youth_Innovation_System.Core.IRepositories;
using Youth_Innovation_System.Core.IServices.PostServices;
using Youth_Innovation_System.Core.Specifications.CommentSpecifications;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Comment;

namespace Youth_Innovation_System.Service.PostAggregateServices
{
    public class CommentReactionService : ICommentReactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentReactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddReactionAsync(int CommentId, string userId, string reactionType)
        {
            var Comment = await _unitOfWork.Repository<Comment>().GetAsync(CommentId);
            if (Comment == null) throw new KeyNotFoundException("There is no comment to React");

            var existingReaction = await GetReactWithSpecAsync(CommentId, userId);
            var ReactionRepo = _unitOfWork.Repository<React>();

            if (existingReaction != null)
            {
                // Update existing reaction
                existingReaction.reactionType = reactionType;
            }
            else
            {
                // Create new reaction
                var newReaction = new React
                {
                    CommentId = CommentId,
                    UserId = userId,
                    reactionType = reactionType,
                };
                await ReactionRepo.AddAsync(newReaction);
            }
            await _unitOfWork.CompleteAsync();
        }
        public async Task<ApiResponse> RemoveReactionAsync(int CommentId, string userId)
        {
            var existingReaction = await GetReactWithSpecAsync(CommentId, userId);

            if (existingReaction != null)
            {
                _unitOfWork.Repository<React>().Delete(existingReaction);
                await _unitOfWork.CompleteAsync();
                return new ApiResponse(StatusCodes.Status200OK, "React removed successfully");
            }
            //There is not react to remove or he is not authorized but we will not specify it in the response
            return new ApiResponse(StatusCodes.Status404NotFound, "There is not react to remove");

        }

        //check if no reacts
        public async Task<IEnumerable<CommentReactionsToReturnDto>> GetReactionsAsync(int commentId)
        {
            GetCommentReactionsSpecifications spec = new GetCommentReactionsSpecifications(commentId);
            var Reacts = await _unitOfWork.Repository<React>().GetAllWithSpecAsync(spec);

            var ReactDto = Reacts.Select(r => new CommentReactionsToReturnDto
            {
                commentId = (int)r.CommentId,
                reactionType = r.reactionType,
                UserId = r.UserId,
            });

            return ReactDto;
        }
        public async Task<int> GetReactionsCountAsync(int commentId)
        {
            GetCommentReactionsSpecifications spec = new GetCommentReactionsSpecifications(commentId);
            var CommentReactions = await _unitOfWork.Repository<React>().GetAllWithSpecAsync(spec);
            return CommentReactions.Count;
        }
        private async Task<React> GetReactWithSpecAsync(int commentId, string userId)
        {
            AddOrRemoveCommentReactionSpecifications spec = new AddOrRemoveCommentReactionSpecifications(commentId, userId);
            var React = await _unitOfWork.Repository<React>().GetWithSpecAsync(spec);
            return React;
        }
    }
}
