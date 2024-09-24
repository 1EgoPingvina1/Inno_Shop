using MediatR;

namespace Inno_Shop.Product.Service.CQRS.Command;

public class DeleteProductCommand : IRequest<Unit>
{
    public int ProductId { get; set; }
}