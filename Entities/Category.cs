﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category : BaseModel
    {
        public string Name { get; set; }        
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
