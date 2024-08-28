using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Comment : BaseModel
    {
        public string? CommentText { get; set; }
        public int RatingStar { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual List<UserComment> UserComments { get; set; } = new List<UserComment>();

    }
}
