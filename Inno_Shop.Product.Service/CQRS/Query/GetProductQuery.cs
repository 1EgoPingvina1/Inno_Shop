using Inno_Shop.Product.Service.DTO;
using MediatR;

namespace Inno_Shop.Product.Service.CQRS.Query;

public class GetProductQuery : IRequest<ProductDTO>
{
    public int ProductId { get; set; }
}