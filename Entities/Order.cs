using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public double TotalAmount { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }

    }
}
