using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth_Innovation_System.Core.Entities
{
    public class CommentReaction : BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey("comment")]
        public int commentId { get; set; }
        public string reactionType { get; set; } // "like", "love", "haha"


        //Navigation Properties
        public Comment comment { get; set; }
    }
}
