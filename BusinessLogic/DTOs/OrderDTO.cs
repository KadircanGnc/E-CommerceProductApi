using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public double TotalAmount { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}
