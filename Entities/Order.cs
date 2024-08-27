using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order : BaseModel
    {
        public double TotalAmount { get; set; }
        public int UserId { get; set; } = 1;
        public virtual User? User { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
