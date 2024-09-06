using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product : BaseModel
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
		public string? ImageUrl { get; set; }
		public int StockCount { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }        
        public virtual Category? Category { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();        
    }
}
