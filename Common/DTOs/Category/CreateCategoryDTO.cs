using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
    }
}
