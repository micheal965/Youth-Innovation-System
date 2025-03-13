using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Core.Specifications.CommentSpecifications
{
	public class UpdateOrDeleteCommentSpec :BaseSpecification<Comment>
	{
        public UpdateOrDeleteCommentSpec(int CommentId,string UserId)
			: base(p => p.Id == CommentId && p.UserId == UserId)
		{
            
        }
    }
}
