using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CreditCardDTO
    {
        [Required(ErrorMessage = "Card holder name is required.")]
        public string? CardHolderName { get; set; }

        [Required(ErrorMessage = "Card number is required.")]
        [CreditCard(ErrorMessage = "Invalid card number format.")]
        [StringLength(19, MinimumLength = 16, ErrorMessage = "Card number must be between 16 and 19 digits.")]
        public string? CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration month is required.")]
        [Range(1, 12, ErrorMessage = "Invalid month. Must be between 1 and 12.")]
        public string? ExpireMonth { get; set; }

        [Required(ErrorMessage = "Expiration year is required.")]
        [Range(2023, 2100, ErrorMessage = "Invalid year. Must be a future year.")]
        public string? ExpireYear { get; set; }

        [Required(ErrorMessage = "CVC is required.")]
        [StringLength(3, ErrorMessage = "CVC must be 3 digits.")]
        public string? Cvc { get; set; }
        public int RegisterCard {  get; set; }

    }
}
