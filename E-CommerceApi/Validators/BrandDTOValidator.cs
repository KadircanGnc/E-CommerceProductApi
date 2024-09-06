using Common.DTOs.Brand;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class CreateBrandDTOValidator : AbstractValidator<CreateBrandDTO>
    {
        public CreateBrandDTOValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(b => b.Email).EmailAddress().WithMessage("Enter a valid email!");
        }
    }

    public class UpdateBrandDTOValidator : AbstractValidator<UpdateBrandDTO>
    {
        public UpdateBrandDTOValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(b => b.Email).EmailAddress().WithMessage("Enter a valid email!");
        }
    }
}
