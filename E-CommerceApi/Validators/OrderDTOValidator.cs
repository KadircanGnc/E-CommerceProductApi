using Common.DTOs;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class OrderDTOValidator : AbstractValidator<OrderDTO>
    {
        public OrderDTOValidator()
        {
            RuleFor(o => o.CreateDate).NotEmpty();
        }
    }
}
