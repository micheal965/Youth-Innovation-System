using System.Linq.Expressions;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Core.Specifications.PostSpecifications
{
    public class DeletePostSpecification : BaseSpecification<Post>
    {
        public DeletePostSpecification(int postId, string userId)
            : base(p => p.Id == postId && p.UserId == userId)
        {
            Includes.Add(p => p.postImages);
        }
    }
}
