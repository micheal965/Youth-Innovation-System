﻿using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Core.Specifications.PostSpecifications
{
    public class UpdateOrDeletePostSpecification : BaseSpecification<Post>
    {
        public UpdateOrDeletePostSpecification(int postId, string userId)
            : base(p => p.Id == postId && p.CreatedBy == userId)
        {
            Includes.Add(p => p.postImages);
        }
    }
}
