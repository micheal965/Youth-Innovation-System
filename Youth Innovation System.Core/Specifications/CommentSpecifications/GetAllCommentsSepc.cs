using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Core.Specifications.CommentSpecifications
{
	public class GetAllCommentsSepc:BaseSpecification<Comment>
	{
        public GetAllCommentsSepc(int postId)
            : base(p => p.postId == postId)
        {
            AddOrderByAsc(p=>p.createOn);
        }
    }
}
