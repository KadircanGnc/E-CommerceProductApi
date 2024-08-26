using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class BrandDTOValidator : AbstractValidator<BrandDTO>
    {
        public BrandDTOValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(b => b.Email).EmailAddress().WithMessage("Enter a valid email!");
        }
    }
}
