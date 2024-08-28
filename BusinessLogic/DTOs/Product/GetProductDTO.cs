using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Product
{
    public class GetProductDTO
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public int StockCount { get; set; }
    }
}
