using FluentValidation;
using Inno_Shop.Product.Service.CQRS.Command;

namespace Inno_Shop.Product.Service.CQRS.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required");
    }
}