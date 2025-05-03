using Microsoft.AspNetCore.Http;
using Youth_Innovation_System.Core.Entities.PostAggregate;
using Youth_Innovation_System.Core.IRepositories;
using Youth_Innovation_System.Core.IServices.PostServices;
using Youth_Innovation_System.Core.Specifications.PostSpecifications;
using Youth_Innovation_System.Shared.ApiResponses;
using Youth_Innovation_System.Shared.DTOs.Post;

namespace Youth_Innovation_System.Service.PostAggregateServices
{
    public class PostReactionService : IPostReactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostReactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddReactionAsync(int postId, string userId, string reactionType)
        {
            var Post = await _unitOfWork.Repository<Post>().GetAsync(postId);
            if (Post == null) throw new KeyNotFoundException("There is no post to React");

            var existingReaction = await GetReactWithSpecAsync(postId, userId);
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
                    PostId = postId,
                    UserId = userId,
                    reactionType = reactionType
                };
                await ReactionRepo.AddAsync(newReaction);
            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ApiResponse> RemoveReactionAsync(int postId, string userId)
        {
            var existingReaction = await GetReactWithSpecAsync(postId, userId);

            if (existingReaction != null)
            {
                _unitOfWork.Repository<React>().Delete(existingReaction);
                await _unitOfWork.CompleteAsync();
                return new ApiResponse(StatusCodes.Status200OK, "React removed successfully");
            }
            //There is not react to remove or he is not authorized but we will not specify it in the response
            return new ApiResponse(StatusCodes.Status404NotFound, "There is not react to remove");

        }

        public async Task<IEnumerable<ReactToReturnDto>> GetReactionsAsync(int postId)
        {
            GetReactsSpecifications spec = new GetReactsSpecifications(postId);
            var Reacts = await _unitOfWork.Repository<React>().GetAllWithSpecAsync(spec);

            var ReactDto = Reacts.Select(r => new ReactToReturnDto
            {
                postId = (int)r.PostId,
                reactionType = r.reactionType,
                UserId = r.UserId,
            });

            return ReactDto;
        }

        public async Task<int> GetReactionsCountAsync(int postId)
        {
            GetReactsSpecifications spec = new GetReactsSpecifications(postId);
            var Reacts = await _unitOfWork.Repository<React>().GetAllWithSpecAsync(spec);
            return Reacts.Count;
        }

        private async Task<React> GetReactWithSpecAsync(int postId, string userId)
        {
            AddOrRemoveReactionSpecifications spec = new AddOrRemoveReactionSpecifications(postId, userId);
            var React = await _unitOfWork.Repository<React>().GetWithSpecAsync(spec);
            return React;
        }
    }

}
