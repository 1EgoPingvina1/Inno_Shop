using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Command;

public record UpdateProductCommand(string ProductName,string Description,decimal Price ) : IRequest<Unit>;