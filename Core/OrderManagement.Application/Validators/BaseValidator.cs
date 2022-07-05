using FluentValidation;

namespace OrderManagement.Application.Validators
{
    public class BaseValidator<TModel> : AbstractValidator<TModel> where TModel : class
    {
    }
}