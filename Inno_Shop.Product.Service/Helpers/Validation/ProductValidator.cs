using FluentValidation;
using Inno_Shop.Product.Service.CQRS.Command;
using Inno_Shop.Product.Service.DTO;

namespace Inno_Shop.Product.Service.Helpers.Validation;

public class ProductValidator: AbstractValidator<ProductDTO>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}