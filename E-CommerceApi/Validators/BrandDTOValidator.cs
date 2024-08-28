using BusinessLogic.DTOs.Brand;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class BrandDTOValidator : AbstractValidator<GetBrandDTO>
    {
        public BrandDTOValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(b => b.Email).EmailAddress().WithMessage("Enter a valid email!");
        }
    }
}
