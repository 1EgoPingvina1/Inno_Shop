using FluentValidation;
using Inno_Shop.Product.Application.CQRS.Command;

namespace Inno_Shop.Product.Application.CQRS.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product cannot be empty");
    }
}