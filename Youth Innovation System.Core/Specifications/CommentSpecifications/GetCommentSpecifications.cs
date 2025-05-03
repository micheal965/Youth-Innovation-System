using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.CommentSpecifications
{
    public class GetCommentSpecifications : BaseSpecification<Comment>
    {
        public GetCommentSpecifications(int CommentId)
            : base(c => c.Id == CommentId)
        {
            Includes.Add(c => c.Replies);
            Includes.Add(c => c.Reactions);
        }
    }
}
