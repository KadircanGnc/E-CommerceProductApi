﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderProduct
    {        
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual List<Product>? Products { get; set; }
        public virtual List<Order>? Orders { get; set; }
    }
}
