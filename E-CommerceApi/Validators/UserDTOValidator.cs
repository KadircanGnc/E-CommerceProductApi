using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name can not be null!");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Enter a valid email!");
        }
    }
}
