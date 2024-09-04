using BusinessLogic.DTOs.Category;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name can not be empty!");
        }
    }

    public class UpdateCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryDTOValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name can not be empty!");
        }
    }
}
