using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Core.Specifications.CommentSpecifications
{
	public class GetCommentSpec:BaseSpecification<Comment>	
	{
        public GetCommentSpec(int commentId)
            :base(p=>p.Id==commentId)
        {
            
        }
    }
}
