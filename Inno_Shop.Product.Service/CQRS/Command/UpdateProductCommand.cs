using Inno_Shop.Product.Service.DTO;
using MediatR;

namespace Inno_Shop.Product.Service.CQRS.Command;

public class UpdateProductCommand : IRequest<Unit>
{
    public UpdateProductCommand(string productName, string description, decimal price)
    {
        ProductName = productName;
        Description = description;
        Price = price;
    }

    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}