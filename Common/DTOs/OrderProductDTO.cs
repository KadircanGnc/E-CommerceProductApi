using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class OrderProductDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

    }        
}

