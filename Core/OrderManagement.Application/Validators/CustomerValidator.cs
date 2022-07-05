using FluentValidation;
using OrderManagement.Application.DTO.Customers;

namespace OrderManagement.Application.Validators
{
    public class CustomerValidator : BaseValidator<CreateCustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.FullName)
                .NotEmpty()
                .NotNull()
                    .WithMessage("FullName cannot be null or empty!")
                .Length(2, 100)
                    .WithMessage("FullName must be between 2 and 100 characters!");

            RuleFor(p => p.Address)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Address cannot be null or empty!")
                .Length(5, 100)
                    .WithMessage("Address must be between 5 and 400 characters!");
        }
    }
}