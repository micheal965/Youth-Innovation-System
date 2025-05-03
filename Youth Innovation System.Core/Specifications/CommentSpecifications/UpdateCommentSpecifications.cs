using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.CommentSpecifications
{
    public class UpdateCommentSpecifications : BaseSpecification<Comment>
    {
        public UpdateCommentSpecifications(int CommentId, string UserId)
            : base(p => p.Id == CommentId && p.UserId == UserId)
        {
        }
    }
}
