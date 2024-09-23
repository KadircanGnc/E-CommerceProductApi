using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.Product
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        public int StockCount { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Brand ID is required.")]
        public int BrandId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
