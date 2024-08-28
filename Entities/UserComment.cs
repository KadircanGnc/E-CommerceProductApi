using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserComment : BaseModel
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public virtual User? User { get; set; }
        public virtual Comment? Comment { get; set; }
    }
}
