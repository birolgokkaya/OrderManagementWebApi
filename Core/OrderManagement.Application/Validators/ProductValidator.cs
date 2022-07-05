using FluentValidation;
using OrderManagement.Application.DTO.Products;

namespace OrderManagement.Application.Validators
{
    public class ProductValidator : BaseValidator<CreateProductModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Product name cannot be null or empty!")
                .Length(3, 100)
                    .WithMessage("Product name must be between 3 and 100 characters!");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Price cannot be null or empty!")
                .Must(p => p > 0)
                    .WithMessage("Price must be greater than zero!");
        }
    }
}