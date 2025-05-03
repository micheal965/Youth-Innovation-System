using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.CommentSpecifications
{
    public class GetCommentReactionsSpecifications : BaseSpecification<React>
    {
        public GetCommentReactionsSpecifications(int CommentId)
            : base(r => r.CommentId == CommentId)
        {
        }
    }
}
