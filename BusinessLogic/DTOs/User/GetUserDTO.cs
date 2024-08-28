using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.User
{
    public class GetUserDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }        
        public string? Address { get; set; }        
    }
}
