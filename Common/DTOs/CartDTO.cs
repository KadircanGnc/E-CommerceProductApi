using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CartDTO
    {
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemDTO>? CartItems { get; set; }
    }
}
