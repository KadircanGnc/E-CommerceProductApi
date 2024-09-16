using Common.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.Product
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
		public string? ImageUrl { get; set; }
        public bool IsInCart { get; set; } = false;
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<GetCommentDTO>? Comments { get; set; } = new List<GetCommentDTO>();
    }
}
