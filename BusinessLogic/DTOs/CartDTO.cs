using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public double TotalPrice { get; set; }
        public List<CartItemDTO>? CartItems { get; set; }
    }
}
