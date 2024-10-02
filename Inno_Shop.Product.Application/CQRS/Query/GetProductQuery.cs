using Inno_Shop.Product.Application.DTO;
using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Query;

public class GetProductQuery : IRequest<ProductDTO>
{
    public int ProductId { get; set; }
}