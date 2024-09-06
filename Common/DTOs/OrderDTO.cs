using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs.Product;

namespace Common.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalAmount { get; set; }        
        public List<GetProductDTO> Products { get; set; } = new List<GetProductDTO>();
    }
}
