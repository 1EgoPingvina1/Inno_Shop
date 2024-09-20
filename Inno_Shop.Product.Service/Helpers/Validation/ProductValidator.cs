using FluentValidation;
using Inno_Shop.Product.Service.CQRS.Command;

namespace Inno_Shop.Product.Service.Helpers.Validation;

public class ProductValidator: AbstractValidator<CreateProductCommand>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductDto.Name).NotEmpty().WithMessage("Product name is required");
        RuleFor(x => x.ProductDto.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}