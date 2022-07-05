using FluentValidation;
using OrderManagement.Application.DTO.CustomerOrders;

namespace OrderManagement.Application.Validators
{
    public class CustomerOrderValidator : BaseValidator<CreateCustomerOrderModel>
    {
        public CustomerOrderValidator()
        {
            RuleFor(c => c.CustomerId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("CustomerId cannot be null or empty!")
                .Must(c => c > 0)
                .WithMessage("CustomerId must be greater than zero!");
        }
    }
}