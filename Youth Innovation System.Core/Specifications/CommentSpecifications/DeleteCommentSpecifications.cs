using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.CommentSpecifications
{
    public class DeleteCommentSpecifications : BaseSpecification<Comment>
    {
        public DeleteCommentSpecifications(int CommentId, string UserId)
           : base(p => p.Id == CommentId && p.UserId == UserId)
        {
            Includes.Add(c => c.Replies);
            Includes.Add(c => c.Reactions);
        }
    }
}
