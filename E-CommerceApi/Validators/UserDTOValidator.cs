using Common.DTOs.User;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name can not be null!");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Enter a valid email!");
            RuleFor(u => u.Role).NotEmpty().WithMessage("Role can not be null!");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password can not be null!");
        }
    }

    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name can not be null!");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Enter a valid email!");
            RuleFor(u => u.Role).NotEmpty().WithMessage("Role can not be null!");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password can not be null!");
        }
    }
}
