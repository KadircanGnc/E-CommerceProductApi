using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.Brand
{
    public class UpdateBrandDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid format")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }
    }
}
