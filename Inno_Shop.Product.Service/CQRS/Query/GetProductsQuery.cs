using Inno_Shop.Product.Service.DTO;
using MediatR;

namespace Inno_Shop.Product.Service.CQRS.Query;

public class GetProductsQuery : IRequest<IEnumerable<ProductDTO>>
{
    
}