using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Product
{
    public class CreateProductDTO
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public int StockCount { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
