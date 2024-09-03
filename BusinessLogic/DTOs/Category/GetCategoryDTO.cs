using BusinessLogic.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Category
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<GetProductDTO>? Products { get; set; }
    }
}
