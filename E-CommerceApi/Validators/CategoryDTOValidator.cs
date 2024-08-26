using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name can not be empty!");
        }
    }
}
