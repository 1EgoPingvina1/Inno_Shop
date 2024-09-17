using Inno_Shop.Product.Service.CQRS.Command;
using Inno_Shop.Product.Service.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Product.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Unit> CreateProduct(ProductDTO product)
        => await _mediator.Send(new CreateProductCommand { ProductDto = product });
    

    // [HttpGet("{id}")]
    // public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    // {
    //     var query = new GetProductQuery(id);
    //     var product = await _mediator.Send(query);
    //     return Ok(product);
    // }
    //
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    // {
    //     var query = new GetProductsQuery();
    //     var products = await _mediator.Send(query);
    //     return Ok(products);
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, UpdateProductCommand command)
    // {
    //     command.Id = id;
    //     await _mediator.Send(command);
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteProduct(int id)
    // {
    //     var command = new DeleteProductCommand(id);
    //     await _mediator.Send(command);
    //     return NoContent();
    // }
}