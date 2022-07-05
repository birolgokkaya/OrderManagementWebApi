using FluentValidation;
using OrderManagement.Application.DTO.OrderProducts;

namespace OrderManagement.Application.Validators
{
    public class OrderProductValidator : BaseValidator<OrderProductModel>
    {
        public OrderProductValidator()
        {
            RuleFor(p => p.Quantity)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Quantity cannot be null or empty!")
                .Must(q => q > 0)
                    .WithMessage("Quantity must be greater than zero!");
        }
    }
}