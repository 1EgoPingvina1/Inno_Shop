using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Command;

public record DeleteProductCommand(int ProductId) : IRequest<Unit>;