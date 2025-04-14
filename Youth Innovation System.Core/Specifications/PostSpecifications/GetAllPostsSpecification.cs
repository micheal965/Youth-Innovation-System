using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.PostSpecifications
{
    public class GetAllPostsSpecification : BaseSpecification<Post>
    {
        public GetAllPostsSpecification(int pageNumber, int pageSize)
            : base()
        {
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
            AddOrderByDesc(p => p.createdOn);
            Includes.Add(p => p.postImages);
        }
        //for total count
        public GetAllPostsSpecification()
            : base()
        {
        }
    }
}
