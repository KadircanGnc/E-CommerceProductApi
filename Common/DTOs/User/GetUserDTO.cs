﻿using Common.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.User
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }        
        public string? Address { get; set; }
        public virtual List<GetCommentDTO>? Comments { get; set; } = new List<GetCommentDTO>();
    }
}
