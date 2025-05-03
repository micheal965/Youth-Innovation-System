using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.CommentSpecifications
{
    public class AddOrRemoveCommentReactionSpecifications : BaseSpecification<React>
    {
        public AddOrRemoveCommentReactionSpecifications(int commentId, string userId)
            : base(r => r.CommentId == commentId && r.UserId == userId)
        {

        }
    }
}
