using BusinessLogic.DTOs.User;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class UserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name can not be null!");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Enter a valid email!");
        }
    }
}
