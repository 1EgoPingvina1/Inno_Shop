using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Command;

public class CreateProductCommand : IRequest<Unit>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CreatedByUserId { get; set; }

}