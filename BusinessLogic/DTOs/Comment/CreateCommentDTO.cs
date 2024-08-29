using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Comment
{
    public class CreateCommentDTO
    {
        public int Id { get; set; }
        public string? CommentText { get; set; }        
        public int RatingStar { get; set; }        
        public int UserId { get; set; }        
        public int ProductId { get; set; }
        
    }
}
