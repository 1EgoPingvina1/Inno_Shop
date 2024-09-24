using FluentValidation;
using Inno_Shop.Product.Service.CQRS.Command;

namespace Inno_Shop.Product.Service.CQRS.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product cannot be empty");
    }
}