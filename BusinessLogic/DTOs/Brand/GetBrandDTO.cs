using BusinessLogic.DTOs.Product;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Brand
{
    public class GetBrandDTO
    {
        public string? Name { get; set; }   
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public List<GetProductDTO> Products { get; set; } = new List<GetProductDTO>();
    }
}
