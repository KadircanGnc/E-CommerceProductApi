using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Comment
{
    public class GetCommentDTO
    {
        public int Id { get; set; }
        public string? CommentText { get; set; }        
        public int RatingStar { get; set; }
        public string? UserName { get; set; }
        public string? ProductName { get; set; }
    }
}
