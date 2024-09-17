using System.Windows.Input;
using Inno_Shop.Product.Service.DTO;
using MediatR;

namespace Inno_Shop.Product.Service.CQRS.Command;

public class CreateProductCommand : IRequest<Unit>
{
    public ProductDTO ProductDto { get; set; }
}