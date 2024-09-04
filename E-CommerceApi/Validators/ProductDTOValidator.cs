using BusinessLogic.DTOs.Product;
using FluentValidation;

namespace E_CommerceApi.Validators
{
    public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MinimumLength(3).WithMessage("Product name length can not be less than 3");
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(p => p.StockCount).NotEmpty().GreaterThan(0).WithMessage("Stock Count must be greater than 0");
        }
    }

    public class UpdateProductDTOValidator : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MinimumLength(3).WithMessage("Product name length can not be less than 3");
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(p => p.StockCount).NotEmpty().GreaterThan(0).WithMessage("Stock Count must be greater than 0");
        }
    }
}
