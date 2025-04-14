using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.PostSpecifications
{
    public class GetAllUserPosts : BaseSpecification<Post>
    {
        //For pagination
        public GetAllUserPosts(string userId, int pageNumber, int pageSize)
        : base(p => p.UserId == userId)
        {
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
            AddOrderByDesc(p => p.createdOn);
            Includes.Add(p => p.postImages);
        }
        //For total count
        public GetAllUserPosts(string userId)
            : base(p => p.UserId == userId)
        {

        }
    }
}
