using System.Linq.Expressions;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Core.Specifications.PostSpecifications
{
    public class GetPostSpecification : BaseSpecification<Post>
    {
        public GetPostSpecification(int postId)
        : base(p => p.Id == postId)
        {
            Includes.Add(p => p.postImages);
        }
    }
}
