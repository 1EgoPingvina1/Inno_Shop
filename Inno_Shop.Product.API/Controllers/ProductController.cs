using Inno_Shop.Product.Application.CQRS.Command;
using Inno_Shop.Product.Application.CQRS.Query;
using Inno_Shop.Product.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator) => _mediator = mediator;

    [HttpPost("Create")]
    public async Task<ActionResult<CreateProductCommand>> CreateProduct(CreateProductCommand product) => await _mediator.Send(product);
    
    [HttpGet("GetProduct/{id}")]
    public async Task<ActionResult<GetProductQuery>> GetProduct(int id) => Ok(await _mediator.Send(new GetProductQuery { ProductId = id }));

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<IEnumerable<GetProductsQuery>>> GetProducts() => Ok(await _mediator.Send(new GetProductsQuery()));
    
    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteProduct(DeleteProductCommand command) => Ok(await _mediator.Send(command));
}