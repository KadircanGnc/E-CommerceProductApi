using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }    // Quantity of the product in the cart
        public double Price { get; set; }
        public int CartId { get; set; }      
        public Cart? Cart { get; set; }       
        public int ProductId { get; set; }   
        public Product? Product { get; set; } 
        
    }
}
