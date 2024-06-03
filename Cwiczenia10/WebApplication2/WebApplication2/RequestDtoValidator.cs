using WebApplication2.Controller;

namespace WebApplication2;
using FluentValidation;

public class ProductRequestValidator : AbstractValidator<ProductDtoRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .NotNull();

        RuleFor(x => x.Width)
            .GreaterThan(0)
            .NotNull();

        RuleFor(x => x.Height)
            .GreaterThan(0)
            .NotNull();

        RuleFor(x => x.Depth)
            .GreaterThan(0)
            .NotNull();

        RuleFor(x => x.Categories)
            .NotEmpty()
            .NotNull();
    }
}