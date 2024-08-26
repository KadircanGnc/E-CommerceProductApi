using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cart
    {
        public int Id {  get; set; }        
        public DateTime createdDate { get; set; }
        public double TotalPrice { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }        
        public List<CartItem>? CartItems { get; set; }
    }
}
