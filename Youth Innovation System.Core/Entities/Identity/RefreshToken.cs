using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youth_Innovation_System.Core.Entities.Identity
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string token { get; set; }
        public DateTime expiryDate { get; set; }
        public string isRevoked { get; set; }



        //mapping with user table

        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
