using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Command;

public class DeleteProductCommand : IRequest<Unit>
{
    public int ProductId { get; set; }
}