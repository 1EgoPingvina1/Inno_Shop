using Inno_Shop.Product.Application.CQRS.Command;
using Inno_Shop.Product.Application.CQRS.Query;
using Inno_Shop.Product.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator) => _mediator = mediator;
    
    [HttpPost("Create")]
    public async Task<Unit> CreateProduct(ProductDTO product)
    {
        return await _mediator.Send(new CreateProductCommand
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
        });
    }
    
    [HttpGet("GetProduct/{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id) => Ok(await _mediator.Send(new GetProductQuery { ProductId = id }));
    
    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts() => Ok(await _mediator.Send(new GetProductsQuery()));
    
    [HttpDelete("Delete/{id}")]
    public async Task<Unit> DeleteProduct(int id) => await _mediator.Send(new DeleteProductCommand { ProductId = id });
}