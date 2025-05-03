using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.CommentSpecifications
{
    public class GetAllCommentsSpecifications : BaseSpecification<Comment>
    {
        public GetAllCommentsSpecifications(int postId)
            : base(p => p.postId == postId)
        {
            AddOrderByDesc(p => p.createOn);
            Includes.Add(p => p.Replies);
            Includes.Add(p => p.Reactions);
        }
    }
}
