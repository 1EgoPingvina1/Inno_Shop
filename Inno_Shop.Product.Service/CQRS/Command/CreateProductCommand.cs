using System.Windows.Input;
using Inno_Shop.Product.Service.DTO;
using MediatR;

namespace Inno_Shop.Product.Service.CQRS.Command;

public class CreateProductCommand : IRequest<Unit>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CreatedByUserId { get; set; }

}