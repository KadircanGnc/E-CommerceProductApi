using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int StockCount { get; set; }
        public int CategoryId { get; set; } = 1;
        public int BrandId { get; set; } = 1;
        public int OrderId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual List<OrderProduct>? OrderProducts { get; set; }
    }
}
