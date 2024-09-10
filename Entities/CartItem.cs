using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CartItem : BaseModel
    {        
        public int Quantity { get; set; }    // Quantity of the product in the cart
        public decimal Price { get; set; }
        public int CartId { get; set; }      
        public Cart? Cart { get; set; }       
        public int ProductId { get; set; }   
        public Product? Product { get; set; }		
	}
}
