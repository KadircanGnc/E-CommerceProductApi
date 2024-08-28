using BusinessLogic.DTOs.Category;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class CategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name can not be empty!");
        }
    }
}
