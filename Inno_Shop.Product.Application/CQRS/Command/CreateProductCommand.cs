using Inno_Shop.Product.Application.DTO;
using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Command;

public record CreateProductCommand (string Name,string Description,decimal Price,string CreatedByUserId) : IRequest<ProductDTO>;