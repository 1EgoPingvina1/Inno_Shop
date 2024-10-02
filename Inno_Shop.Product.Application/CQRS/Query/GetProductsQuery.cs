using Inno_Shop.Product.Application.DTO;
using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Query;

public class GetProductsQuery : IRequest<IEnumerable<ProductDTO>>
{
    
}